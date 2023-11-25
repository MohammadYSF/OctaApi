﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OctaApi.Persistence.Contexts;

#nullable disable

namespace OctaApi.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OAS.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("245db4b9-4aed-43e5-a02a-001202523e86"),
                            Code = 12345678,
                            FirstName = "مشتری متفرقه",
                            IsActive = true,
                            LastName = "",
                            PhoneNumber = "00000000000",
                            RegisterDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("OAS.Domain.Models.CustomerHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerHistory", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.DescriptionItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("DescriptionItem", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.InventoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("BuyPrice")
                        .HasColumnType("bigint");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<float?>("Count")
                        .HasColumnType("real");

                    b.Property<float?>("CountLowerBound")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("SellPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("InventoryItem", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.InventoryItemHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("BuyPrice")
                        .HasColumnType("bigint");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<float?>("Count")
                        .HasColumnType("real");

                    b.Property<float?>("CountLowerBound")
                        .HasColumnType("real");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long?>("SellPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("InventoryItemHistory", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<float?>("DiscountAmount")
                        .HasColumnType("real");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SerllerName")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool?>("UseBuyPrice")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Invoice", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DescriptionItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionItemId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceDescription", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceInventoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("Count")
                        .HasColumnType("real");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceInventoryItem", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoicePayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastPaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("PaidAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("TrackCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoicePayments");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoicePaymentHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoicePaymentId")
                        .HasColumnType("uuid");

                    b.Property<long>("PaidAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("TrackCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("InvoicePaymentId");

                    b.ToTable("InvoicePaymentHistories");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("InvoiceServiceItem", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<long>("DefaultPrice")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.ServiceHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("DefaultPrice")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceHistory", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Vehicle", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.VehicleHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Code")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleHistory", (string)null);
                });

            modelBuilder.Entity("OAS.Domain.Models.CustomerHistory", b =>
                {
                    b.HasOne("OAS.Domain.Models.Customer", "Customer")
                        .WithMany("CustomerHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OAS.Domain.Models.InventoryItemHistory", b =>
                {
                    b.HasOne("OAS.Domain.Models.InventoryItem", "InventoryItem")
                        .WithMany("InventoryItemHistories")
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItem");
                });

            modelBuilder.Entity("OAS.Domain.Models.Invoice", b =>
                {
                    b.HasOne("OAS.Domain.Models.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId");

                    b.HasOne("OAS.Domain.Models.Vehicle", "Vehicle")
                        .WithMany("Invoices")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Customer");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceDescription", b =>
                {
                    b.HasOne("OAS.Domain.Models.DescriptionItem", "DescriptionItem")
                        .WithMany("InvoiceDescriptions")
                        .HasForeignKey("DescriptionItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OAS.Domain.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDescriptions")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DescriptionItem");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceInventoryItem", b =>
                {
                    b.HasOne("OAS.Domain.Models.InventoryItem", "InventoryItem")
                        .WithMany("InvoiceInventoryItems")
                        .HasForeignKey("InventoryItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OAS.Domain.Models.Invoice", "Invoice")
                        .WithMany("InvoiceInventoryItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItem");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoicePayment", b =>
                {
                    b.HasOne("OAS.Domain.Models.Invoice", "Invoice")
                        .WithMany("InvoicePayments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoicePaymentHistory", b =>
                {
                    b.HasOne("OAS.Domain.Models.InvoicePayment", "InvoicePayment")
                        .WithMany("InvoicePaymentHistories")
                        .HasForeignKey("InvoicePaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvoicePayment");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoiceService", b =>
                {
                    b.HasOne("OAS.Domain.Models.Invoice", "Invoice")
                        .WithMany("InvoiceServices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OAS.Domain.Models.Service", "Service")
                        .WithMany("InvoiceServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("OAS.Domain.Models.ServiceHistory", b =>
                {
                    b.HasOne("OAS.Domain.Models.Service", "Service")
                        .WithMany("ServiceHistories")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("OAS.Domain.Models.Vehicle", b =>
                {
                    b.HasOne("OAS.Domain.Models.Customer", "Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OAS.Domain.Models.VehicleHistory", b =>
                {
                    b.HasOne("OAS.Domain.Models.Vehicle", "Vehicle")
                        .WithMany("VehicleHistory")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("OAS.Domain.Models.Customer", b =>
                {
                    b.Navigation("CustomerHistories");

                    b.Navigation("Invoices");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("OAS.Domain.Models.DescriptionItem", b =>
                {
                    b.Navigation("InvoiceDescriptions");
                });

            modelBuilder.Entity("OAS.Domain.Models.InventoryItem", b =>
                {
                    b.Navigation("InventoryItemHistories");

                    b.Navigation("InvoiceInventoryItems");
                });

            modelBuilder.Entity("OAS.Domain.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDescriptions");

                    b.Navigation("InvoiceInventoryItems");

                    b.Navigation("InvoicePayments");

                    b.Navigation("InvoiceServices");
                });

            modelBuilder.Entity("OAS.Domain.Models.InvoicePayment", b =>
                {
                    b.Navigation("InvoicePaymentHistories");
                });

            modelBuilder.Entity("OAS.Domain.Models.Service", b =>
                {
                    b.Navigation("InvoiceServices");

                    b.Navigation("ServiceHistories");
                });

            modelBuilder.Entity("OAS.Domain.Models.Vehicle", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("VehicleHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
