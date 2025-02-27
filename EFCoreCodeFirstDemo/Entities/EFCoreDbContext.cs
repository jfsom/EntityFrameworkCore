﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeFirstDemo.Entities
{
    public class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the SQL Server connection string
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RUC57UF;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseEntity>().UseTpcMappingStrategy();
            modelBuilder.Entity<DerivedEntity1>().ToTable("DerivedTable1");
            modelBuilder.Entity<DerivedEntity2>().ToTable("DerivedTable2");
        }
        public DbSet<BaseEntity> BaseEntites { get; set; }

    }
}