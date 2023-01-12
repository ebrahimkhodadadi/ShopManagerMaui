using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddTransportLoggerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferLoggerDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TransferLoggerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferLoggerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferLoggerDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferLoggerDetail_TransportLogger_TransferLoggerId",
                        column: x => x.TransferLoggerId,
                        principalTable: "TransportLogger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferLoggerDetail_ProductId",
                table: "TransferLoggerDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferLoggerDetail_TransferLoggerId",
                table: "TransferLoggerDetail",
                column: "TransferLoggerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferLoggerDetail");
        }
    }
}
