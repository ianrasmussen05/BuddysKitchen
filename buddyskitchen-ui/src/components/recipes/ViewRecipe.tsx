import { useState, useEffect } from 'react';
import { Card, Spinner, Toast, Container, Row, Col } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import { Recipe } from '../../types/types';
import { getRecipeById } from '../../services/apiService';
import IngredientCard from './IngredientCard';

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

    const renderIngredientCards = () => {
        if (recipe?.recipeIngredients.length === 0) return;
        return recipe?.recipeIngredients.map(ingredient => {
            return <IngredientCard key={ingredient.id} {...ingredient} />;
        });
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
            <Container>
                <Row className="justify-content-md-center">
                    <Col md="8">
                        <Card className="mt-4">
                            <Card.Header as="h5">{recipe.name}</Card.Header>
                            <Card.Body>
                                <Card.Text>
                                    <strong>Description:</strong> {recipe.description}
                                </Card.Text>
                                <Card.Text>
                                    <strong>Servings:</strong> {recipe.servings}
                                </Card.Text>
                                <Card.Text>
                                    <strong>Meal Type:</strong> {recipe.mealType}
                                </Card.Text>
                                <Card.Text>
                                    <strong>Cuisine:</strong> {recipe.cuisine ? recipe.cuisine.name : 'N/A'}
                                </Card.Text>
                            </Card.Body>
                            <Card.Footer>
                                {renderIngredientCards()}
                            </Card.Footer>                        
                        </Card>
                    </Col>
                </Row>
            </Container>
        );
    }
};

export default ViewRecipe;