// LikedProducts.js
import React, { useState, useEffect } from 'react';
import {getWishList} from '../../Services/LikeService';
import { toast } from 'react-toastify';
import NavBar from "../NavBar/NavBar";

function WishList() {
    const [likedProducts, setLikedProducts] = useState([]);

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        getWishList(token)
            .then(products => {
                setLikedProducts(products);
            })
            .catch(error => {
                toast.error("Erreur lors de la récupération des produits aimés.");
                console.error('Erreur lors de la récupération des produits aimés:', error);
            });
    }, []);

    return (
        <div>
            <NavBar/>
            <h2>Produits Aimés</h2>
            {likedProducts.length > 0 ? (
                <ul>
                    {likedProducts.map(product => (
                        <li key={product.id}>
                            {product.price}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>Vous n'avez pas encore aimé de produits.</p>
            )}
        </div>
    );
}

export default WishList;
