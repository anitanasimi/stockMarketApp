using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockMarketWithSignalR.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToCurrencyWithDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("36ca82c5-e5be-4a27-be3d-7fb323dec262"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("385327f1-95f9-4399-b06e-de058657f1dc"));

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "MarketStateId", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("699bb0f8-9b10-4f5e-a98c-39498067edb6"), 1.1m, new DateTime(2024, 2, 23, 15, 57, 35, 726, DateTimeKind.Utc).AddTicks(8868), 2, null, 20m, "DogeCoin" },
                    { new Guid("ae1507ec-d52d-4211-ac02-30eebd569cee"), 1.5m, new DateTime(2024, 2, 23, 15, 57, 35, 726, DateTimeKind.Utc).AddTicks(8816), 1, null, 60000m, "BitCoin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("699bb0f8-9b10-4f5e-a98c-39498067edb6"));

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: new Guid("ae1507ec-d52d-4211-ac02-30eebd569cee"));

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Coefficient", "CreatedAt", "CurrencyCode", "MarketStateId", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("36ca82c5-e5be-4a27-be3d-7fb323dec262"), 1.5m, new DateTime(2024, 2, 23, 15, 35, 22, 490, DateTimeKind.Utc).AddTicks(5702), 1, null, 0m, "BitCoin" },
                    { new Guid("385327f1-95f9-4399-b06e-de058657f1dc"), 1.1m, new DateTime(2024, 2, 23, 15, 35, 22, 490, DateTimeKind.Utc).AddTicks(5749), 2, null, 0m, "DogeCoin" }
                });
        }
    }
}
