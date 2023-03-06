using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrucksTakov.Data;
using TrucksTakov.Domain;


namespace TrucksTakov.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataManufacturer = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedManufacturers(dataManufacturer);
            return app;
        }

        public static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Administrator", "Client" };

            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNumber = "0888888888";
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                var result = await userManager.CreateAsync(user, "Admin123456");
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();

                }
            }
        }

        public static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }

            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName="Dumper"},
                new Category{CategoryName="Road assistance"},
                new Category{CategoryName="Тug"},
                new Category{CategoryName="Refrigerator truck"},
                new Category{CategoryName="Tank Truc"},
                new Category{CategoryName="Accessory"},
                
            });
            dataCategory.SaveChanges();
        }

        public static void SeedManufacturers(ApplicationDbContext dataManufacturer)
        {
            if (dataManufacturer.Manufacturers.Any())
            {
                return;
            }

            dataManufacturer.Manufacturers.AddRange(new[]
            {
                new Manufacturer{ManufacturerName="Man"},
                new Manufacturer{ManufacturerName="Scania"},
                new Manufacturer{ManufacturerName="Mercedes-Benz"},
                new Manufacturer{ManufacturerName="Volvo"},
                new Manufacturer{ManufacturerName="Daf"},
                new Manufacturer{ManufacturerName="Astra"},
                new Manufacturer{ManufacturerName="Citroen"},
                new Manufacturer{ManufacturerName="Mazda"},

            });
            dataManufacturer.SaveChanges();
        }
    }
}
