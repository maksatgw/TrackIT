using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RegisterProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRegistirations_Product_ProductId",
                table: "ProductRegistirations",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRegistirations_Product_ProductId",
                table: "ProductRegistirations");

            migrationBuilder.DropIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations");
        }
    }
}
