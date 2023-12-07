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
        public DbSet<ComponentLocation> ComponentLocations { get; set; }
        public DbSet<FinishedGoodLocation> FinishedGoodLocations { get; set; }
        public DbSet<Box<Component>> ComponentStock { get; set; }
        public DbSet<ProductionLocation> ProductionLocations { get; set; }
        public DbSet<Box<FinishedGood>> FinishedGoodStock { get; set; }

        public InventoryManagementDBContext(DbContextOptions<InventoryManagementDBContext> options) : base(options)
        {
        }

        // must extend in accordance of new entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<FinishedGood>()
            //.Property(f => f.BuildOfMaterial)
            //.IsRequired()
            //.HasConversion(
            //    bom => JsonConvert.SerializeObject(bom),
            //    jsonBom => jsonBom == null
            //        ? new Dictionary<Component, int>()
            //        : JsonConvert.DeserializeObject<Dictionary<Component, int>>(jsonBom)
            //);
            //MUST ADD TO ALL IMPLEMENTATIONS OF "GOOD" ABSTRACT CLASS

            modelBuilder
            .Entity<Component>()
            .Property(g => g.ProductDesignation)
            .HasConversion(
            enumType => enumType.ToString(),
            str => Enum.Parse<ProductDesignation>(str)); // new, generic syntax

            modelBuilder
            .Entity<FinishedGood>()
            .Property(g => g.ProductDesignation)
            .HasConversion(
            enumType => enumType.ToString(),
            str => (ProductDesignation)Enum.Parse(typeof(ProductDesignation), str)); //"old" syntax
        }
    }
}
