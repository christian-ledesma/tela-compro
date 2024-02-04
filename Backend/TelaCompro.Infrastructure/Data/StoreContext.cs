using Microsoft.EntityFrameworkCore;
using TelaCompro.Domain.Entities;

namespace TelaCompro.Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasMany(x => x.Tags)
                .WithMany(x => x.Products);

            modelBuilder
                .Entity<User>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Owner);

            base.OnModelCreating(modelBuilder);
        }
    }
}
