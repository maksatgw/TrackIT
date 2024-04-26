using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIT.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductRegistirationHistoryProductUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistirationHistorys_ProductRegistirations_ProductRegistirationId",
                table: "RegistirationHistorys");

            migrationBuilder.RenameColumn(
                name: "ProductRegistirationId",
                table: "RegistirationHistorys",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "RegistirationHistorys",
                newName: "RegistrationDate");

            migrationBuilder.RenameIndex(
                name: "IX_RegistirationHistorys_ProductRegistirationId",
                table: "RegistirationHistorys",
                newName: "IX_RegistirationHistorys_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "RegistirationHistorys",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RegistirationHistorys_AppUserId",
                table: "RegistirationHistorys",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistirationHistorys_AspNetUsers_AppUserId",
                table: "RegistirationHistorys",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistirationHistorys_Product_ProductId",
                table: "RegistirationHistorys",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistirationHistorys_AspNetUsers_AppUserId",
                table: "RegistirationHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistirationHistorys_Product_ProductId",
                table: "RegistirationHistorys");

            migrationBuilder.DropIndex(
                name: "IX_RegistirationHistorys_AppUserId",
                table: "RegistirationHistorys");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RegistirationHistorys");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "RegistirationHistorys",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "RegistirationHistorys",
                newName: "ProductRegistirationId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistirationHistorys_ProductId",
                table: "RegistirationHistorys",
                newName: "IX_RegistirationHistorys_ProductRegistirationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistirationHistorys_ProductRegistirations_ProductRegistirationId",
                table: "RegistirationHistorys",
                column: "ProductRegistirationId",
                principalTable: "ProductRegistirations",
                principalColumn: "ProductRegistirationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
