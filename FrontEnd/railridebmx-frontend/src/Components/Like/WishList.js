import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom'; // Assurez-vous que vous utilisez react-router-dom dans votre projet
import { getWishList } from '../../Services/LikeService';
import { toast, ToastContainer } from 'react-toastify';
import NavBar from "../NavBar/NavBar";

function WishList() {
    const [likedProducts, setLikedProducts] = useState([]);
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        if (!token) {
            // Si l'utilisateur n'est pas connecté, afficher un message
            toast.info("Veuillez vous connecter pour accéder à la wishlist.");
            return; // Ne pas faire l'appel à l'API si l'utilisateur n'est pas connecté
        }

        setIsLoggedIn(true); // L'utilisateur est connecté
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
            {isLoggedIn ? (
                likedProducts.length > 0 ? (
                    <ul>
                        {likedProducts.map(product => (
                            <div key={product.id}>
                                <div><img src={product.image} alt={product.title}/></div>
                                <div>
                                    <h1>{product.title}</h1>
                                    <p>{product.price}</p></div>

                            </div>
                        ))}
                    </ul>
                ) : (
                    <p>Vous n'avez pas encore aimé de produits.</p>
                )
            ) : (
                <div>
                    <p>Pour accéder à la wishlist, veuillez <Link to="/login">vous connecter</Link> ou <Link to="/register">créer un compte</Link>.</p>

                </div>
            )}
        </div>
    );
}

export default WishList;
