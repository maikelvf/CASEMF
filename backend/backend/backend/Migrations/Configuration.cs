namespace backend.Migrations
{
    using backend.Models;
    using System;
    using System.Collections.Generic;
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
            context.Cursussen.AddOrUpdate(x => x.Titel,
                new Cursus() { Code = "abc", Duur = 2, Titel = "Cursus 1" },
                new Cursus() { Code = "def", Duur = 5, Titel = "Cursus 2" },
                new Cursus() { Code = "ghi", Duur = 1, Titel = "Cursus 3" }
            );

            context.Cursusinstanties.AddOrUpdate(x => x.Startdatum,
                new Cursusinstantie() { Startdatum = "01/01/2020", Cursus = new Cursus() { Code = "zzz", Duur = 3, Titel = "Cursus 10" }},
                new Cursusinstantie() { Startdatum = "05/05/2020", Cursus = new Cursus() { Code = "abcde", Duur = 5, Titel = "Cursus 20" } }
            );
        }
    }
}
