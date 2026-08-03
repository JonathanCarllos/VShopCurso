using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Category
            mb.Entity<Category>().HasKey(c => c.CategoryId);

            mb.Entity<Category>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Product
            mb.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(255)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasMaxLength(255)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(12, 2);

            mb.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Categories
            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Eletrônicos"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Informática"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Acessórios"
                }
            );

            // Seed Products
            mb.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Notebook Dell Inspiron",
                    Description = "Notebook Intel Core i5, 16GB RAM, SSD 512GB",
                    Price = 4299.90m,
                    ImageUrl = "notebook-dell.jpg",
                    Stock = 15,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Mouse Gamer Logitech G203",
                    Description = "Mouse Gamer RGB 8000 DPI",
                    Price = 149.90m,
                    ImageUrl = "mouse-g203.jpg",
                    Stock = 50,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Teclado Mecânico Redragon Kumara",
                    Description = "Switch Blue ABNT2",
                    Price = 279.90m,
                    ImageUrl = "kumara.jpg",
                    Stock = 35,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Smartphone Samsung Galaxy S25",
                    Description = "256GB, 12GB RAM",
                    Price = 5299.90m,
                    ImageUrl = "galaxy-s25.jpg",
                    Stock = 20,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Monitor LG UltraWide 29\"",
                    Description = "Monitor IPS Full HD",
                    Price = 1299.90m,
                    ImageUrl = "lg-ultrawide.jpg",
                    Stock = 12,
                    CategoryId = 2
                }
            );
        }
    }
}