using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Taşınmaz_Projesi.Models;

namespace Taşınmaz_Projesi.Context
{
    public class TasinmazContext : DbContext
    {
        public DbSet<Tasinmaz> tasinmaz { get; set; }
        public DbSet<Kullanici> kullanici { get; set; }
        public DbSet<Il> il { get; set; }
        public DbSet<Ilce> ilce { get; set; }
        public DbSet<Mahalle> mahalle { get; set; }
        public DbSet<Log> log { get; set; }
        public DbSet<Login> login { get; set; }

    }
}