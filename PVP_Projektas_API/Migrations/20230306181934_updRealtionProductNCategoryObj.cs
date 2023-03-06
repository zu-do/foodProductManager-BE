using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class updRealtionProductNCategoryObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbCategories_ProductCategoryCategoryName",
                table: "DbProducts");

            migrationBuilder.DropIndex(
                name: "IX_DbProducts_ProductCategoryCategoryName",
                table: "DbProducts");

            migrationBuilder.DropColumn(
                name: "ProductCategoryCategoryName",
                table: "DbProducts");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "DbProducts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbProducts_CategoryName",
                table: "DbProducts",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbCategories_CategoryName",
                table: "DbProducts",
                column: "CategoryName",
                principalTable: "DbCategories",
                principalColumn: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbCategories_CategoryName",
                table: "DbProducts");

            migrationBuilder.DropIndex(
                name: "IX_DbProducts_CategoryName",
                table: "DbProducts");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "DbProducts");

            migrationBuilder.AddColumn<string>(
                name: "ProductCategoryCategoryName",
                table: "DbProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DbProducts_ProductCategoryCategoryName",
                table: "DbProducts",
                column: "ProductCategoryCategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbCategories_ProductCategoryCategoryName",
                table: "DbProducts",
                column: "ProductCategoryCategoryName",
                principalTable: "DbCategories",
                principalColumn: "CategoryName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
