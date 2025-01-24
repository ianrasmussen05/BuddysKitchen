import { useState, useEffect } from 'react';
import { Table, Spinner, Toast } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import { Recipe } from '../../types/types';
import { getRecipeById } from '../../services/apiService';

const ViewRecipe = () => {
    const { recipeId } = useParams<{ recipeId: string }>();
    const [recipe, setRecipe] = useState<Recipe | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getRecipe();
    }, [recipeId]);

    const getRecipe = async () => {
        if (!recipeId) return;

        const result = await getRecipeById(Number(recipeId));
        if (result.status !== 200) {
            setLoading(false);
            setRecipe(null);
            return;
        }

        // set recipe
        setLoading(false);
        setRecipe(result.data);
        console.log(result);
    };

    if (loading) {
        return (
            <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span>
            </Spinner>
        );
    } else if (recipe === null) {
        return (
            <div>
                <Toast className="d-inline-block m-1" bg="danger">
                    <Toast.Header>
                        <strong className="me-auto">Error</strong>
                    </Toast.Header>
                    <Toast.Body>There was an error loading the recipe. Please try again later.</Toast.Body>
                </Toast>
            </div>
        );
    } else {
        return (
            <div>
                <h1>View Recipe</h1>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Name</th>
                        <td>{recipe.name}</td>
                    </tr>
                    <tr>
                            <th>Description</th>
                        <td>{recipe.description}</td>
                    </tr>
                    <tr>
                            <th>Servings</th>
                        <td>{recipe.servings}</td>
                    </tr>
                    <tr>
                            <th>Meal Type</th>
                            <th>Cuisine</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{recipe.name}</td>
                            <td>{recipe.description}</td>
                            <td>{recipe.servings}</td>
                            <td>{recipe.mealType}</td>
                            <td>{recipe.cuisine ? recipe.cuisine.name : ''}</td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }
};

export default ViewRecipe;