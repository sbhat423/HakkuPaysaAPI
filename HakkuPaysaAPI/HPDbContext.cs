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

        public DbSet<Post> Posts { get; set; }

    }
}
