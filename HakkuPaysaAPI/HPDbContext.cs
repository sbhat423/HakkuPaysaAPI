using HakkuPaysaAPI.DTOs;
using HakkuPaysaAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI
{
    public class HPDbContext : DbContext
    {
        public HPDbContext(DbContextOptions<HPDbContext> options)
        : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToContainer("HPDbContext").OwnsMany<Comment>(p => p.Comments);
        
        }

        public DbSet<Post> Posts { get; set; }

    }
}
