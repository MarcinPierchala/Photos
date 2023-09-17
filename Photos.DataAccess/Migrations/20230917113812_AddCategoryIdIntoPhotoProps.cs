using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photos.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryIdIntoPhotoProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MyPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 5,
                column: "CategoryId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "MyPhotos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CategoryId",
                value: 13);

            migrationBuilder.CreateIndex(
                name: "IX_MyPhotos_CategoryId",
                table: "MyPhotos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyPhotos_Categories_CategoryId",
                table: "MyPhotos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyPhotos_Categories_CategoryId",
                table: "MyPhotos");

            migrationBuilder.DropIndex(
                name: "IX_MyPhotos_CategoryId",
                table: "MyPhotos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MyPhotos");
        }
    }
}
