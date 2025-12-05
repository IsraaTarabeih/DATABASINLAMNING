using System;
using System.ComponentModel.DataAnnotations;
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
                    // Huvudmeny
                    Console.WriteLine("\n----Welcome to shop----");
                    Console.WriteLine("\nPick an option: 1 - Categories | 2 - Customers | 3 - Orders | 4 - Products | 5 - Exit ");
                    Console.WriteLine("Your choice: ");
                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    /* var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries); // Gör att användaren kan mata in flera svar samtidigt, behövs inte i detta fall? 
                     var cmd = parts[0].ToLowerInvariant(); // Gör att det inte spelar någon roll om användaren skriver med små eller stora bokstäver. Behövs inte? 
                    Speciellt inte nu när jag skrivit om edit och delete, test kör.*/

                    switch (input)
                    {
                        case "1":
                            await CategoryMenuAsync();
                            break;
                        case "2":
                            await CustomerMenuAsync();
                            break;
                        case "3":
                            await OrderMenuAsync();
                            break;
                        case "4":
                            await ProductMenuAsync();
                            break;
                        case "5":
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid option.");
                            break;
                    }

                }

                // Metoder för menyval 
                // CRUD-flöde för Categories, Customers, Orders, Products
                static async Task CategoryMenuAsync()
                {
                    using var db = new ShopContext();

                    while (true)
                    {
                        Console.WriteLine("Would you like to: 1 - Show categories | 2 - Add category | 3 - Edit Category | 4 - Delete category | 5 - Return to menu ");
                        Console.WriteLine("Your choice: ");
                        string input = Console.ReadLine()?.Trim() ?? string.Empty;

                        switch (input)
                        {
                            case "1":
                                await ListCategoriesAsync();
                                break;
                            case "2":
                                await AddCategoryAsync();
                                break;
                            case "3":
                                Console.Write("Enter category id to edit: ");
                                var editInput = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!int.TryParse(editInput, out int editId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await EditCategoryAsync(editId);
                                break;
                            case "4":
                                Console.Write("Enter category id to delete: ");
                                var deleteInput = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!int.TryParse(deleteInput, out int deleteId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await DeleteCategoryAsync(deleteId);
                                break;
                            case "5":
                                Console.WriteLine("Returning to main menu...");
                                return;
                            default:
                                Console.WriteLine("Please enter a valid option");
                                break;
                        }
                    }
                }

                static async Task CustomerMenuAsync()
                {
                    using var db = new ShopContext();

                    while (true)
                    {
                        Console.WriteLine("Would you like to: 1 - Show customers | 2 - Add customer | 3 - Edit customer | 4 - Delete customer | 5 - Return to menu ");
                        Console.WriteLine("Your choice: ");
                        string input = Console.ReadLine() ?? string.Empty;

                        switch (input)
                        {
                            case "1":
                                await ListCustomersAsync();
                                break;
                            case "2":
                                await AddCustomerAsync();
                                break;
                            case "3":
                                Console.Write("Enter customer id to edit: ");
                                var editInput = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!int.TryParse(editInput, out int editId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await EditCustomerAsync(editId);
                                break;
                            case "4":
                                Console.Write("Enter customer id to delete: ");
                                var deleteInput = Console.ReadLine()?.Trim()?.Trim() ?? string.Empty;

                                if (!int.TryParse(deleteInput, out int deleteId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await DeleteCustomerAsync(deleteId);
                                break;
                            case "5":
                                Console.WriteLine("Returning to main menu...");
                                return;
                            default:
                                Console.WriteLine("Please enter a valid option");
                                break;

                        }
                    }
                }

                static async Task OrderMenuAsync()
                {
                    using var db = new ShopContext();

                    while (true)
                    {
                        Console.WriteLine("Would you like to: 1 - Show orders | 2 - Add order | 3 - Edit Order | 4 - Delete Order | 5 - Return to menu ");
                        Console.WriteLine("Your choice: ");
                        string input = Console.ReadLine()?.Trim() ?? string.Empty;


                        switch (input)
                        {
                            case "1":
                                await ListOrdersAsync();
                                break;
                            case "2":
                                await AddOrderAsync();
                                break;
                            case "3":
                                Console.Write("Enter customer id to edit: ");
                                var editInput = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!int.TryParse(editInput, out int editId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await EditOrderAsync(editId);
                                break;
                            case "4":
                                Console.Write("Enter customer id to delete: ");
                                var deleteInput = Console.ReadLine()?.Trim()?.Trim() ?? string.Empty;

                                if (!int.TryParse(deleteInput, out int deleteId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await DeleteOrderAsync(deleteId);
                                break;
                            case "5":
                                Console.WriteLine("Returning to main menu...");
                                return;
                            default:
                                Console.WriteLine("Please enter a valid option");
                                break;

                        }
                    }
                }

                static async Task ProductMenuAsync()
                {
                    using var db = new ShopContext();

                    while (true)
                    {
                        Console.WriteLine("Would you like to: 1 - Show products | 2 - Add product | 3 - Edit product | 4 - Delete product | 5 - Return to menu ");
                        Console.WriteLine("Your choice: ");
                        string input = Console.ReadLine()?.Trim() ?? string.Empty;


                        switch (input)
                        {
                            case "1":
                                await ListProductsAsync();
                                break;
                            case "2":
                                await AddProductAsync();
                                break;
                            case "3":
                                Console.Write("Enter product id to edit: ");
                                var editInput = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (!int.TryParse(editInput, out int editId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await EditProductAsync(editId);
                                break;
                            case "4":
                                Console.Write("Enter product id to delete: ");
                                var deleteInput = Console.ReadLine()?.Trim()?.Trim() ?? string.Empty;

                                if (!int.TryParse(deleteInput, out int deleteId))
                                {
                                    Console.WriteLine("You must enter a valid id.");
                                    Console.ReadKey();
                                    break;
                                }
                                await DeleteProductAsync(deleteId);
                                break;
                            case "5":
                                Console.WriteLine("Returning to main menu...");
                                return;
                            default:
                                Console.WriteLine("Please enter a valid option");
                                break;
                        }
                    }
                }

                // Efter huvudmenyn klickar man sig in på dessa beroende på vad man väljer.

                // Metoder för att visa/lägga till/redigera/ta bort Categories
                static async Task ListCategoriesAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task AddCategoryAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task EditCategoryAsync(int editId)
                {
                    using var db = new ShopContext();
                }

                static async Task DeleteCategoryAsync(int deleteId)
                {
                    using var db = new ShopContext();
                }

                // Metoder för att visa/lägga till/redigera/ta bort Customers
                static async Task ListCustomersAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task AddCustomerAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task EditCustomerAsync(int editId)
                {
                    using var db = new ShopContext();
                }

                static async Task DeleteCustomerAsync(int deleteId)
                {
                    using var db = new ShopContext();
                }

                // Metoder för att visa/lägga till/redigera/ta bort Orders
                static async Task ListOrdersAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task AddOrderAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task EditOrderAsync(int editId)
                {
                    using var db = new ShopContext();
                }

                static async Task DeleteOrderAsync(int deleteId)
                {
                    using var db = new ShopContext();
                }

                // Metoder för att visa/lägga till/redigera/ta bort Products
                static async Task ListProductsAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task AddProductAsync()
                {
                    using var db = new ShopContext();
                }

                static async Task EditProductAsync(int editId)
                {
                    using var db = new ShopContext();
                }

                static async Task DeleteProductAsync(int deleteId)
                {
                    using var db = new ShopContext();
                }
            }
        }
    }
}
