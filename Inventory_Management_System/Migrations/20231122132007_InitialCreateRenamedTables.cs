﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateRenamedTables : Migration
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
                name: "ComponentLocations",
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
                    table.PrimaryKey("PK_ComponentLocations", x => x.Id);
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
                name: "FinishedGoodLocations",
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
                    table.PrimaryKey("PK_FinishedGoodLocations", x => x.Id);
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
                    ComponentLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentStock_ComponentLocations_ComponentLocationId",
                        column: x => x.ComponentLocationId,
                        principalTable: "ComponentLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentStock_Component_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Component",
                        principalColumn: "Id");
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
                    FinishedGoodLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishedGoodStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishedGoodStock_FinishedGoodLocations_FinishedGoodLocationId",
                        column: x => x.FinishedGoodLocationId,
                        principalTable: "FinishedGoodLocations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinishedGoodStock_FinishedGood_GoodId",
                        column: x => x.GoodId,
                        principalTable: "FinishedGood",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStock_ComponentLocationId",
                table: "ComponentStock",
                column: "ComponentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentStock_GoodId",
                table: "ComponentStock",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedGoodStock_FinishedGoodLocationId",
                table: "FinishedGoodStock",
                column: "FinishedGoodLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishedGoodStock_GoodId",
                table: "FinishedGoodStock",
                column: "GoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentStock");

            migrationBuilder.DropTable(
                name: "FinishedGoodStock");

            migrationBuilder.DropTable(
                name: "ComponentLocations");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "FinishedGoodLocations");

            migrationBuilder.DropTable(
                name: "FinishedGood");
        }
    }
}