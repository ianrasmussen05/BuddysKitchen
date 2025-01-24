import { useState, useEffect } from 'react';
import { Card, ListGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Recipe } from '../../types/types';

const RecipeCard = (recipe: Recipe) => {
    const navigate = useNavigate();
    const [mealType, setMealType] = useState<string>('');

    useEffect(() => {
        getMealType();
    });

    const handleViewRecipe = (recipeId: number) => {
        navigate(`/recipes/${recipeId}`);
    };

    const getMealType = () => {
        if (recipe.mealType === 0) {
            setMealType('Breakfast');
        } else if (recipe.mealType === 1) {
            setMealType('Lunch');
        } else if (recipe.mealType === 2) {
            setMealType('Dinner');
        }
    };

    return (
        <Card style={{ width: '18rem' }}>
            <Card.Img variant="top" src="holder.js/100px180?text=Image cap" />
            <Card.Body>
                <Card.Title>{recipe.name}</Card.Title>
                <Card.Text>{recipe.description}</Card.Text>
            </Card.Body>
            <ListGroup className="list-group-flush">
                <ListGroup.Item><b>Cuisine -</b> {recipe.cuisineId}</ListGroup.Item>
                <ListGroup.Item><b>Meal Type -</b> {mealType}</ListGroup.Item>
                <ListGroup.Item><b>Servings -</b> {recipe.servings}</ListGroup.Item>
            </ListGroup>
            <Card.Body>
                <Card.Link onClick={() => handleViewRecipe(recipe.id)}>View Recipe</Card.Link>
            </Card.Body>
        </Card>
    );
}

export default RecipeCard;
