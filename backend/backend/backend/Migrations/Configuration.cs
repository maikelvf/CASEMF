namespace backend.Migrations
{
    using backend.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<backend.Data.CursusDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.CursusDBContext context)
        {
            var cursussen = new List<Cursus>()
            {
                new Cursus() { Code = "CNETIN", Duur = 5, Titel = "C# Programmeren", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "JPA", Duur = 2, Titel = "Java Persistence API", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "BLZ", Duur = 5, Titel = "Blazor", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "LNQ", Duur = 2, Titel = "LINQ", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "AZF", Duur = 5, Titel = "Azure Fundamentals", Cursusinstanties = new Collection<Cursusinstantie>() }
            };

            var cursusinstanties = new List<Cursusinstantie>()
            {
                // 2019 week 25 t/m 28
                new Cursusinstantie() { Startdatum = new DateTime(2019, 06, 29).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 07, 06).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 07, 14).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 07, 06).Date, Cursus = cursussen[2] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 07, 01).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 06, 22).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 06, 29).Date, Cursus = cursussen[4] },

                // 2020 week 26 t/m 29
                new Cursusinstantie() { Startdatum = new DateTime(2020, 06, 22).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 06, 29).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 06).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 01).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 09).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 14).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 06).Date, Cursus = cursussen[2] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 13).Date, Cursus = cursussen[2] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 01).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 06).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 09).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 06, 22).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 06, 29).Date, Cursus = cursussen[4] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 07, 06).Date, Cursus = cursussen[4] },

                // 2021 week 25 t/m 28
                new Cursusinstantie() { Startdatum = new DateTime(2021, 06, 29).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 06).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 01).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 14).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 06).Date, Cursus = cursussen[2] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 13).Date, Cursus = cursussen[2] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 01).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 09).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 06, 22).Date, Cursus = cursussen[3] },
                new Cursusinstantie() { Startdatum = new DateTime(2021, 07, 06).Date, Cursus = cursussen[4] },
            };

            context.Cursussen.AddOrUpdate(cursussen.ToArray());

            context.Cursusinstanties.AddOrUpdate(cursusinstanties.ToArray());
        }
    }
}
