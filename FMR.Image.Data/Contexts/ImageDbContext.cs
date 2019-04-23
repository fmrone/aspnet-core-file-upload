using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMR.Image.Data.Contexts
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Entities.Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Image>().HasKey(p => p.Id);
        }
    }
}
