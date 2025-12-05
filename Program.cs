using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Threading.Tasks;
using DATABASINLÄMNING.Models;
using Microsoft.EntityFrameworkCore;

namespace DATABASINLÄMNING
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("DB: " + Path.Combine(AppContext.BaseDirectory, "shop.db"));

            using (var db = new ShopContext())
            {
                await db.Database.MigrateAsync();

                if (!await db.Categories.AnyAsync())
                {
                    db.Categories.AddRange(
                        new Category { CategoryName = "Dairy", CategoryDescription = "Milk products, cheeses, butter, yogurt, etc.." },
                        new Category { CategoryName = "Meat", CategoryDescription = "Various types of fresh and processed meat." },
                        new Category { CategoryName = "Produce", CategoryDescription = "Fresh fruits and vegetables." }
                    );

                    await db.SaveChangesAsync();
                    Console.WriteLine("Categories seeded to DB");
                }

                if (!await db.Customers.AnyAsync())
                {
                    db.Customers.AddRange(
                        new Customer { CustomerName = "Israa Tarabeih", Email = "Israa123@hotmail.com", City = "Västervik" },
                        new Customer { CustomerName = "Anna Andersson", Email = "AnnaA@hotmail.com", City = "Stockholm" },
                        new Customer { CustomerName = "Arvid Castello", Email = "Arvid@gmail.com", City = "London" }
                    );

                    await db.SaveChangesAsync();
                    Console.WriteLine("Customers seeded to DB");
                }

                var dairy = await db.Categories.FirstAsync(g => g.CategoryName == "Dairy");
                var meat = await db.Categories.FirstAsync(g => g.CategoryName == "Meat");
                var produce = await db.Categories.FirstAsync(g => g.CategoryName == "Produce");

                if (!await db.Products.AnyAsync())
                {
                    db.Products.AddRange(
                        new Product { ProductName = "Milk", Price = 32, Category = dairy },
                        new Product { ProductName = "Banana", Price = 27, Category = produce },
                        new Product { ProductName = "Cheese", Price = 89, Category = dairy },
                        new Product { ProductName = "Butter", Price = 48, Category = dairy },
                        new Product { ProductName = "Apple", Price = 35, Category = produce },
                        new Product { ProductName = "Turkey", Price = 38, Category = meat },
                        new Product { ProductName = "Minced meat", Price = 129, Category = meat }
                    );

                    await db.SaveChangesAsync();
                    Console.WriteLine("Products seeded to DB");
                }  

                while (true)
                {

                }
            }
        }
    }
}
