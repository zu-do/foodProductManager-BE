using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DbUnitTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kg" },
                    { 2, "L" },
                    { 3, "Vnt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DbUnitTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DbUnitTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DbUnitTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
