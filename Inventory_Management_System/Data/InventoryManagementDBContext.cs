using Inventory_Management_System.Model.Employee;
using Inventory_Management_System.Model.Location;
using Inventory_Management_System.Model.Good;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_System.Model.HandlingUnit;
using Newtonsoft.Json;
using Inventory_Management_System.Model.Enums;

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
                    ? new Dictionary<Component, int>()
                    : JsonConvert.DeserializeObject<Dictionary<Component, int>>(jsonBom)
            );
            //MUST ADD TO ALL IMPLEMENTATIONS OF "GOOD" ABSTRACT CLASS
            modelBuilder
            .Entity<Component>()
            .Property(g => g.ProductDesignation)
            .HasConversion(
            v => v.ToString(),
            v => (ProductDesignation)Enum.Parse(typeof(ProductDesignation), v));

            modelBuilder
            .Entity<FinishedGood>()
            .Property(g => g.ProductDesignation)
            .HasConversion(
            v => v.ToString(),
            v => (ProductDesignation)Enum.Parse(typeof(ProductDesignation), v));
        }
    }
}
