using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuddysKitchen.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDirectionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredient");

            migrationBuilder.AddColumn<long>(
                name: "IngredientQuantityId",
                table: "RecipeIngredient",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CuisineId",
                table: "Recipe",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeDirection",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<long>(type: "bigint", nullable: false),
                    DirectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDirection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeDirection_Direction_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDirection_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientQuantityId",
                table: "RecipeIngredient",
                column: "IngredientQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CuisineId",
                table: "Recipe",
                column: "CuisineId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDirection_DirectionId",
                table: "RecipeDirection",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDirection_RecipeId",
                table: "RecipeDirection",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_IngredientQuantity_IngredientQuantityId",
                table: "RecipeIngredient",
                column: "IngredientQuantityId",
                principalTable: "IngredientQuantity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Cuisine_CuisineId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_IngredientQuantity_IngredientQuantityId",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "RecipeDirection");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredient_IngredientQuantityId",
                table: "RecipeIngredient");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_CuisineId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "IngredientQuantityId",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Recipe");

            migrationBuilder.AddColumn<long>(
                name: "RecipeId",
                table: "Ingredient",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id");
        }
    }
}
