using BuddysKitchen.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                type.SetTableName(type.DisplayName());
            }

            // Recipe -> RecipeIngredients (One-to-Many)
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeIngredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);

            // Recipe -> RecipeDirections (One-to-Many)
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeDirections)
                .WithOne(rd => rd.Recipe)
                .HasForeignKey(rd => rd.RecipeId);

            // RecipeIngredient -> Ingredient (Many-to-One)
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany() // No navigation back from Ingredient to RecipeIngredient
                .HasForeignKey(ri => ri.IngredientId);

            // RecipeIngredient -> IngredientQuantity (Many-to-One)
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.IngredientImage)
                .WithMany() // No navigation back from IngredientQuantity to RecipeIngredient
                .HasForeignKey(ri => ri.IngredientImageId);

            // RecipeDirection -> Direction (Many-to-One)
            modelBuilder.Entity<RecipeDirection>()
                .HasOne(rd => rd.Direction)
                .WithMany() // No navigation back from Direction to RecipeDirection
                .HasForeignKey(rd => rd.DirectionId);

            // Recipe -> Cuisine (Many-to-One)
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Cuisine)
                .WithMany() // No navigation back from Cuisine to Recipe
                .HasForeignKey(r => r.CuisineId);
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientImage> IngredientsImage { get; set; }
        public DbSet<RecipeDirection> RecipeDirection { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
    }
}
