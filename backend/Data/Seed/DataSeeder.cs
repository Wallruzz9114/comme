using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Models;
using Serilog;

namespace Data.Seed
{
    public class DataSeeder
    {
        public static async Task SeedAsync(DatabaseContext databaseContext)
        {
            try
            {
                await databaseContext.Database.EnsureCreatedAsync();

                if (!databaseContext.ProductBrands.Any())
                {
                    var productBrandsStringData = File.ReadAllText("../Data/Seed/json/brands.json");
                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsStringData);

                    foreach (var productBrand in productBrands) databaseContext.ProductBrands.Add(productBrand);

                    await databaseContext.SaveChangesAsync();
                }

                if (!databaseContext.ProductTypes.Any())
                {
                    var productTypesStringData = File.ReadAllText("../Data/Seed/json/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesStringData);

                    foreach (var productType in productTypes) databaseContext.ProductTypes.Add(productType);

                    await databaseContext.SaveChangesAsync();
                }

                if (!databaseContext.Products.Any())
                {
                    var productsStringData = File.ReadAllText("../Data/Seed/json/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsStringData);

                    foreach (var product in products)
                        databaseContext.Products.Add(product);

                    await databaseContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"An error occurred while seeding the database " +
                    $"{exception.Message} {exception.StackTrace} " +
                    $"{exception.InnerException} {exception.Source}"
                );
            }
        }

        public static async Task SeedIdentityAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FullName = "Bob",
                    Email = "bob@email.com",
                    UserName = "bob@email.com",
                    Address = new Address
                    {
                        Street = "9916 111 St",
                        City = "Edmonton",
                        Province = "AB",
                        PostalCode = "T5H 0C5",
                        Country = "Canada"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}