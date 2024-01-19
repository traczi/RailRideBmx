// RatingStars.js
import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar as solidStar } from '@fortawesome/free-solid-svg-icons';
import { faStar as regularStar } from '@fortawesome/free-regular-svg-icons';

function RatingStars({ onRatingChange }) {
    const [rating, setRating] = useState(0);
    const [hoverRating, setHoverRating] = useState(0);

    const handleMouseOver = (newHoverRating) => {
        setHoverRating(newHoverRating);
    };

    const handleMouseLeave = () => {
        setHoverRating(0);
    };

    const handleClick = (newRating) => {
        setRating(newRating);
        if (onRatingChange) {
            onRatingChange(newRating);
        }
    };

    return (
        <div >
            {[1, 2, 3, 4, 5].map((index) => (
                <FontAwesomeIcon
                    className="rating-stars"
                    key={index}
                    icon={index <= (hoverRating || rating) ? solidStar : regularStar}
                    onMouseOver={() => handleMouseOver(index)}
                    onMouseLeave={handleMouseLeave}
                    onClick={() => handleClick(index)}
                    style={{ cursor: 'pointer', color: '#FFD700' }}
                />
            ))}
        </div>
    );
}

export default RatingStars;
