﻿// <auto-generated />
using System;
using ManoExperta.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManoExperta.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231120042400_AddCategoryCode")]
    partial class AddCategoryCode
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ManoExperta.API.Domain.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ProfessionalProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalProfileId");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.PhoneNumber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfessionalProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalProfileId");

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.ProfessionalCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.ToTable("ProfessionalCategories");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.ProfessionalProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ProfessionalProfiles");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.WorkingHours", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("End")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProfessionalProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Start")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalProfileId");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("ProfessionalCategoryProfessionalProfile", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessionalsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "ProfessionalsId");

                    b.HasIndex("ProfessionalsId");

                    b.ToTable("ProfessionalCategoryProfessionalProfile");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.Email", b =>
                {
                    b.HasOne("ManoExperta.API.Domain.ProfessionalProfile", null)
                        .WithMany("Email")
                        .HasForeignKey("ProfessionalProfileId");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.PhoneNumber", b =>
                {
                    b.HasOne("ManoExperta.API.Domain.ProfessionalProfile", null)
                        .WithMany("PhoneNumber")
                        .HasForeignKey("ProfessionalProfileId");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.ProfessionalProfile", b =>
                {
                    b.HasOne("ManoExperta.API.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ManoExperta.API.Domain.WorkingHours", b =>
                {
                    b.HasOne("ManoExperta.API.Domain.ProfessionalProfile", null)
                        .WithMany("WorkingHours")
                        .HasForeignKey("ProfessionalProfileId");
                });

            modelBuilder.Entity("ProfessionalCategoryProfessionalProfile", b =>
                {
                    b.HasOne("ManoExperta.API.Domain.ProfessionalCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManoExperta.API.Domain.ProfessionalProfile", null)
                        .WithMany()
                        .HasForeignKey("ProfessionalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManoExperta.API.Domain.ProfessionalProfile", b =>
                {
                    b.Navigation("Email");

                    b.Navigation("PhoneNumber");

                    b.Navigation("WorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
