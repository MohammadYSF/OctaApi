﻿// <auto-generated />
using System;
using Command.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Command.Persistence.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Command.Core.Domain.Customer.CustomerAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Vehicles")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Vehicles");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Command.Core.Domain.InventoryItem.InventoryItemَAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BuyPriceHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("CountLowerBound")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SellPriceHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("Command.Core.Domain.Invoice.BuyInvoiceAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("BuyInvoice", (string)null);
                });

            modelBuilder.Entity("Command.Core.Domain.SellInvoice.SellInvoiceAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Customer")
                        .HasColumnType("uuid");

                    b.Property<bool>("UseBuyPrice")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("Vehicle")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SellInvoices");
                });

            modelBuilder.Entity("Command.Core.Domain.Service.ServiceAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DefaultPricecHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Command.Core.Domain.Vehicle.VehicleAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Vehicle", (string)null);
                });

            modelBuilder.Entity("Command.Core.Domain.Customer.CustomerAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.Customer.ValueObjects.CustomerCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("CustomerAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("CustomerAggregateId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Customer.ValueObjects.CustomerFirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("CustomerAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("FirstName");

                            b1.HasKey("CustomerAggregateId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Customer.ValueObjects.CustomerLastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("CustomerAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("LastName");

                            b1.HasKey("CustomerAggregateId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Customer.ValueObjects.CustomerPhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("CustomerAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("CustomerAggregateId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerAggregateId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Command.Core.Domain.InventoryItem.InventoryItemَAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.Core.Price", "BuyPrice", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemَAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("BuyPrice");

                            b1.HasKey("InventoryItemَAggregateId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemَAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Core.Price", "SellPrice", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemَAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("SellPrice");

                            b1.HasKey("InventoryItemَAggregateId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemَAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.InventoryItem.ValueObjects.InventoryItemCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemَAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("InventoryItemَAggregateId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemَAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.InventoryItem.ValueObjects.InventoryItemCount", "Count", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemَAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Value")
                                .HasColumnType("real")
                                .HasColumnName("Count");

                            b1.HasKey("InventoryItemَAggregateId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemَAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.InventoryItem.ValueObjects.InventoryItemName", "Name", b1 =>
                        {
                            b1.Property<Guid>("InventoryItemَAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("InventoryItemَAggregateId");

                            b1.ToTable("InventoryItems");

                            b1.WithOwner()
                                .HasForeignKey("InventoryItemَAggregateId");
                        });

                    b.Navigation("BuyPrice")
                        .IsRequired();

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("Count")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("SellPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("Command.Core.Domain.Invoice.BuyInvoiceAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.BuyInvoice.ValueObjects.BuyInvoiceBuyDate", "BuyDate", b1 =>
                        {
                            b1.Property<Guid>("BuyInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("BuyDate");

                            b1.HasKey("BuyInvoiceAggregateId");

                            b1.ToTable("BuyInvoice");

                            b1.WithOwner()
                                .HasForeignKey("BuyInvoiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.BuyInvoice.ValueObjects.BuyInvoiceCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("BuyInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("BuyInvoiceAggregateId");

                            b1.ToTable("BuyInvoice");

                            b1.WithOwner()
                                .HasForeignKey("BuyInvoiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.BuyInvoice.ValueObjects.BuyInvoiceSellerName", "SellerName", b1 =>
                        {
                            b1.Property<Guid>("BuyInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("SellerName");

                            b1.HasKey("BuyInvoiceAggregateId");

                            b1.ToTable("BuyInvoice");

                            b1.WithOwner()
                                .HasForeignKey("BuyInvoiceAggregateId");
                        });

                    b.OwnsMany("Command.Core.Domain.BuyInvoice.Entities.BuyInvoiceInventoryItem", "InventoryItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("BuyInvoiceId")
                                .HasColumnType("uuid");

                            b1.Property<float>("BuyPrice")
                                .HasColumnType("real");

                            b1.Property<float>("Count")
                                .HasColumnType("real");

                            b1.Property<Guid>("InventoryItemId")
                                .HasColumnType("uuid");

                            b1.Property<float>("SellPrice")
                                .HasColumnType("real");

                            b1.HasKey("Id", "BuyInvoiceId");

                            b1.HasIndex("BuyInvoiceId");

                            b1.ToTable("BuyInvoicecInventoryItems", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("BuyInvoiceId");
                        });

                    b.Navigation("BuyDate")
                        .IsRequired();

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("InventoryItems");

                    b.Navigation("SellerName")
                        .IsRequired();
                });

            modelBuilder.Entity("Command.Core.Domain.SellInvoice.SellInvoiceAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.Core.Price", "Discount", b1 =>
                        {
                            b1.Property<Guid>("SellInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("Discount");

                            b1.HasKey("SellInvoiceAggregateId");

                            b1.ToTable("SellInvoices");

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoiceCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("SellInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("SellInvoiceAggregateId");

                            b1.ToTable("SellInvoices");

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoiceSellDate", "CreateDate", b1 =>
                        {
                            b1.Property<Guid>("SellInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp without time zone")
                                .HasColumnName("CreateDate");

                            b1.HasKey("SellInvoiceAggregateId");

                            b1.ToTable("SellInvoices");

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoicecDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("SellInvoiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Description");

                            b1.HasKey("SellInvoiceAggregateId");

                            b1.ToTable("SellInvoices");

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceAggregateId");
                        });

                    b.OwnsMany("Command.Core.Domain.SellInvoice.Entities.SellInvoiceInventoryItem", "InventoryItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("SellInvoiceId")
                                .HasColumnType("uuid");

                            b1.Property<float>("Count")
                                .HasColumnType("real")
                                .HasColumnName("Count");

                            b1.Property<Guid>("InventoryItemId")
                                .HasColumnType("uuid")
                                .HasColumnName("InventoryItemId");

                            b1.HasKey("Id", "SellInvoiceId");

                            b1.HasIndex("SellInvoiceId");

                            b1.ToTable("SellInvoicecInventoryItems", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceId");
                        });

                    b.OwnsMany("Command.Core.Domain.SellInvoice.Entities.SellInvoicePayment", "Payments", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("SellInvoiceId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id", "SellInvoiceId");

                            b1.HasIndex("SellInvoiceId");

                            b1.ToTable("SellInvoicePayments", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceId");

                            b1.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoicePaidAmount", "PaidAmount", b2 =>
                                {
                                    b2.Property<Guid>("SellInvoicePaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("SellInvoicePaymentSellInvoiceId")
                                        .HasColumnType("uuid");

                                    b2.Property<long>("Value")
                                        .HasColumnType("bigint")
                                        .HasColumnName("PaidAmount");

                                    b2.HasKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");

                                    b2.ToTable("SellInvoicePayments");

                                    b2.WithOwner()
                                        .HasForeignKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");
                                });

                            b1.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoicePaymentDate", "PaidDate", b2 =>
                                {
                                    b2.Property<Guid>("SellInvoicePaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("SellInvoicePaymentSellInvoiceId")
                                        .HasColumnType("uuid");

                                    b2.Property<DateTime>("Value")
                                        .HasColumnType("timestamp without time zone")
                                        .HasColumnName("PaidDate");

                                    b2.HasKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");

                                    b2.ToTable("SellInvoicePayments");

                                    b2.WithOwner()
                                        .HasForeignKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");
                                });

                            b1.OwnsOne("Command.Core.Domain.SellInvoice.ValueObjects.SellInvoicePaymentTrackCode", "PaymentTrackCode", b2 =>
                                {
                                    b2.Property<Guid>("SellInvoicePaymentId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("SellInvoicePaymentSellInvoiceId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("PaymentTrackCode");

                                    b2.HasKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");

                                    b2.ToTable("SellInvoicePayments");

                                    b2.WithOwner()
                                        .HasForeignKey("SellInvoicePaymentId", "SellInvoicePaymentSellInvoiceId");
                                });

                            b1.Navigation("PaidAmount")
                                .IsRequired();

                            b1.Navigation("PaidDate")
                                .IsRequired();

                            b1.Navigation("PaymentTrackCode")
                                .IsRequired();
                        });

                    b.OwnsMany("Command.Core.Domain.SellInvoice.Entities.SellInvoiceService", "Services", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("SellInvoiceId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("ServiceId")
                                .HasColumnType("uuid")
                                .HasColumnName("ServiceId");

                            b1.HasKey("Id", "SellInvoiceId");

                            b1.HasIndex("SellInvoiceId");

                            b1.ToTable("SellInvoiceServices", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SellInvoiceId");

                            b1.OwnsOne("Command.Core.Domain.Core.Price", "ServicePrice", b2 =>
                                {
                                    b2.Property<Guid>("SellInvoiceServiceId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("SellInvoiceServiceSellInvoiceId")
                                        .HasColumnType("uuid");

                                    b2.Property<long>("Value")
                                        .HasColumnType("bigint")
                                        .HasColumnName("ServicePrice");

                                    b2.HasKey("SellInvoiceServiceId", "SellInvoiceServiceSellInvoiceId");

                                    b2.ToTable("SellInvoiceServices");

                                    b2.WithOwner()
                                        .HasForeignKey("SellInvoiceServiceId", "SellInvoiceServiceSellInvoiceId");
                                });

                            b1.Navigation("ServicePrice")
                                .IsRequired();
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("CreateDate")
                        .IsRequired();

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Discount")
                        .IsRequired();

                    b.Navigation("InventoryItems");

                    b.Navigation("Payments");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Command.Core.Domain.Service.ServiceAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.Core.Price", "DefaultPrice", b1 =>
                        {
                            b1.Property<Guid>("ServiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("DefaultPrice");

                            b1.HasKey("ServiceAggregateId");

                            b1.ToTable("Services");

                            b1.WithOwner()
                                .HasForeignKey("ServiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Service.ValueObjects.ServiceCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("ServiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("ServiceAggregateId");

                            b1.ToTable("Services");

                            b1.WithOwner()
                                .HasForeignKey("ServiceAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Service.ValueObjects.ServiceName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ServiceAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("ServiceAggregateId");

                            b1.ToTable("Services");

                            b1.WithOwner()
                                .HasForeignKey("ServiceAggregateId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("DefaultPrice")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Command.Core.Domain.Vehicle.VehicleAggregate", b =>
                {
                    b.OwnsOne("Command.Core.Domain.Vehicle.ValueObjects.VehicleCode", "Code", b1 =>
                        {
                            b1.Property<Guid>("VehicleAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Value")
                                .HasMaxLength(255)
                                .HasColumnType("integer")
                                .HasColumnName("Code");

                            b1.HasKey("VehicleAggregateId");

                            b1.ToTable("Vehicle");

                            b1.WithOwner()
                                .HasForeignKey("VehicleAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Vehicle.ValueObjects.VehicleColor", "Color", b1 =>
                        {
                            b1.Property<Guid>("VehicleAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Color");

                            b1.HasKey("VehicleAggregateId");

                            b1.ToTable("Vehicle");

                            b1.WithOwner()
                                .HasForeignKey("VehicleAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Vehicle.ValueObjects.VehicleName", "Name", b1 =>
                        {
                            b1.Property<Guid>("VehicleAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("Name");

                            b1.HasKey("VehicleAggregateId");

                            b1.ToTable("Vehicle");

                            b1.WithOwner()
                                .HasForeignKey("VehicleAggregateId");
                        });

                    b.OwnsOne("Command.Core.Domain.Vehicle.ValueObjects.VehiclePlate", "Plate", b1 =>
                        {
                            b1.Property<Guid>("VehicleAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("character varying(255)")
                                .HasColumnName("Plate");

                            b1.HasKey("VehicleAggregateId");

                            b1.ToTable("Vehicle");

                            b1.WithOwner()
                                .HasForeignKey("VehicleAggregateId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("Color")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Plate")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
