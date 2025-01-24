import React, { useState } from 'react';
import { Button, Form, Spinner } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Recipe } from '../../types/types';
import { saveRecipe } from '../../services/apiService';

const AddRecipe = () => {
    const navigate = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);
    const [recipe, setRecipe] = useState<Recipe>({
        id: 0,
        name: '',
        description: '',
        servings: '',
        mealType: null,
        cuisineId: null,
        cuisine: null,
        recipeIngredients: [],
        recipeDirections: []
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRecipe({ 
            ...recipe, 
            [name]: value 
        });
    };

    const handleCuisineChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        console.log("target", e.target);
        const { name, value } = e.target;
        setRecipe({ 
            ...recipe, 
            [name]: value 
        });
    };

    const save = () => {
        setLoading(true);
        console.log(recipe);
        setLoading(false);

        /*const result = await saveRecipe(recipe!);
        if (result.status !== 200) {
            setLoading(false);
            setRecipe(null);
            return;
        }

        // set recipe
        setLoading(false);
        //setRecipe(result.data);
        console.log(result);
        navigate('/recipes');*/
    };

    return (
        <div>
            <h1>Add Recipe</h1>
            <Spinner animation="border" role="status" hidden={!loading}>
                <span className="visually-hidden">Loading...</span>
            </Spinner>
            <Form>
                <Form.Group controlId="formName" className="mb-3">
                    <Form.Label>Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        name="name"
                        value={recipe.name}
                        onChange={handleChange}
                        placeholder="Enter recipe name" 
                    />
                </Form.Group>

                <Form.Group controlId="formDescription" className="mb-3">
                    <Form.Label>Description</Form.Label>
                    <Form.Control 
                        as="textarea" 
                        rows={3} 
                        name="description"
                        value={recipe.description || ''}
                        onChange={handleChange}
                        placeholder="Enter recipe description" 
                    />
                </Form.Group>

                <Form.Group controlId="formServings" className="mb-3">
                    <Form.Label>Servings</Form.Label>
                    <Form.Control 
                        type="text" 
                        name="servings"
                        value={recipe.servings || ''}
                        onChange={handleChange}
                        placeholder="Enter recipe servings" 
                    />
                </Form.Group>

                <Form.Group controlId="formMealType" className="mb-3">
                    <Form.Label>Meal Type</Form.Label>
                    <Form.Control
                        as="select"
                        name="mealType"
                        value={recipe.mealType || ''}
                        onChange={handleChange}
                    >
                        <option value="">Select Meal Type</option>
                        <option value="0">Breakfast</option>
                        <option value="1">Lunch</option>
                        <option value="2">Dinner</option>
                    </Form.Control>
                </Form.Group>

                <Form.Group controlId="formCuisine" className="mb-3">
                    <Form.Label>Cuisine</Form.Label>
                    <Form.Control
                        as="select"
                        name="cuisineId"
                        value={recipe.cuisineId || ''}
                        onChange={handleChange}
                    >
                        <option value="">Select Cuisine</option>
                        <option value="1">Italian</option>
                        <option value="2">Mexican</option>
                        <option value="3">Medeterranean</option>
                    </Form.Control>
                </Form.Group>
            </Form>

            <Button variant="primary" type="submit" onClick={() => save()}>Save</Button>
        </div>
    );
};

export default AddRecipe;
