using Microsoft.EntityFrameworkCore;
using MySpectrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySpectrum.Standard
{
    public class DatabaseContext : DbContext
    {
         public DbSet<User> Users { get; set; }

        private readonly string _databasePath;


        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(string.Format("Filename={0}", _databasePath));
        }
    }
}
