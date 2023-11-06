using Inventory_Management_System.Model;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System
{
    public class InventoryManagementDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public InventoryManagementDBContext(DbContextOptions<InventoryManagementDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

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
