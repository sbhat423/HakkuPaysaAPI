using HakkuPaysaAPI.DTOs;
using HakkuPaysaAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakkuPaysaAPI
{
    public class HakkuPayasaDbContext : DbContext
    {
        public HakkuPayasaDbContext(DbContextOptions<HakkuPayasaDbContext> options)
        : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Store comments within the posts for now.
            modelBuilder.Entity<Post>().ToContainer("Posts").OwnsMany<Comment>(p => p.Comments);
            // odelBuilder.Entity<Comment>().ToContainer("Comments");


        }

        public DbSet<Post> Posts { get; set; }
        // public DbSet<Comment> Comments { get; set; }

    }
}
