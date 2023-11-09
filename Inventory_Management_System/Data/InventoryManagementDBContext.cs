using Inventory_Management_System.Model.Employee;
using Inventory_Management_System.Model.Location;
using Inventory_Management_System.Model.Good;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_System.Model.HandlingUnit;
using Newtonsoft.Json;

namespace Inventory_Management_System.Data
{
    public class InventoryManagementDBContext : DbContext
    {
        public DbSet<RawMaterialLocation> RawMaterialLocations { get; set; }
        public DbSet<OutboundLocation> OutboundLocations { get; set; }
        public DbSet<Box<Component>> ComponentStock { get; set; }
        public DbSet<Box<FinishedGood>> FinishedGoodStock { get; set; }

        public InventoryManagementDBContext(DbContextOptions<InventoryManagementDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // must extend in accordance of new entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinishedGood>()
            .Property(f => f.BuildOfMaterial)
            .IsRequired()
            .HasConversion(
                bom => JsonConvert.SerializeObject(bom),
                jsonBom => jsonBom == null
                    ? new Dictionary<Component, int>() // fallback
                    : JsonConvert.DeserializeObject<Dictionary<Component, int>>(jsonBom)
            );
        }   
    }
}
