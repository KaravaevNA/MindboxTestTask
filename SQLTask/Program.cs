using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SQLTask
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new();
    }

    public class MyDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; AttachDbFilename=sqltask.mdf; Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(product => product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity(
                    "CategoryProduct",
                    l => l.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId").HasPrincipalKey(nameof(Category.Id)),
                    r => r.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.Id)),
                    j => j.HasKey("ProductId", "CategoryId"));
        }
    }

    class Program
    {
        //Пример заполнения
        static void DbFilling()
        {
            using (MyDbContext context = new MyDbContext())
            {
                Product sony = new Product { Name = "Sony HDR-CX405" };
                Product hp = new Product { Name = "HP 15s-fq0068ur" };
                Product jardin = new Product { Name = "Jardin Colombia Supremo" };
                context.Products.AddRange(sony, hp, jardin);

                Category electronics = new Category { Name = "Electronics" };
                Category videoCameras = new Category { Name = "Video cameras" };
                Category laptops = new Category { Name = "Laptops" };
                Category foods = new Category { Name = "Foods" };
                Category coffees = new Category { Name = "Coffees" };
                context.Categories.AddRange(electronics, videoCameras, laptops, foods, coffees);

                sony.Categories.Add(electronics);
                sony.Categories.Add(videoCameras);
                hp.Categories.Add(electronics);
                hp.Categories.Add(laptops);
                jardin.Categories.Add(foods);
                jardin.Categories.Add(coffees);

                Product uncategorized = new Product { Name = "uncategorized" };
                context.Products.Add(uncategorized);

                context.SaveChanges();
            }
        }

        // Запрос с использованием методов Entity Framework
        static void DbRequest()
        {
            using (var context = new MyDbContext())
            {
                var result = context.Products
                    .SelectMany(p => p.Categories.DefaultIfEmpty(), (p, c) => new { ProductName = p.Name, CategoryName = c.Name })
                    .ToList();

                foreach (var item in result)
                    Console.WriteLine($"Product: {item.ProductName.PadRight(25)}, Category: {item.CategoryName}");
            }
        }

        static void Main()
        {
            DbRequest();
        }
    }
}
