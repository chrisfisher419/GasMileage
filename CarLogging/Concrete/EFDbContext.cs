using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
//using CarLogging.Entities;
using CarLogging.Models;

namespace CarLogging.Concrete
{
    public class EFDbContext : System.Data.Entity.DbContext
    {
        public DbSet<LogViewModel> Logs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }


    }
}