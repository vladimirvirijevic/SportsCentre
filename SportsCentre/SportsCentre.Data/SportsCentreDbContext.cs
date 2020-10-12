using Microsoft.EntityFrameworkCore;
using SportsCentre.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCentre.Data
{
    public class SportsCentreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Centre> Centres { get; set; }
        public DbSet<Court> Courts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Stock);

            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-D1AVCJU;Database=SportsCentreDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
