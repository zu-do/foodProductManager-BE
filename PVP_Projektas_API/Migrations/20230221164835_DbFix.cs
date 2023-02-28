using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class DbFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbUsers_Shelf_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_ProductCategoryCategoryName",
                table: "DbProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Shelf_ShelfId",
                table: "DbProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shelf",
                table: "DbShelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "DbProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "DbCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ShelfId",
                table: "DbProducts",
                newName: "IX_DbProducts_ShelfId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategoryCategoryName",
                table: "DbProducts",
                newName: "IX_DbProducts_ProductCategoryCategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCategoryCategoryName",
                table: "DbProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbShelves",
                table: "DbShelves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbProducts",
                table: "DbProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbCategories",
                table: "DbCategories",
                column: "CategoryName");

            migrationBuilder.CreateTable(
                name: "DbGiveawaySpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningHours = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingHours = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbGiveawaySpots", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbCategories_ProductCategoryCategoryName",
                table: "DbProducts",
                column: "ProductCategoryCategoryName",
                principalTable: "DbCategories",
                principalColumn: "CategoryName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DbProducts_DbShelves_ShelfId",
                table: "DbProducts",
                column: "ShelfId",
                principalTable: "DbShelves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbUsers_DbShelves_ShelfId",
                table: "DbUsers",
                column: "ShelfId",
                principalTable: "DbShelves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbCategories_ProductCategoryCategoryName",
                table: "DbProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DbProducts_DbShelves_ShelfId",
                table: "DbProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DbUsers_DbShelves_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropTable(
                name: "DbGiveawaySpots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbShelves",
                table: "DbShelves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbProducts",
                table: "DbProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbCategories",
                table: "DbCategories");

            migrationBuilder.RenameTable(
                name: "DbShelves",
                newName: "Shelf");

            migrationBuilder.RenameTable(
                name: "DbProducts",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "DbCategories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_DbProducts_ShelfId",
                table: "Product",
                newName: "IX_Product_ShelfId");

            migrationBuilder.RenameIndex(
                name: "IX_DbProducts_ProductCategoryCategoryName",
                table: "Product",
                newName: "IX_Product_ProductCategoryCategoryName");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCategoryCategoryName",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shelf",
                table: "Shelf",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_DbUsers_Shelf_ShelfId",
                table: "DbUsers",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_ProductCategoryCategoryName",
                table: "Product",
                column: "ProductCategoryCategoryName",
                principalTable: "Category",
                principalColumn: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Shelf_ShelfId",
                table: "Product",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id");
        }
    }
}