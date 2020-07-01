using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace backend.HelperClasses
{
    public class FileHelper
    {
        private CursusDBContext _db;

        private ExtractHelper _extract;

        public string[] fileContent;

        private int _cursusCount;
        private int _cursusinstantieCount;
        private int _duplicateCursusCount;
        private int _duplicateCursusinstantieCount;

        public FileHelper(CursusDBContext db)
        {
            _db = db;
        }

        public FileHelper(CursusDBContext db, ExtractHelper extract)
        {
            _db = db;
            _extract = extract;
        }

        public string FileIsValid(HttpPostedFile file)
        {
            GetContentFromFile(file);

            for (int lineNumber = 0; lineNumber < fileContent.Length - 1; lineNumber++)
            {
                Regex reg;
                
                switch (lineNumber % 5)
                {
                    case 0:
                        // Regex voor "Titel: <titel>"
                        reg = new Regex(@"^Titel:\s.+");
                        if (!reg.IsMatch(fileContent[lineNumber]))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Titel zijn met formaat: 'Titel: <titel>'";
                        }
                        break;

                    case 1:
                        // Regex voor "Cursuscode: <code>"
                        reg = new Regex(@"^Cursuscode:\s.+");
                        if (!reg.IsMatch(fileContent[lineNumber]))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Cursuscode zijn met formaat: 'Cursuscode: <code>'";
                        }
                        break;

                    case 2:
                        // Regex voor "Duur: # dagen"
                        reg = new Regex(@"^Duur:\s\d\sdagen");
                        if (!reg.IsMatch(fileContent[lineNumber]))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Cursusduur zijn met formaat: 'Duur: # dagen'";
                        }
                        break;

                    case 3:
                        // Regex voor "Startdatum: ##/##/####"
                        reg = new Regex(@"^Startdatum:\s\d{1,2}\/\d{1,2}\/\d{4}$");
                        if (!reg.IsMatch(fileContent[lineNumber]))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat, regel {lineNumber + 1} moet Startdatum zijn met formaat: 'Startdatum: 01/01/2020'";
                        }
                        break;

                    case 4:
                        if (!string.IsNullOrEmpty(fileContent[lineNumber]))
                        {
                            return $"Regel {lineNumber + 1} is niet in het juiste formaat: Regel moet een witregel zijn";
                        }
                        break;

                    default:
                        return "Ok";
                }
            }

            return "Ok";
        }

        private void GetContentFromFile(HttpPostedFile file)
        {
            // IStream binnen krijgen ipv HttpPostedFile, dan kan vanuit de unit test ook een IStream mee worden gegeven.
            string content;
            using (StreamReader sr = new StreamReader(file.InputStream))
            {
                content = sr.ReadToEnd();
            }

            fileContent = SplitContentString(content);
        }

        public void AddCursussenFromFileToDatabase(HttpPostedFile file)
        {
            InitializeCount();

            _extract = new ExtractHelper(fileContent);

            try
            {
                var cursussen = ReadAllCursussenFromFileContent();
                _db.Cursussen.AddRange(cursussen);
                _db.SaveChanges();

                var instanties = ReadAllInstantiesFromFileContent();
                _db.Cursusinstanties.AddRange(instanties);
                _db.SaveChanges();
            }
            finally
            {
                _db.Dispose();
            }
        }

        public void InitializeCount()
        {
            _cursusCount = 0;
            _cursusinstantieCount = 0;
            _duplicateCursusCount = 0;
            _duplicateCursusinstantieCount = 0;
        }

        public string[] SplitContentString(string content)
        {
            var splitContent = content.Split(new [] { Environment.NewLine }, StringSplitOptions.None);
            return splitContent;
        }

        public List<Cursus> ReadAllCursussenFromFileContent()
        {
            var cursussen = new List<Cursus>();

            for (int i = 0; i < fileContent.Length - 1; i += 5)
            {
                Cursus cursus = GetCursus(i);

                var newCursus = IsNewCursus(cursussen, cursus);

                if (newCursus)
                {
                    cursussen.Add(cursus);
                    _cursusCount += 1;
                }
                else
                {
                    _duplicateCursusCount += 1;
                }
            }
            return cursussen;
        }

        public bool IsNewCursus(List<Cursus> cursussen, Cursus cursus)
        {
            return !cursussen.Any(c => c.Code == cursus.Code) && !_db.Cursussen.Any(c => c.Code == cursus.Code);
        }

        public List<Cursusinstantie> ReadAllInstantiesFromFileContent()
        {
            var instanties = new List<Cursusinstantie>();

            for (int i = 0; i < fileContent.Length - 1; i += 5)
            {
                Cursusinstantie instantie = GetCursusinstantie(i);
                bool newInstantie = IsNewInstantie(instanties, instantie);

                if (newInstantie)
                {
                    instanties.Add(instantie);
                    _cursusinstantieCount += 1;
                }
                else
                {
                    _duplicateCursusinstantieCount += 1;
                }
            }
            return instanties;
        }

        public bool IsNewInstantie(List<Cursusinstantie> instanties, Cursusinstantie instantie)
        {
            return !instanties.Any(c => c.Cursus.Code == instantie.Cursus.Code && c.Startdatum == instantie.Startdatum) &&
                   !_db.Cursusinstanties.Any(c => c.Cursus.Code == instantie.Cursus.Code && c.Startdatum == instantie.Startdatum);
        }

        public Cursus GetCursus(int i)
        {
            return new Cursus()
            {
                Titel = _extract.ExtractTitel(i),
                Code = _extract.ExtractCode(i),
                Duur = _extract.ExtractDuur(i)
            };
        }

        public Cursusinstantie GetCursusinstantie(int i)
        {
            var cursusCode = _extract.ExtractCode(i);

            return new Cursusinstantie()
            {
                Cursus = _db.Cursussen.Where(c => c.Code == cursusCode).Single(),
                Startdatum = _extract.ExtractStartdatum(i)
            };
        }

        public string ReturnMessage()
        {
            var message = $"{_cursusCount} nieuwe cursus(sen) toegevoegd, {_cursusinstantieCount} nieuwe instantie(s) toegevoegd.";

            if (_duplicateCursusCount > 0)
            {
                message += $" {_duplicateCursusCount} cursus(sen) dubbel, niet toegevoegd.";
            }

            if (_duplicateCursusinstantieCount > 0)
            {
                message += $" {_duplicateCursusinstantieCount} cursusinstantie(s) dubbel, niet toegevoegd.";
            }

            return message;
        }


    }
}