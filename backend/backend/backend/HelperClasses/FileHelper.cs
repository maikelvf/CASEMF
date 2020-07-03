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

        private static string[] _fileContent;

        private int _cursusCount;
        private int _cursusinstantieCount;
        private int _duplicateCursusCount;
        private int _duplicateCursusinstantieCount;

        public FileHelper(CursusDBContext db)
        {
            _db = db;
        }

        public FileHelper(CursusDBContext db, string[] fileContent)
        {
            _db = db;
            _fileContent = fileContent;
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
                Titel = _fileContent.ExtractTitel(i),
                Code = _fileContent.ExtractCode(i),
                Duur = _fileContent.ExtractDuur(i)
            };
        }

        public Cursusinstantie GetCursusinstantie(int i)
        {
            var cursusCode = _fileContent.ExtractCode(i);

            return new Cursusinstantie()
            {
                Cursus = _db.Cursussen.Where(c => c.Code == cursusCode).Single(),
                Startdatum = _fileContent.ExtractStartdatum(i)
            };
        }

        public string ReturnMessage()
        {
            var message = string.Format(resources.CursussenToegevoegd, _cursusCount, _cursusinstantieCount);
                
            if (_duplicateCursusCount > 0)
            {
                message += string.Format(resources.DuplicateCursussen, _duplicateCursusCount);
            }

            if (_duplicateCursusinstantieCount > 0)
            {
                message += string.Format(resources.DuplicateInstanties, _duplicateCursusinstantieCount);
            }

            return message;
        }
    }
}