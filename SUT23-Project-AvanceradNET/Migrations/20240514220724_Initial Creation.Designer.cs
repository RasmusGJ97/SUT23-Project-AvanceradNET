﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SUT23_Project_AvanceradNET.Data;

#nullable disable

namespace SUT23_Project_AvanceradNET.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20240514220724_Initial Creation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AppointmentStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.ChangeLog", b =>
                {
                    b.Property<int>("ChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChangeId"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ChangedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NewEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NewStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OldEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OldStart")
                        .HasColumnType("datetime2");

                    b.HasKey("ChangeId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CompanyPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Companys");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            CompanyName = "Employee1",
                            CompanyPassword = "Employee1"
                        },
                        new
                        {
                            CompanyId = 2,
                            CompanyName = "Employee2",
                            CompanyPassword = "Employee2"
                        });
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerFName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("CustomerLName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            CustomerEmail = "j.persson@hotmail.com",
                            CustomerFName = "John",
                            CustomerLName = "Persson",
                            CustomerPhone = "1234567890"
                        },
                        new
                        {
                            CustomerId = 2,
                            CustomerEmail = "f.johansson@hotmail.com",
                            CustomerFName = "Fredrik",
                            CustomerLName = "Johansson",
                            CustomerPhone = "0987654321"
                        },
                        new
                        {
                            CustomerId = 3,
                            CustomerEmail = "s.andersson@hotmail.com",
                            CustomerFName = "Sara",
                            CustomerLName = "Andersson",
                            CustomerPhone = "6543214567"
                        },
                        new
                        {
                            CustomerId = 4,
                            CustomerEmail = "g.svensson@hotmail.com",
                            CustomerFName = "Göran",
                            CustomerLName = "Svensson",
                            CustomerPhone = "3098632789"
                        },
                        new
                        {
                            CustomerId = 5,
                            CustomerEmail = "a.lindblad@hotmail.com",
                            CustomerFName = "Anna",
                            CustomerLName = "Lindblad",
                            CustomerPhone = "781234590"
                        });
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Appointment", b =>
                {
                    b.HasOne("ClassLibrary_Project_AvanceradNET.Models.Customer", "Customer")
                        .WithMany("Appointment")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.ChangeLog", b =>
                {
                    b.HasOne("ClassLibrary_Project_AvanceradNET.Models.Appointment", "Appointment")
                        .WithMany("ChangeLogs")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Appointment", b =>
                {
                    b.Navigation("ChangeLogs");
                });

            modelBuilder.Entity("ClassLibrary_Project_AvanceradNET.Models.Customer", b =>
                {
                    b.Navigation("Appointment");
                });
#pragma warning restore 612, 618
        }
    }
}
