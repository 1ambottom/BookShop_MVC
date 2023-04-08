﻿using Bulky.DataAccess;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", displayOrder = 1},
                new Category { Id = 2, Name = "History", displayOrder = 2},
                new Category { Id = 3, Name = "SciFi", displayOrder = 3}
                );
        }
    }
}
