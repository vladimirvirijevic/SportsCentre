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
        public DbSet<Match> Matches { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<TrainingPlayer> TrainingPlayers { get; set; }
        public DbSet<Permit> Permits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainingPlayer>()
                .HasKey(tp => new { tp.TrainingId, tp.PlayerId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-D1AVCJU;Database=SportsCentreDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
