using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockMarketWithSignalR.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CurrencyCode = table.Column<int>(type: "integer", nullable: false),
                    Coefficient = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketState_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    OperationType = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketStatistic_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "Title" },
                values: new object[,]
                {
                    { new Guid("4158cab2-17d3-470a-a74d-277f5a447896"), 1.5m, new DateTime(2024, 2, 22, 21, 26, 53, 442, DateTimeKind.Utc).AddTicks(643), 1, "BitCoin" },
                    { new Guid("c3cc5a43-6d01-40f4-a68e-6a2f4089e416"), 1.1m, new DateTime(2024, 2, 22, 21, 26, 53, 442, DateTimeKind.Utc).AddTicks(687), 2, "DogeCoin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketStates_CurrencyId",
                table: "MarketStates",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketStatistics_CurrencyId",
                table: "MarketStatistics",
                column: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketStates");

            migrationBuilder.DropTable(
                name: "MarketStatistics");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
