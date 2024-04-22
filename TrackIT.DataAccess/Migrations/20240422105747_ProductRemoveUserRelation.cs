using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductRemoveUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_AppUserId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_AppUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AppUserId",
                table: "Product",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_AppUserId",
                table: "Product",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
