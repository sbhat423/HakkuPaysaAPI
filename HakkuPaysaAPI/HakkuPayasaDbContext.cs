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

            // Posts
            modelBuilder.Entity<Post>().ToContainer("Posts");
            modelBuilder.Entity<Post>().OwnsMany<Comment>(p => p.Comments).OwnsOne(c => c.Author);
            modelBuilder.Entity<Post>().OwnsOne<Author>(p => p.Author);

            // UserProfiles
            modelBuilder.Entity<UserProfile>().ToContainer("UserProfiles");
            modelBuilder.Entity<UserProfile>().OwnsOne(ud => ud.Address);
            modelBuilder.Entity<UserProfile>().OwnsOne(ud => ud.Contact);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        // public DbSet<Comment> Comments { get; set; }

    }
}
