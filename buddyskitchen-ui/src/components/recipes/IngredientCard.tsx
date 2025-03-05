import { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import { RecipeIngredient } from '../../types/types';
import { getImage } from '../../services/apiService';

const IngredientCard = (recipeIngredient: RecipeIngredient) => {
    const [imageData, setImageData] = useState<any>(null);

    useEffect(() => {
        getImageData();
    });
    
    const getImageData = async () => {
        if (!recipeIngredient.ingredientImage?.imageURL) return;

        const result = await getImage('ingredients', recipeIngredient.ingredientImage.imageURL)
        if (result.status !== 200) return;

        setImageData(`data:image/jpeg;base64,${result.data}`); // set data to image format
    }

    return (
        <Card style={{ width: '18rem' }}>
            <Card.Img variant="top" src={imageData} />
            <Card.Body>
                <Card.Title>{recipeIngredient.ingredient?.name || ""}</Card.Title>
                <Card.Text><strong>Quantity:</strong> {recipeIngredient.ingredient?.quantity || ""}</Card.Text>
            </Card.Body>
        </Card>
    );
}

export default IngredientCard;
