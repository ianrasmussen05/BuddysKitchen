import { useState, useEffect } from 'react';
import { Button, Spinner } from 'react-bootstrap';
import { getAllRecipes, saveRecipe } from '../services/apiService';
import { Recipe } from '../types/types';
import RecipeCard from './RecipeCard';

const Recipes = () => {
    const [recipes, setRecipes] = useState<Recipe[] | null>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getRecipes();
    }, []);

    const getRecipes = async () => {
        const result = await getAllRecipes();
        if (result.status !== 200) {
            setLoading(false);
            return;
        }

        // set recipe list
        setLoading(false);
        setRecipes(result.data);
        console.log(result);
    }

    const handleAddRecipe = async () => {
        const recipe: Recipe = {
            id: 9,
            name: 'Recipe Name',
            description: 'Recipe Description',
            servings: 'Recipe Servings',
            mealType: 0,
            cuisineId: 1,
            recipeIngredients: [
                {
                    id: 18,
                    recipeId: 9,
                    ingredient: { 
                        id: 19, 
                        name: 'Tomato 2', 
                        quantity: '3' 
                    },
                    ingredientId: 19,
                    ingredientImage: null,
                    ingredientImageId: null
                }
            ],
            recipeDirections: [
                {
                    id: 13,
                    recipeId: 9,
                    direction: { 
                        id: 15, 
                        stepNumber: 2, 
                        description: 'Direction Description Testing 3' 
                    },
                    directionId: 15
                }
            ],
        };

        const result = await saveRecipe(recipe);
        if (result.status !== 200) return;
        console.log(result);
    };

    const renderRecipeCards = () => {
        if (!recipes) return;
        return recipes.map(recipe => {
            return <RecipeCard key={recipe.id} {...recipe} />;
        });
    };

    if (loading) {
        return (
            <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span>
            </Spinner>
        );
    }
    else if (!recipes) {
        return (
            <div>
                <h1>Our Delicious Recipes!</h1>
                <div>Error loading recipes</div>
            </div>
        );
    }
    else {
        return (
            <div>
                <h1>Our Delicious Recipes!</h1>
                <div>{renderRecipeCards()}</div>
                <Button onClick={handleAddRecipe}>Add a Recipe</Button>
            </div>
        );
    }
}

export default Recipes;
