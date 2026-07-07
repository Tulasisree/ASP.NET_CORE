using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiaryApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingDataDiaryEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DiaryEntries",
                columns: new[] { "Id", "Content", "Created", "Title" },
                values: new object[,]
                {
                    { 1, "Went hiking with friends", new DateTime(2026, 7, 6, 16, 21, 38, 87, DateTimeKind.Local).AddTicks(8320), "Went Hiking" },
                    { 2, "Went shopping with friends", new DateTime(2026, 7, 6, 16, 21, 38, 87, DateTimeKind.Local).AddTicks(8550), "Went shopping" },
                    { 3, "Went diving with friends", new DateTime(2026, 7, 6, 16, 21, 38, 87, DateTimeKind.Local).AddTicks(8550), "Went diving" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
