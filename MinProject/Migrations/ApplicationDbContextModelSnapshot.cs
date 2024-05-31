﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinProject.Data;

#nullable disable

namespace MinProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MinProject.Models.City", b =>
                {
                    b.Property<Guid>("CityCodePk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CityCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityCode"));

                    b.Property<Guid>("StateCodeFk")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CityCodePk");

                    b.HasIndex("StateCodeFk")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("MinProject.Models.Country", b =>
                {
                    b.Property<Guid>("CountryCodePk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryCodePk");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MinProject.Models.Notifaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("MinProject.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MinProject.Models.Products", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MinProject.Models.State", b =>
                {
                    b.Property<Guid>("StateCodePk")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryCodeFk")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StateCodePk");

                    b.HasIndex("CountryCodeFk")
                        .IsUnique();

                    b.ToTable("States");
                });

            modelBuilder.Entity("MinProject.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MinProject.Models.City", b =>
                {
                    b.HasOne("MinProject.Models.State", "State")
                        .WithOne("City")
                        .HasForeignKey("MinProject.Models.City", "StateCodeFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("MinProject.Models.Notifaction", b =>
                {
                    b.HasOne("MinProject.Models.User", "User")
                        .WithOne("Notifaction")
                        .HasForeignKey("MinProject.Models.Notifaction", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MinProject.Models.Order", b =>
                {
                    b.HasOne("MinProject.Models.Products", "Products")
                        .WithOne("Order")
                        .HasForeignKey("MinProject.Models.Order", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinProject.Models.User", "User")
                        .WithOne("Order")
                        .HasForeignKey("MinProject.Models.Order", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MinProject.Models.State", b =>
                {
                    b.HasOne("MinProject.Models.Country", "Country")
                        .WithOne("State")
                        .HasForeignKey("MinProject.Models.State", "CountryCodeFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MinProject.Models.Country", b =>
                {
                    b.Navigation("State");
                });

            modelBuilder.Entity("MinProject.Models.Products", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("MinProject.Models.State", b =>
                {
                    b.Navigation("City");
                });

            modelBuilder.Entity("MinProject.Models.User", b =>
                {
                    b.Navigation("Notifaction");

                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
