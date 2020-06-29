using backend.Models;
using System.Data.Entity;

namespace backend.Data
{
    public class CursusDBContext : DbContext
    {
        public CursusDBContext() : base ("CursusDB")
        {
        }

        public static CursusDBContext Create()
        {
            return new CursusDBContext();
        }

        public DbSet<Cursus> SugarLevels { get; set; }
    }
}
