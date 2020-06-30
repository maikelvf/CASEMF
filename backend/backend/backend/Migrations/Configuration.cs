namespace backend.Migrations
{
    using backend.Models;
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<backend.Data.CursusDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(backend.Data.CursusDBContext context)
        {
            var cursussen = new List<Cursus>()
            {
                new Cursus() { Code = "abc", Duur = 2, Titel = "Cursus 10", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "def", Duur = 5, Titel = "Cursus 20", Cursusinstanties = new Collection<Cursusinstantie>() },
                new Cursus() { Code = "ghi", Duur = 1, Titel = "Cursus 30", Cursusinstanties = new Collection<Cursusinstantie>() }
            };

            var cursusinstanties = new List<Cursusinstantie>()
            {
                new Cursusinstantie() { Startdatum = new DateTime(2020, 01, 01).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 05, 05).Date, Cursus = cursussen[0] },
                new Cursusinstantie() { Startdatum = new DateTime(2019, 12, 31).Date, Cursus = cursussen[1] },
                new Cursusinstantie() { Startdatum = new DateTime(2020, 11, 11).Date, Cursus = cursussen[2] }
            };

            cursussen[0].Cursusinstanties.Add(cursusinstanties[0]);
            cursussen[0].Cursusinstanties.Add(cursusinstanties[1]);
            cursussen[1].Cursusinstanties.Add(cursusinstanties[2]);
            cursussen[2].Cursusinstanties.Add(cursusinstanties[3]);

            context.Cursussen.AddOrUpdate(x => x.Titel,
                cursussen[0],
                cursussen[1],
                cursussen[2]);

            context.Cursusinstanties.AddOrUpdate(x => x.Startdatum,
                cursusinstanties[0],
                cursusinstanties[1],
                cursusinstanties[2],
                cursusinstanties[3]
            );
        }
    }
}
