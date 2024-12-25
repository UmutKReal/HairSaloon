﻿// <auto-generated />
using System;
using BarberSaloon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BarberSaloon.Migrations
{
    [DbContext(typeof(BarberSaloonDBContext))]
    partial class BarberSaloonDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BarberSaloon.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("AppointmentID");

                    b.HasIndex("CustomerID")
                        .IsUnique();

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("BarberSaloon.Models.AppointmentDateTime", b =>
                {
                    b.Property<int>("AppointmentDTID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentDTID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AppointmentID")
                        .HasColumnType("int");

                    b.Property<int>("ServiceID")
                        .HasColumnType("int");

                    b.HasKey("AppointmentDTID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("ServiceID");

                    b.ToTable("AppointmentDateTimes");
                });

            modelBuilder.Entity("BarberSaloon.Models.AppointmentDateTimeEmployee", b =>
                {
                    b.Property<int>("AppointmentDTID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentDTID"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("ServiceID")
                        .HasColumnType("int");

                    b.HasKey("AppointmentDTID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ServiceID");

                    b.ToTable("appointmentDateTimeEmployees");
                });

            modelBuilder.Entity("BarberSaloon.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerID = 1,
                            Email = "a@gmail.com",
                            Gender = "Erkek",
                            Name = "Mehmet",
                            Password = "1",
                            PhoneNumber = "5551234567",
                            Surname = "Kara"
                        });
                });

            modelBuilder.Entity("BarberSaloon.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            Gender = "Erkek",
                            Name = "Ahmet",
                            Surname = "Yılmaz"
                        },
                        new
                        {
                            EmployeeID = 2,
                            Gender = "Erkek",
                            Name = "Veysel",
                            Surname = "Aras"
                        },
                        new
                        {
                            EmployeeID = 3,
                            Gender = "Kadın",
                            Name = "Ayşe",
                            Surname = "Demir"
                        });
                });

            modelBuilder.Entity("BarberSaloon.Models.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceID"));

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("ServiceDuration")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ServiceID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ServiceID = 1,
                            EmployeeID = 1,
                            ServiceDuration = 30,
                            ServiceName = "Sakal Tıraşı",
                            ServicePrice = 20.00m
                        },
                        new
                        {
                            ServiceID = 2,
                            EmployeeID = 1,
                            ServiceDuration = 60,
                            ServiceName = "Saç Kesimi",
                            ServicePrice = 50.00m
                        },
                        new
                        {
                            ServiceID = 3,
                            EmployeeID = 2,
                            ServiceDuration = 60,
                            ServiceName = "Renk",
                            ServicePrice = 70.00m
                        });
                });

            modelBuilder.Entity("BarberSaloon.Models.Appointment", b =>
                {
                    b.HasOne("BarberSaloon.Models.Customer", "Customer")
                        .WithOne("Appointment")
                        .HasForeignKey("BarberSaloon.Models.Appointment", "CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BarberSaloon.Models.AppointmentDateTime", b =>
                {
                    b.HasOne("BarberSaloon.Models.Appointment", "Appointment")
                        .WithMany("AppointmentDateTimes")
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BarberSaloon.Models.Service", "Service")
                        .WithMany("AppointmentDateTimes")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BarberSaloon.Models.AppointmentDateTimeEmployee", b =>
                {
                    b.HasOne("BarberSaloon.Models.Employee", "Employee")
                        .WithMany("AppointmentDateTimes")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberSaloon.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("BarberSaloon.Models.Service", b =>
                {
                    b.HasOne("BarberSaloon.Models.Employee", "Employee")
                        .WithMany("Services")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BarberSaloon.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentDateTimes");
                });

            modelBuilder.Entity("BarberSaloon.Models.Customer", b =>
                {
                    b.Navigation("Appointment")
                        .IsRequired();
                });

            modelBuilder.Entity("BarberSaloon.Models.Employee", b =>
                {
                    b.Navigation("AppointmentDateTimes");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BarberSaloon.Models.Service", b =>
                {
                    b.Navigation("AppointmentDateTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
