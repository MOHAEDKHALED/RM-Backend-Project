using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace RadwaMintaWebAPI.Data.SeedingData
{
    public class DataSeeding(MintaDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }



                // Categories
                if (!_dbContext.Set<Category>().Any())
                {
                    var CategoriesData = File.OpenRead(@"Data\Categories.json");
                    var Categories = await JsonSerializer.DeserializeAsync<List<Category>>(CategoriesData);
                    if (Categories is not null && Categories.Any())
                    {
                        await _dbContext.Categories.AddRangeAsync(Categories);
                    }
                }

                // Products
                if (!_dbContext.Set<Product>().Any())
                {
                    var ProductsData = File.OpenRead(@"Data\Products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                    }

                }

                // Social Media
                if (!_dbContext.Set<SocialMedia>().Any())
                {
                    var MediasData = File.OpenRead(@"Data\SocialMedia.json");
                    var Medias = await JsonSerializer.DeserializeAsync<List<SocialMedia>>(MediasData);
                    if (Medias is not null && Medias.Any())
                    {
                        await _dbContext.Medias.AddRangeAsync(Medias);
                    }

                }

                // Quality 
                if (!_dbContext.Set<Quality>().Any())
                {
                    var QualityData = File.OpenRead(@"Data\Quality.json");
                    var Qualities = await JsonSerializer.DeserializeAsync<List<Quality>>(QualityData);
                    if (Qualities is not null && Qualities.Any())
                    {
                        await _dbContext.Qualities.AddRangeAsync(Qualities);
                    }

                }

                // Reviews
                if (!_dbContext.Set<Review>().Any())
                {
                    var ReviewData = File.OpenRead(@"Data\Reviews.json");
                    var Reviews = await JsonSerializer.DeserializeAsync<List<Review>>(ReviewData);
                    if (Reviews is not null && Reviews.Any())
                    {
                        await _dbContext.Reviews.AddRangeAsync(Reviews);
                    }

                }

                // Admin
                if (!_dbContext.Set<AdminUser>().Any())
                {
                    var AdminData = File.OpenRead(@"Data\AdminData.json");
                    var Admin = await JsonSerializer.DeserializeAsync<List<AdminUser>>(AdminData);
                    if (Admin is not null && Admin.Any())
                    {
                        await _dbContext.Admins.AddRangeAsync(Admin);
                    }

                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data seeding: {ex.Message}");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
