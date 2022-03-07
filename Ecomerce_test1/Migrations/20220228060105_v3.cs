using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomerce_test1.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorsProduct_Colors_ProductTagsId",
                table: "ColorsProduct");

            migrationBuilder.RenameColumn(
                name: "ProductTagsId",
                table: "ColorsProduct",
                newName: "ProductColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsProduct_Colors_ProductColorId",
                table: "ColorsProduct",
                column: "ProductColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorsProduct_Colors_ProductColorId",
                table: "ColorsProduct");

            migrationBuilder.RenameColumn(
                name: "ProductColorId",
                table: "ColorsProduct",
                newName: "ProductTagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsProduct_Colors_ProductTagsId",
                table: "ColorsProduct",
                column: "ProductTagsId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
