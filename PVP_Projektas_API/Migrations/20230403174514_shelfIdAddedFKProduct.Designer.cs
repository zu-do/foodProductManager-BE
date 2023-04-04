﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PVP_Projektas_API.Data;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20230403174514_shelfIdAddedFKProduct")]
    partial class shelfIdAddedFKProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PVP_Projektas_API.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(8,6)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(9,6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DbAddresses");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DbAdmins");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Category", b =>
                {
                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoryName");

                    b.ToTable("DbCategories");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.GiveawaySpot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ClosingHours")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OpeningHours")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DbGiveawaySpots");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShelfId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryName");

                    b.HasIndex("ShelfId");

                    b.ToTable("DbProducts");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DbShelves");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("DbTrades");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DbUsers");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Address", b =>
                {
                    b.HasOne("PVP_Projektas_API.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Product", b =>
                {
                    b.HasOne("PVP_Projektas_API.Models.Category", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryName");

                    b.HasOne("PVP_Projektas_API.Models.Shelf", "ProductShelf")
                        .WithMany("Products")
                        .HasForeignKey("ShelfId");

                    b.Navigation("ProductCategory");

                    b.Navigation("ProductShelf");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Shelf", b =>
                {
                    b.HasOne("PVP_Projektas_API.Models.User", "User")
                        .WithMany("Shelves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.Shelf", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("PVP_Projektas_API.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Shelves");
                });
#pragma warning restore 612, 618
        }
    }
}
