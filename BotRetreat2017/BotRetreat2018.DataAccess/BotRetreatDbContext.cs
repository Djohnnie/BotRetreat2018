﻿using BotRetreat2018.Model;
using Microsoft.EntityFrameworkCore;

namespace BotRetreat2018.DataAccess
{
    public class BotRetreatDbContext : DbContext, IBotRetreatDbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Arena> Arenas { get; set; }

        public DbSet<Bot> Bots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

        public void InitializeDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("TEAMS");
            modelBuilder.Entity<Team>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Team>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Team>().Property(x => x.SysId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Arena>().ToTable("ARENAS");
            modelBuilder.Entity<Arena>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Arena>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Arena>().Property(x => x.SysId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Bot>().ToTable("BOTS");
            modelBuilder.Entity<Bot>().HasKey(x => x.Id).ForSqlServerIsClustered(clustered: false);
            modelBuilder.Entity<Bot>().HasIndex(x => x.SysId).IsUnique().ForSqlServerIsClustered();
            modelBuilder.Entity<Bot>().Property(x => x.SysId).ValueGeneratedOnAdd();
        }
    }
}