using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationeryMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuotationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationsItem_Quotations_QuotationId",
                table: "QuotationsItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationsItem_StationeryItems_StationeryItemId",
                table: "QuotationsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotationsItem",
                table: "QuotationsItem");

            migrationBuilder.RenameTable(
                name: "QuotationsItem",
                newName: "QuotationItems");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationsItem_StationeryItemId",
                table: "QuotationItems",
                newName: "IX_QuotationItems_StationeryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationsItem_QuotationId",
                table: "QuotationItems",
                newName: "IX_QuotationItems_QuotationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotationItems",
                table: "QuotationItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationItems_Quotations_QuotationId",
                table: "QuotationItems",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationItems_StationeryItems_StationeryItemId",
                table: "QuotationItems",
                column: "StationeryItemId",
                principalTable: "StationeryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationItems_Quotations_QuotationId",
                table: "QuotationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationItems_StationeryItems_StationeryItemId",
                table: "QuotationItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuotationItems",
                table: "QuotationItems");

            migrationBuilder.RenameTable(
                name: "QuotationItems",
                newName: "QuotationsItem");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationItems_StationeryItemId",
                table: "QuotationsItem",
                newName: "IX_QuotationsItem_StationeryItemId");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationItems_QuotationId",
                table: "QuotationsItem",
                newName: "IX_QuotationsItem_QuotationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuotationsItem",
                table: "QuotationsItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationsItem_Quotations_QuotationId",
                table: "QuotationsItem",
                column: "QuotationId",
                principalTable: "Quotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationsItem_StationeryItems_StationeryItemId",
                table: "QuotationsItem",
                column: "StationeryItemId",
                principalTable: "StationeryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
