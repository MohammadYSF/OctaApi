﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Query.Persistence.Contexts;

#nullable disable

namespace Query.Persistence.Migrations
{
    [DbContext(typeof(QueryDbContext))]
    [Migration("20240128094309_updating readmodels")]
    partial class updatingreadmodels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OctaShared.ReadModels.BuyInvoiceRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BuyInvoiceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BuyInvoiceCreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("BuyInvoiceId")
                        .HasColumnType("uuid");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BuyInvoiceRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.CustomerRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerCode")
                        .HasColumnType("text");

                    b.Property<string>("CustomerFirstName")
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerLastName")
                        .HasColumnType("text");

                    b.Property<string>("CustomerPhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CustomerRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.CustomerVehicleRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("VehicleCode")
                        .HasColumnType("text");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.Property<string>("VehicleName")
                        .HasColumnType("text");

                    b.Property<string>("VehiclePlate")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CustomerVehicleRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.InventoryItemRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("InventoryItemBuyPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("InventoryItemCode")
                        .HasColumnType("text");

                    b.Property<float>("InventoryItemCount")
                        .HasColumnType("real");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("InventoryItemName")
                        .HasColumnType("text");

                    b.Property<long>("InventoryItemSellPrice")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("InventoryItemRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.SellInvoiceDescriptionRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Kilometer1")
                        .HasColumnType("bigint");

                    b.Property<long>("Kilometer2")
                        .HasColumnType("bigint");

                    b.Property<long>("Kilometer3")
                        .HasColumnType("bigint");

                    b.Property<long>("Kilometer4")
                        .HasColumnType("bigint");

                    b.Property<Guid>("SellInvoiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SellInvoiceDescriptionRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.SellInvoiceInventoryItemRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("BuyPrice")
                        .HasColumnType("bigint");

                    b.Property<float>("Count")
                        .HasColumnType("real");

                    b.Property<string>("InventoryItemCode")
                        .HasColumnType("text");

                    b.Property<Guid>("InventoryItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("InventoryItemName")
                        .HasColumnType("text");

                    b.Property<Guid>("SellInvoiceId")
                        .HasColumnType("uuid");

                    b.Property<long>("SellPrice")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("SellInvoiceInventoryItemRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.SellInvoicePaymentRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("PaidAmount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("SellInvoiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SellInvoicePaymentRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.SellInvoiceRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerCode")
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .HasColumnType("text");

                    b.Property<long>("Discount")
                        .HasColumnType("bigint");

                    b.Property<string>("SellInvoiceCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("SellInvoiceDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SellInvoiceId")
                        .HasColumnType("uuid");

                    b.Property<long>("Tax")
                        .HasColumnType("bigint");

                    b.Property<long>("ToPay")
                        .HasColumnType("bigint");

                    b.Property<long>("ToPayWhenUsingBuyPrices")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalPriceWhenUsingBuyPrices")
                        .HasColumnType("bigint");

                    b.Property<bool>("UseBuyPrice")
                        .HasColumnType("boolean");

                    b.Property<string>("VehicleCode")
                        .HasColumnType("text");

                    b.Property<string>("VehicleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SellInvoiceRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.SellInvoiceServiceRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("DefaultPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid>("SellInvoiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceCode")
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("SellInvoiceServiceRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.ServiceRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ServiceCode")
                        .HasColumnType("text");

                    b.Property<long>("ServiceDefaultPrice")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ServiceName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ServiceRMs");
                });

            modelBuilder.Entity("OctaShared.ReadModels.VehicleRM", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VehicleCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VehicleRMs");
                });
#pragma warning restore 612, 618
        }
    }
}
