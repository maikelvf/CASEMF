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
        private static CursusDBContext _db;

        public static void AddCursussenFromFileToDatabase(HttpPostedFile file, CursusDBContext db)
        {
            _db = db;

            string[] lines = ReadAllLinesFromFile(file);

            ReadAllCursussenFromFile(lines);

            _db.SaveChanges();
        }

        public static string[] ReadAllLinesFromFile(HttpPostedFile file)
        {
            var content = new StreamReader(file.InputStream).ReadToEnd();
            var lines = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }

        public static void ReadAllCursussenFromFile(string[] lines)
        {
            for (int i = 0; i < lines.Count(); i += 4)
            {
                Cursus cursus = GetCursus(lines, i);
                _db.Cursussen.Add(cursus);

                Cursusinstantie cursusinstantie = GetCursusinstantie(lines, i, cursus);
                _db.Cursusinstanties.Add(cursusinstantie);
            }
        }
        
        public static Cursus GetCursus(string[] lines, int i)
        {
            return new Cursus()
            {
                Titel = ExtractTitel(lines, i),
                Code = ExtractCode(lines, i),
                Duur = ExtractDuur(lines, i)
            };
        }

        public static int ExtractDuur(string[] lines, int i)
        {
            return int.Parse(lines[i + 2].Substring(6, 1));
        }

        public static string ExtractCode(string[] lines, int i)
        {
            return lines[i + 1].Substring(12);
        }

        public static string ExtractTitel(string[] lines, int i)
        {
            return lines[i].Substring(7);
        }

        public static Cursusinstantie GetCursusinstantie(string[] lines, int i, Cursus cursus)
        {
            return new Cursusinstantie()
            {
                Cursus = cursus,
                Startdatum = ExtractStartdatum(lines, i)
            };
        }

        public static DateTime ExtractStartdatum(string[] lines, int i)
        {
            return DateTime.Parse(lines[i + 3].Substring(12));
        }
    }
}