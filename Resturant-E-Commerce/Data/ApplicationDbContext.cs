using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resturant_E_Commerce.Models;

namespace Resturant_E_Commerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // composite primary key to the table 
            builder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId , pi.IngredientId});

            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(I => I.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            // seeding data
            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Pizza",
                    Description = "Different types of pizzas"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Burger",
                    Description = "Delicious burgers"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Drinks",
                    Description = "Cold and hot drinks"
                }
            );

            // Seed Ingredients
            builder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientId = 1,
                    Name = "Cheese",
                    Description = "Mozzarella Cheese"
                },
                new Ingredient
                {
                    IngredientId = 2,
                    Name = "Tomato Sauce",
                    Description = "Fresh Tomato Sauce"
                },
                new Ingredient
                {
                    IngredientId = 3,
                    Name = "Beef Patty",
                    Description = "Grilled Beef Patty"
                },
                new Ingredient
                {
                    IngredientId = 4,
                    Name = "Lettuce",
                    Description = "Fresh Lettuce"
                }
            );

            // Seed Products
            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Margherita Pizza",
                    Description = "Classic pizza with cheese and tomato sauce",
                    Price = 120.00m,
                    Stock = 50,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Beef Burger",
                    Description = "Burger with beef patty and lettuce",
                    Price = 150.00m,
                    Stock = 40,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Cola",
                    Description = "Cold soft drink",
                    Price = 25.00m,
                    Stock = 100,
                    CategoryId = 3
                }
            );

            // Seed ProductIngredients
            builder.Entity<ProductIngredient>().HasData(
                new ProductIngredient
                {
                    ProductId = 1,
                    IngredientId = 1
                },
                new ProductIngredient
                {
                    ProductId = 1,
                    IngredientId = 2
                },
                new ProductIngredient
                {
                    ProductId = 2,
                    IngredientId = 3
                },
                new ProductIngredient
                {
                    ProductId = 2,
                    IngredientId = 4
                }
            );
        }
    }
}
