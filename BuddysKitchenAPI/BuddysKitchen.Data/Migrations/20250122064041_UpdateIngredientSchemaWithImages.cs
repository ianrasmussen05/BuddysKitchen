using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuddysKitchen.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIngredientSchemaWithImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_IngredientQuantity_IngredientQuantityId",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "IngredientQuantity");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "IngredientQuantityId",
                table: "RecipeIngredient",
                newName: "IngredientImageId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredient_IngredientQuantityId",
                table: "RecipeIngredient",
                newName: "IX_RecipeIngredient_IngredientImageId");

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Ingredient",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "IngredientImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientImage", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient",
                column: "IngredientImageId",
                principalTable: "IngredientImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_IngredientImage_IngredientImageId",
                table: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "IngredientImage");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "IngredientImageId",
                table: "RecipeIngredient",
                newName: "IngredientQuantityId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredient_IngredientImageId",
                table: "RecipeIngredient",
                newName: "IX_RecipeIngredient_IngredientQuantityId");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Ingredient",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientQuantity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientQuantity_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientQuantity_IngredientId",
                table: "IngredientQuantity",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_IngredientQuantity_IngredientQuantityId",
                table: "RecipeIngredient",
                column: "IngredientQuantityId",
                principalTable: "IngredientQuantity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
