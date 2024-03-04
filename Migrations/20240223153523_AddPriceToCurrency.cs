using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockMarketWithSignalR.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("7b8289bc-11b0-4720-abc6-c15654b4f2ed"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("b075574b-55db-4f26-96ef-6e7e25cf88e8"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "MarketStateId", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("36ca82c5-e5be-4a27-be3d-7fb323dec262"), 1.5m, new DateTime(2024, 2, 23, 15, 35, 22, 490, DateTimeKind.Utc).AddTicks(5702), 1, null, 0m, "BitCoin" },
                    { new Guid("385327f1-95f9-4399-b06e-de058657f1dc"), 1.1m, new DateTime(2024, 2, 23, 15, 35, 22, 490, DateTimeKind.Utc).AddTicks(5749), 2, null, 0m, "DogeCoin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("36ca82c5-e5be-4a27-be3d-7fb323dec262"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("385327f1-95f9-4399-b06e-de058657f1dc"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Currencies");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "MarketStateId", "Title" },
                values: new object[,]
                {
                    { new Guid("7b8289bc-11b0-4720-abc6-c15654b4f2ed"), 1.1m, new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2606), 2, null, "DogeCoin" },
                    { new Guid("b075574b-55db-4f26-96ef-6e7e25cf88e8"), 1.5m, new DateTime(2024, 2, 22, 21, 31, 34, 78, DateTimeKind.Utc).AddTicks(2549), 1, null, "BitCoin" }
                });
        }
    }
}
