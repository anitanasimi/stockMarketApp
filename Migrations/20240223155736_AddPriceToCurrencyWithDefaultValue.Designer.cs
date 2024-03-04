﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StockMarketWithSignalR.Database;

#nullable disable

namespace StockMarketWithSignalR.Migrations
{
    [DbContext(typeof(StockMarketDb))]
    [Migration("20240223155736_AddPriceToCurrencyWithDefaultValue")]
    partial class AddPriceToCurrencyWithDefaultValue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StockMarketWithSignalR.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Coefficient")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CurrencyCode")
                        .HasColumnType("integer");

                    b.Property<Guid?>("MarketStateId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ae1507ec-d52d-4211-ac02-30eebd569cee"),
                            Coefficient = 1.5m,
                            CreatedAt = new DateTime(2024, 2, 23, 15, 57, 35, 726, DateTimeKind.Utc).AddTicks(8816),
                            CurrencyCode = 1,
                            Price = 60000m,
                            Title = "BitCoin"
                        },
                        new
                        {
                            Id = new Guid("699bb0f8-9b10-4f5e-a98c-39498067edb6"),
                            Coefficient = 1.1m,
                            CreatedAt = new DateTime(2024, 2, 23, 15, 57, 35, 726, DateTimeKind.Utc).AddTicks(8868),
                            CurrencyCode = 2,
                            Price = 20m,
                            Title = "DogeCoin"
                        });
                });

            modelBuilder.Entity("StockMarketWithSignalR.Entities.MarketState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId")
                        .IsUnique();

                    b.ToTable("MarketStates");
                });

            modelBuilder.Entity("StockMarketWithSignalR.Entities.MarketStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<int>("OperationType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("MarketStatistics");
                });

            modelBuilder.Entity("StockMarketWithSignalR.Entities.MarketState", b =>
                {
                    b.HasOne("StockMarketWithSignalR.Entities.Currency", "Currency")
                        .WithOne("MarketState")
                        .HasForeignKey("StockMarketWithSignalR.Entities.MarketState", "CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MarketState_Currency");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("StockMarketWithSignalR.Entities.MarketStatistic", b =>
                {
                    b.HasOne("StockMarketWithSignalR.Entities.Currency", "Currency")
                        .WithMany("MarketStatistics")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_MarketStatistic_Currency");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("StockMarketWithSignalR.Entities.Currency", b =>
                {
                    b.Navigation("MarketState");

                    b.Navigation("MarketStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
