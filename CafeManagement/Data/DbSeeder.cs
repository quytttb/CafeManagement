using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Data;

// Data/DbSeeder.cs
public static class DbSeeder
{
    public static async Task SeedDefaultData(ApplicationDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed Roles if none exist
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role
                {
                    RoleName = "Admin",
                    CanManageEmployees = true,
                    CanManageProducts = true,
                    CanManageOrders = true,
                    IsAdmin = true
                },
                new Role
                {
                    RoleName = "Manager",
                    CanManageEmployees = true,
                    CanManageProducts = true,
                    CanManageOrders = true,
                    IsAdmin = false
                },
                new Role
                {
                    RoleName = "Staff",
                    CanManageEmployees = false,
                    CanManageProducts = false,
                    CanManageOrders = true,
                    IsAdmin = false
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        // Seed default store if none exists
        if (!context.Stores.Any())
        {
            var store = new Store
            {
                StoreName = "Main Branch",
                Address = "123 Main Street",
                Phone = "0123456789"
            };

            await context.Stores.AddAsync(store);
            await context.SaveChangesAsync();

            // Seed default admin employee
            if (!context.Employees.Any())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
                var adminEmployee = new Employee
                {
                    Name = "Admin User",
                    Username = "admin",
                    // In production, use a proper password hashing mechanism
                    Password = "8899",
                    HireDate = DateTime.UtcNow,
                    StoreId = store.StoreId,
                    RoleId = adminRole.RoleId
                };

                await context.Employees.AddAsync(adminEmployee);
                await context.SaveChangesAsync();
            }
        }

        // Seed sample products if none exist
        if (!context.Products.Any())
        {
            var store = await context.Stores.FirstAsync();
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Espresso",
                    Price = 2.50m,
                    Stock = 100,
                    StoreId = store.StoreId
                },
                new Product
                {
                    Name = "Cappuccino",
                    Price = 3.50m,
                    Stock = 100,
                    StoreId = store.StoreId
                },
                new Product
                {
                    Name = "Latte",
                    Price = 3.00m,
                    Stock = 100,
                    StoreId = store.StoreId
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}