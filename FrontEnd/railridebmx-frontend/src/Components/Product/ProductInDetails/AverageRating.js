// AverageRating.js
import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar as fullStar } from '@fortawesome/free-solid-svg-icons';
import { faStar as emptyStar } from '@fortawesome/free-regular-svg-icons';
import { fetchAverageRating } from '../../../Services/CommentService.js';

function AverageRating({ productId }) {
    const [averageRating, setAverageRating] = useState(0);

    useEffect(() => {
        fetchAverageRating(productId)
            .then(rating => setAverageRating(rating))
            .catch(error => console.error('Erreur lors de la récupération de la note moyenne:', error));
    }, [productId]);

    // Créez une liste d'étoiles basée sur la note moyenne
    const starIcons = [];
    for (let i = 1; i <= 5; i++) {
        starIcons.push(
            <FontAwesomeIcon
                key={i}
                icon={i <= averageRating ? fullStar : emptyStar}
                style={{ color: 'gold' }}
            />
        );
    }

    return (
        <div>
            {starIcons}
        </div>
    );
}

export default AverageRating;
