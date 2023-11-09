using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_System.Migrations.InventoryManagementDB
{
    /// <inheritdoc />
    public partial class boxColumnAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocatedAt",
                table: "FinishedGoodStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocatedAt",
                table: "ComponentStock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocatedAt",
                table: "FinishedGoodStock");

            migrationBuilder.DropColumn(
                name: "LocatedAt",
                table: "ComponentStock");
        }
    }
}
