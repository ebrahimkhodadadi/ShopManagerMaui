using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddStoredProductBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Baskets",
                newName: "StoredProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                newName: "IX_Baskets_StoredProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_StoredProducts_StoredProductId",
                table: "Baskets",
                column: "StoredProductId",
                principalTable: "StoredProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_StoredProducts_StoredProductId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "StoredProductId",
                table: "Baskets",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_StoredProductId",
                table: "Baskets",
                newName: "IX_Baskets_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
