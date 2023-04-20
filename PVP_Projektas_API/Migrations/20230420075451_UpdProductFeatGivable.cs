using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdProductFeatGivable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Givable",
                table: "DbProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Givable",
                table: "DbProducts");
        }
    }
}
