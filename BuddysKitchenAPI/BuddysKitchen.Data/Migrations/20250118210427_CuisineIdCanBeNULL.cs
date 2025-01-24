using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuddysKitchen.Data.Migrations
{
    /// <inheritdoc />
    public partial class CuisineIdCanBeNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe");

            migrationBuilder.AlterColumn<long>(
                name: "CuisineId",
                table: "Recipe",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe");

            migrationBuilder.AlterColumn<long>(
                name: "CuisineId",
                table: "Recipe",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
