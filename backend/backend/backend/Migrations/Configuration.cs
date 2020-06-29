namespace backend.Migrations
{
    using backend.Models;
    using System;
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
            context.Cursussen.AddOrUpdate(x => x.Id,
                new Cursus() { Id = 1, Code = "abc", Duur = 2, Titel = "Cursus 1" },
                new Cursus() { Id = 2, Code = "def", Duur = 5, Titel = "Cursus 2" },
                new Cursus() { Id = 3, Code = "ghi", Duur = 1, Titel = "Cursus 3" }
            );
        }
    }
}
