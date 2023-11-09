using Inventory_Management_System.Model.Employee;
using Inventory_Management_System.Model.Location;
using Inventory_Management_System.Model.Good;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_System.Model.HandlingUnit;

namespace Inventory_Management_System
{
    public class InventoryManagementDBContext : DbContext
    {
        public DbSet<RawMaterialLocation> RawMaterialLocations { get; set; }
        public DbSet<OutboundLocation> OutboundLocations { get; set; }
        public DbSet<Box<Component>> ComponentStock {  get; set; }
        public DbSet<Box<FinishedGood>> FinishedGoodStock { get; set; }  

        public InventoryManagementDBContext(DbContextOptions<InventoryManagementDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // must extend in accordance of new entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId); // Define the primary key
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(50); // Set a maximum length for a property
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName) // Create an index on a column
                .IsUnique();
            // Define relationships here using modelBuilder.Entity().Has... methods.
        }
    }
}
