using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVP_Projektas_API.Migrations
{
    /// <inheritdoc />
    public partial class MoreModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "DbUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DbCategories",
                columns: table => new
                {
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryName);
                });

            migrationBuilder.CreateTable(
                name: "DbShelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryCategoryName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationTime = table.Column<int>(type: "int", nullable: false),
                    ShelfId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_ProductCategoryCategoryName",
                        column: x => x.ProductCategoryCategoryName,
                        principalTable: "Category",
                        principalColumn: "CategoryName");
                    table.ForeignKey(
                        name: "FK_Product_Shelf_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelf",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbUsers_ShelfId",
                table: "DbUsers",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryCategoryName",
                table: "DbProducts",
                column: "ProductCategoryCategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShelfId",
                table: "DbProducts",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbUsers_Shelf_ShelfId",
                table: "DbUsers",
                column: "ShelfId",
                principalTable: "Shelf",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbUsers_Shelf_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Shelf");

            migrationBuilder.DropIndex(
                name: "IX_DbUsers_ShelfId",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "DbUsers");
        }
    }
}
