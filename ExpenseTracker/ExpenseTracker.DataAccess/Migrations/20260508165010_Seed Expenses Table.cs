using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedExpensesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "Date", "Description" },
                values: new object[,]
                {
                    { 1, 230m, "Electronics", new DateTime(2026, 5, 8, 20, 50, 8, 159, DateTimeKind.Local).AddTicks(4924), "Motherboard" },
                    { 2, 410m, "Electronics", new DateTime(2026, 5, 8, 20, 50, 8, 159, DateTimeKind.Local).AddTicks(4936), "CPU" },
                    { 3, 40m, "Peripherals", new DateTime(2026, 5, 8, 20, 50, 8, 159, DateTimeKind.Local).AddTicks(4937), "Keyboard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
