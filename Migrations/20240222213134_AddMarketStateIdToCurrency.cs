using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockMarketWithSignalR.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketStateIdToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("4158cab2-17d3-470a-a74d-277f5a447896"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("c3cc5a43-6d01-40f4-a68e-6a2f4089e416"));

            migrationBuilder.AddColumn<Guid>(
                name: "MarketStateId",
                table: "Currencies",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "MarketStateId", "Title" },
                values: new object[,]
                {
                    { new Guid("7b8289bc-11b0-4720-abc6-c15654b4f2ed"), 1.1m, new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2606), 2, null, "DogeCoin" },
                    { new Guid("b075574b-55db-4f26-96ef-6e7e25cf88e8"), 1.5m, new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2549), 1, null, "BitCoin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7b8289bc-11b0-4720-abc6-c15654b4f2ed"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b075574b-55db-4f26-96ef-6e7e25cf88e8"));

            migrationBuilder.DropColumn(
                name: "MarketStateId",
                table: "Currencies");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "Title" },
                values: new object[,]
                {
                    { new Guid("4158cab2-17d3-470a-a74d-277f5a447896"), 1.5m, new DateTime(2024, 2, 22, 21, 26, 53, 442, DateTimeKind.Utc).AddTicks(643), 1, "BitCoin" },
                    { new Guid("c3cc5a43-6d01-40f4-a68e-6a2f4089e416"), 1.1m, new DateTime(2024, 2, 22, 21, 26, 53, 442, DateTimeKind.Utc).AddTicks(687), 2, "DogeCoin" }
                });
        }
    }
}
