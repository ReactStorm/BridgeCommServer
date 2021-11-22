using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }
    }
}
