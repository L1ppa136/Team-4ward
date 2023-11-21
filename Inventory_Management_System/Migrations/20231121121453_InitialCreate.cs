using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishedGood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedGood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboundLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    MaxBoxCapacity = table.Column<int>(type: "int", nullable: false),
                    Full = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RawMaterialLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    MaxBoxCapacity = table.Column<int>(type: "int", nullable: false),
                    Full = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterialLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinishedGoodStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    OutboundLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedGoodStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedGoodStock_FinishedGood_GoodId",
                        column: x => x.GoodId,
                        principalTable: "FinishedGood",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinishedGoodStock_OutboundLocations_OutboundLocationId",
                        column: x => x.OutboundLocationId,
                        principalTable: "OutboundLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComponentStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    RawMaterialLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentStock_Component_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Component",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentStock_RawMaterialLocations_RawMaterialLocationId",
                        column: x => x.RawMaterialLocationId,
                        principalTable: "RawMaterialLocations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStock_GoodId",
                table: "ComponentStock",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStock_RawMaterialLocationId",
                table: "ComponentStock",
                column: "RawMaterialLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedGoodStock_GoodId",
                table: "FinishedGoodStock",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedGoodStock_OutboundLocationId",
                table: "FinishedGoodStock",
                column: "OutboundLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentStock");

            migrationBuilder.DropTable(
                name: "FinishedGoodStock");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "RawMaterialLocations");

            migrationBuilder.DropTable(
                name: "FinishedGood");

            migrationBuilder.DropTable(
                name: "OutboundLocations");
        }
    }
}
