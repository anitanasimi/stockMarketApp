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
    [Migration("20240222213134_AddMarketStateIdToCurrency")]
    partial class AddMarketStateIdToCurrency
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b075574b-55db-4f26-96ef-6e7e25cf88e8"),
                            Coefficient = 1.5m,
                            CreatedAt = new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2549),
                            CurrencyCode = 1,
                            Title = "BitCoin"
                        },
                        new
                        {
                            Id = new Guid("7b8289bc-11b0-4720-abc6-c15654b4f2ed"),
                            Coefficient = 1.1m,
                            CreatedAt = new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2606),
                            CurrencyCode = 2,
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
