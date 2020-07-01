using backend.Data;
using backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace backend.HelperClasses
{
    public class FileHelper
    {
        private CursusDBContext _db;

        private ExtractHelper _extract;

        private static string[] _fileContent;

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

        public static string[] GetContentFromFile(Stream file)
        {
            string[] content;
            using (StreamReader sr = new StreamReader(file))
            {
                content = sr.ReadToEnd().SplitContentString();
            }

            _fileContent = content;

            return content;
        }

        public void AddCursussenFromFileToDatabase()
        {
            InitializeCount();

            _extract = new ExtractHelper(_fileContent);

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

        public List<Cursus> ReadAllCursussenFromFileContent()
        {
            var cursussen = new List<Cursus>();

            for (int i = 0; i < _fileContent.Length - 1; i += 5)
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

            for (int i = 0; i < _fileContent.Length - 1; i += 5)
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