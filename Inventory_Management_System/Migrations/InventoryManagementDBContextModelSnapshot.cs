﻿// <auto-generated />
using System;
using Inventory_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inventory_Management_System.Migrations
{
    [DbContext(typeof(InventoryManagementDBContext))]
    partial class InventoryManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Inventory_Management_System.Model.Good.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoxCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.Property<string>("ProductDesignation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Component");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Good.FinishedGood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoxCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.Property<string>("ProductDesignation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinishedGood");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.HandlingUnit.Box<Inventory_Management_System.Model.Good.Component>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("ComponentLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GoodId")
                        .HasColumnType("int");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ProductionLocationId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentLocationId");

                    b.HasIndex("GoodId");

                    b.HasIndex("ProductionLocationId");

                    b.ToTable("ComponentStock");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.HandlingUnit.Box<Inventory_Management_System.Model.Good.FinishedGood>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FinishedGoodLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("GoodId")
                        .HasColumnType("int");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ProductionLocationId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FinishedGoodLocationId");

                    b.HasIndex("GoodId");

                    b.HasIndex("ProductionLocationId");

                    b.ToTable("FinishedGoodStock");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.ComponentLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Full")
                        .HasColumnType("bit");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.Property<int>("MaxBoxCapacity")
                        .HasColumnType("int");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ComponentLocations");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.FinishedGoodLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Full")
                        .HasColumnType("bit");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.Property<int>("MaxBoxCapacity")
                        .HasColumnType("int");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FinishedGoodLocations");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.ProductionLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartNumber")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductionLocations");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.HandlingUnit.Box<Inventory_Management_System.Model.Good.Component>", b =>
                {
                    b.HasOne("Inventory_Management_System.Model.Location.ComponentLocation", null)
                        .WithMany("Boxes")
                        .HasForeignKey("ComponentLocationId");

                    b.HasOne("Inventory_Management_System.Model.Good.Component", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");

                    b.HasOne("Inventory_Management_System.Model.Location.ProductionLocation", null)
                        .WithMany("Components")
                        .HasForeignKey("ProductionLocationId");

                    b.Navigation("Good");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.HandlingUnit.Box<Inventory_Management_System.Model.Good.FinishedGood>", b =>
                {
                    b.HasOne("Inventory_Management_System.Model.Location.FinishedGoodLocation", null)
                        .WithMany("Boxes")
                        .HasForeignKey("FinishedGoodLocationId");

                    b.HasOne("Inventory_Management_System.Model.Good.FinishedGood", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId");

                    b.HasOne("Inventory_Management_System.Model.Location.ProductionLocation", null)
                        .WithMany("FinishedGoods")
                        .HasForeignKey("ProductionLocationId");

                    b.Navigation("Good");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.ComponentLocation", b =>
                {
                    b.Navigation("Boxes");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.FinishedGoodLocation", b =>
                {
                    b.Navigation("Boxes");
                });

            modelBuilder.Entity("Inventory_Management_System.Model.Location.ProductionLocation", b =>
                {
                    b.Navigation("Components");

                    b.Navigation("FinishedGoods");
                });
#pragma warning restore 612, 618
        }
    }
}
