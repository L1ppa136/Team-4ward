using Inventory_Management_System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement_Integration_Test;

public class InventoryManagementFactory : WebApplicationFactory<Program>
{
    //Inmemory-s DB-test
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace YourDbContext1 and YourDbContext2 with the actual names of your DbContext classes
            var dbContext1Descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<InventoryManagementDBContext>)
            );

            if (dbContext1Descriptor != null)
            {
                services.Remove(dbContext1Descriptor);
            }

            services.AddDbContext<InventoryManagementDBContext>(options =>
            {
                //options.UseInMemoryDatabase("InMemoryDatabase1"); // Provide a specific name if needed
                options.UseSqlServer("Data Source =InMemoryDatabase1.db");
            });

            var dbContext2Descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<UsersContext>)
            );

            if (dbContext2Descriptor != null)
            {
                services.Remove(dbContext2Descriptor);
            }

            services.AddDbContext<UsersContext>(options =>
            {
                //options.UseInMemoryDatabase("InMemoryDatabase2"); // Provide a specific name if needed
                options.UseSqlServer("Data Source =InMemoryDatabase2.db");
            });
        });
    }
}