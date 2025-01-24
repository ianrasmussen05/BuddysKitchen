import { useState, useEffect } from 'react';
import { Form, Spinner, Toast } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import { Recipe } from '../../types/types';
import { getRecipeById } from '../../services/apiService';

const RecipeForm = () => {
    const { recipeId } = useParams<{ recipeId: string }>();
    const [recipe, setRecipe] = useState<Recipe | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        getRecipe();
    }, [recipeId]);

    const getRecipe = async () => {
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
                <h1>Recipe Form</h1>
                <Form>
                    <Form.Group className="mb-3">
                        
                    </Form.Group>
                </Form>
            </div>
        );
    }
};

export default RecipeForm;