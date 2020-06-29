using backend.Models;
using System.Data.Entity;

namespace backend.Data
{
    public class CursusDBContext : DbContext
    {
        public CursusDBContext() : base ("CursusDB")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public static CursusDBContext Create()
        {
            return new CursusDBContext();
        }

        public DbSet<Cursus> Cursussen { get; set; }

        public DbSet<Cursusinstantie> Cursusinstanties { get; set; }
    }
}
