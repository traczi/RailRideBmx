// LikeButton.js
import React, {useEffect, useState} from 'react';
import { toast } from 'react-toastify';
import {isProductLiked,unlikeProduct,likeProduct} from '../../Services/LikeService.js';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome';
import {faHeart as solidHeart} from '@fortawesome/free-solid-svg-icons';
import {faHeart as regularHeart} from '@fortawesome/free-regular-svg-icons';
import './Like.css'
import Loading from "../Loading/Loading";


function LikeButton({productId}) {
    const [isLoading, setIsLoading] = useState(true);
    const [isLiked, setIsLiked] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        isProductLiked(productId, token)
            .then(status => setIsLiked(status))
            .catch(error => console.error('Erreur lors de la vérification du like:', error));
        setIsLoading(false);
    }, [productId]);

    const handleLike = async () => {
        try {
            const token = localStorage.getItem("jwtToken");
            if (isLiked) {
                await unlikeProduct(productId, token);
                toast.info("Vous n'aimez plus ce produit.");
            } else {
                await likeProduct(productId, token);
                toast.info("Vous avez aimé ce produit !");
            }
            setIsLiked(!isLiked);
        }
        catch (e) {
            toast.error(e)
        }
    };
    if(isLoading){
        return <Loading/>
    }

    return (
        <button className="button-like" onClick={handleLike} aria-label={isLiked ? "Unlike" : "Like"}>
            <FontAwesomeIcon className="heart" icon={isLiked ? solidHeart : regularHeart}/>
        </button>
    );
}

export default LikeButton;
