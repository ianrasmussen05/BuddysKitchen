export interface Ingredient {
    id: number;
    name: string;
    quantity: string | null;
};

export interface IngredientImage {
    id: number;
    imageURL: string | null;
};

export interface RecipeIngredient {
    id: number;
    recipeId: number;
    ingredient: Ingredient | null;
    ingredientId: number | null;
    ingredientImage: IngredientImage | null;
    ingredientImageId: number | null;
};

export interface Direction {
    id: number;
    stepNumber: number;
    description: string | null;
};

export interface RecipeDirection {
    id: number;
    recipeId: number;
    direction: Direction;
    directionId: number;
};

export interface Recipe {
    id: number;
    name: string;
    description: string | null;
    servings: string | null;
    mealType: number | null;
    cuisineId: number | null;
    recipeIngredients: RecipeIngredient[];
    recipeDirections: RecipeDirection[];
};