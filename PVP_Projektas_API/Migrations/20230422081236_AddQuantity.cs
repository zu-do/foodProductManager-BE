using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "DbProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitTypeId",
                table: "DbProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbUnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUnitTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbProducts_UnitTypeId",
                table: "DbProducts",
                column: "UnitTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbUnitTypes_UnitTypeId",
                table: "DbProducts",
                column: "UnitTypeId",
                principalTable: "DbUnitTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbUnitTypes_UnitTypeId",
                table: "DbProducts");

            migrationBuilder.DropTable(
                name: "DbUnitTypes");

            migrationBuilder.DropIndex(
                name: "IX_DbProducts_UnitTypeId",
                table: "DbProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DbProducts");

            migrationBuilder.DropColumn(
                name: "UnitTypeId",
                table: "DbProducts");
        }
    }
}
