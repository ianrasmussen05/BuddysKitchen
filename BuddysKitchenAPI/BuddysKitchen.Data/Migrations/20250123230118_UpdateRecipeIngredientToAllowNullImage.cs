using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuddysKitchen.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipeIngredientToAllowNullImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient");

            migrationBuilder.AlterColumn<long>(
                name: "IngredientImageId",
                table: "RecipeIngredient",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient",
                column: "IngredientImageId",
                principalTable: "IngredientImage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient");

            migrationBuilder.AlterColumn<long>(
                name: "IngredientImageId",
                table: "RecipeIngredient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient",
                column: "IngredientImageId",
                principalTable: "IngredientImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
