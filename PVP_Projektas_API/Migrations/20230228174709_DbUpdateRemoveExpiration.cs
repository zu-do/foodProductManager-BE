using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdateRemoveExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationTime",
                table: "DbProducts");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DbShelves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DbTrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTrades", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTrades");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DbShelves");

            migrationBuilder.AddColumn<int>(
                name: "ExpirationTime",
                table: "DbProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
