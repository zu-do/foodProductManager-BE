using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class AddShelvesUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbUsers_DbShelves_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropIndex(
                name: "IX_DbUsers_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "DbUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DbShelves",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbShelves_UserId",
                table: "DbShelves",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves",
                column: "UserId",
                principalTable: "DbUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves");

            migrationBuilder.DropIndex(
                name: "IX_DbShelves_UserId",
                table: "DbShelves");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DbShelves");

            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "DbUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbUsers_ShelfId",
                table: "DbUsers",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbUsers_DbShelves_ShelfId",
                table: "DbUsers",
                column: "ShelfId",
                principalTable: "DbShelves",
                principalColumn: "Id");
        }
    }
}
