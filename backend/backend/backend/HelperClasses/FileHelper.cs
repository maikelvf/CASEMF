using backend.Data;
using backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace backend.HelperClasses
{
    public class FileHelper
    {
        private CursusDBContext _db;

        public string[] fileContent;

        private int _cursusCount;
        private int _cursusinstantieCount;
        private int _duplicateCursusCount;
        private int _duplicateCursusinstantieCount;

        public FileHelper()
        {
        }

        public FileHelper(CursusDBContext db)
        {
            _db = db;
        }

        public void AddCursussenFromFileToDatabase(HttpPostedFile file)
        {
            InitializeCount();

            fileContent = ReadAllLinesFromFile(file);

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

        public string[] ReadAllLinesFromFile(HttpPostedFile file)
        {
            var content = new StreamReader(file.InputStream).ReadToEnd();
            var lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }

        public List<Cursus> ReadAllCursussenFromFileContent()
        {
            var cursussen = new List<Cursus>();

            for (int i = 0; i < fileContent.Count(); i += 4)
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

        private bool IsNewCursus(List<Cursus> cursussen, Cursus cursus)
        {
            return !cursussen.Any(c => c.Code == cursus.Code) && !_db.Cursussen.Any(c => c.Code == cursus.Code);
        }

        public List<Cursusinstantie> ReadAllInstantiesFromFileContent()
        {
            var instanties = new List<Cursusinstantie>();

            for (int i = 0; i < fileContent.Count(); i += 4)
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

        private bool IsNewInstantie(List<Cursusinstantie> instanties, Cursusinstantie instantie)
        {
            return !instanties.Any(c => c.Cursus.Code == instantie.Cursus.Code && c.Startdatum == instantie.Startdatum) &&
                   !_db.Cursusinstanties.Any(c => c.Cursus.Code == instantie.Cursus.Code && c.Startdatum == instantie.Startdatum);
        }

        public Cursus GetCursus(int i)
        {
            return new Cursus()
            {
                Titel = ExtractTitel(i),
                Code = ExtractCode(i),
                Duur = ExtractDuur(i)
            };
        }

        public int ExtractDuur(int i)
        {
            return int.Parse(fileContent[i + 2].Substring(6, 1));
        }

        public string ExtractCode(int i)
        {
            return fileContent[i + 1].Substring(12);
        }

        public string ExtractTitel(int i)
        {
            return fileContent[i].Substring(7);
        }

        public Cursusinstantie GetCursusinstantie(int i)
        {
            var cursusCode = ExtractCode(i);

            return new Cursusinstantie()
            {
                Cursus = _db.Cursussen.Where(c => c.Code == cursusCode).Single(),
                Startdatum = ExtractStartdatum(i)
            };
        }

        public DateTime ExtractStartdatum(int i)
        {
            return DateTime.Parse(fileContent[i + 3].Substring(12));
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