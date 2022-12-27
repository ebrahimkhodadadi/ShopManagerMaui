using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class removeProductListTrnsportLogger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductList",
                table: "TransportLogger");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductList",
                table: "TransportLogger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
