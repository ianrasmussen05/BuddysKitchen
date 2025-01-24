import { useState, useEffect } from 'react';
import { Button, Spinner, Toast } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import RecipeCard from './RecipeCard';
import { getAllRecipes, saveRecipe } from '../../services/apiService';
import { Recipe } from '../../types/types';

const Recipes = () => {
    const navigate = useNavigate();
    const [recipes, setRecipes] = useState<Recipe[] | null>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getRecipes();
    }, []);

    const getRecipes = async () => {
        const result = await getAllRecipes();
        if (result.status !== 200) {
            setLoading(false);
            setRecipes(null);
            return;
        }

        // set recipe list
        setLoading(false);
        setRecipes(result.data);
        console.log(result);
    }

    const handleAddRecipe = () => {
        navigate('/recipes/add');
    };

    const handleAddRecipeTest = async () => {
        const recipe: Recipe = {
            id: 9,
            name: 'Recipe Name',
            description: 'Recipe Description',
            servings: 'Recipe Servings',
            mealType: 0,
            cuisineId: 0,
            cuisine: {
                id: 0,
                name: 'Medeterranean'
            },
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
    else if (recipes === null) {
        return (
            <div>
                <h1>Our Delicious Recipes!</h1>
                <Toast className="d-inline-block m-1" bg="danger">
                    <Toast.Header>
                        <strong className="me-auto">Error</strong>
                    </Toast.Header>
                    <Toast.Body>There was an error loading the recipes. Please try again later.</Toast.Body>
                </Toast>
            </div>
        );
    }
    else {
        return (
            <div>
                <h1>Our Delicious Recipes!</h1>
                <div>{renderRecipeCards()}</div>
                <Button onClick={() => handleAddRecipe()}>Add a Recipe</Button>
                <Button onClick={() => handleAddRecipeTest()}>(Test) Add a Recipe</Button>
            </div>
        );
    }
}

export default Recipes;
