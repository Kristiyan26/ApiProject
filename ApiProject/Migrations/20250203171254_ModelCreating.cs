using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiProject.Migrations
{
    /// <inheritdoc />
    public partial class ModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "CompanyName", "Industry", "LastDiv", "MarketCap", "Purchase", "Symbol" },
                values: new object[,]
                {
                    { 1, "Apple Inc.", "Technology", 0.82m, 2230000000000L, 145.30m, "AAPL" },
                    { 2, "Microsoft Corporation", "Technology", 0.56m, 1980000000000L, 265.65m, "MSFT" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreateOn", "StockId", "Title" },
                values: new object[,]
                {
                    { 1, "Apple stocks are performing exceptionally well.", new DateTime(2025, 2, 3, 19, 12, 54, 121, DateTimeKind.Local).AddTicks(4555), 1, "Great Stock" },
                    { 2, "Microsoft has shown consistent growth.", new DateTime(2025, 2, 3, 19, 12, 54, 124, DateTimeKind.Local).AddTicks(3260), 2, "Steady Growth" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
