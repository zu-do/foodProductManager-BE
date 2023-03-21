using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class UserAndShelfRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DbShelves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves",
                column: "UserId",
                principalTable: "DbUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DbShelves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DbShelves_DbUsers_UserId",
                table: "DbShelves",
                column: "UserId",
                principalTable: "DbUsers",
                principalColumn: "Id");
        }
    }
}
