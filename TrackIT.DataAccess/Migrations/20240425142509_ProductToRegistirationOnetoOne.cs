using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductToRegistirationOnetoOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRegistirations_ProductId",
                table: "ProductRegistirations",
                column: "ProductId");
        }
    }
}
