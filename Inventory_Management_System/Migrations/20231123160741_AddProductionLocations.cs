using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductionLocationId",
                table: "FinishedGoodStock",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductionLocationId",
                table: "ComponentStock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductionLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishedGoodStock_ProductionLocationId",
                table: "FinishedGoodStock",
                column: "ProductionLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStock_ProductionLocationId",
                table: "ComponentStock",
                column: "ProductionLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentStock_ProductionLocations_ProductionLocationId",
                table: "ComponentStock",
                column: "ProductionLocationId",
                principalTable: "ProductionLocations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedGoodStock_ProductionLocations_ProductionLocationId",
                table: "FinishedGoodStock",
                column: "ProductionLocationId",
                principalTable: "ProductionLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentStock_ProductionLocations_ProductionLocationId",
                table: "ComponentStock");

            migrationBuilder.DropForeignKey(
                name: "FK_FinishedGoodStock_ProductionLocations_ProductionLocationId",
                table: "FinishedGoodStock");

            migrationBuilder.DropTable(
                name: "ProductionLocations");

            migrationBuilder.DropIndex(
                name: "IX_FinishedGoodStock_ProductionLocationId",
                table: "FinishedGoodStock");

            migrationBuilder.DropIndex(
                name: "IX_ComponentStock_ProductionLocationId",
                table: "ComponentStock");

            migrationBuilder.DropColumn(
                name: "ProductionLocationId",
                table: "FinishedGoodStock");

            migrationBuilder.DropColumn(
                name: "ProductionLocationId",
                table: "ComponentStock");
        }
    }
}
