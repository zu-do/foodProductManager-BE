using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdProductGiveAwayAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "DbProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbProducts_AddressId",
                table: "DbProducts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbAddresses_AddressId",
                table: "DbProducts",
                column: "AddressId",
                principalTable: "DbAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbAddresses_AddressId",
                table: "DbProducts");

            migrationBuilder.DropIndex(
                name: "IX_DbProducts_AddressId",
                table: "DbProducts");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "DbProducts");
        }
    }
}
