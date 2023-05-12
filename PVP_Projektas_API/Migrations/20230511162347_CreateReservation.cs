using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "DbProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DbReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    GivesUserId = table.Column<int>(type: "int", nullable: false),
                    TakesUserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbReservations_DbProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DbProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DbReservations_DbUsers_GivesUserId",
                        column: x => x.GivesUserId,
                        principalTable: "DbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DbReservations_DbUsers_TakesUserId",
                        column: x => x.TakesUserId,
                        principalTable: "DbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbReservations_GivesUserId",
                table: "DbReservations",
                column: "GivesUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DbReservations_ProductId",
                table: "DbReservations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DbReservations_TakesUserId",
                table: "DbReservations",
                column: "TakesUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbReservations");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "DbProducts");
        }
    }
}
