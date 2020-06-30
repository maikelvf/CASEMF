using System;

namespace backend.Models
{
    public class Cursusinstantie
    {
        public int Id { get; set; }

        public DateTime Startdatum { get; set; }

        public virtual Cursus Cursus { get; set; }
    }
}