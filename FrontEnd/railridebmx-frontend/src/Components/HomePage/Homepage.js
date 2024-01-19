import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import { getRandomProduct, getTopRatedProducts } from '../../Services/ProductService';
import NavBar from "../NavBar/NavBar";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faCircle} from "@fortawesome/free-solid-svg-icons";
import ImageHomePage from "../../Img/ImageHomePage.png"
import "./HomePage.css"
import {Link} from "react-router-dom";
function HomePage() {
    const [randomProduct, setRandomProduct] = useState([]);
    const [topRatedProducts, setTopRatedProducts] = useState([]);

    useEffect(() => {
        getRandomProduct()
            .then(data => setRandomProduct(data))
            .catch(error => toast.error(error.message));
        getTopRatedProducts()
            .then(data => setTopRatedProducts(data))
            .catch(error => toast.error(error.message));
    }, []);

    const getStatusText = (quantity) => {
        if (quantity > 0) {
            return "En stock";
        } else {
            return "Pas en stock";
        }
    };
    const getStatusColor = (quantity) => {
        if (quantity > 0) {
            return (
                <FontAwesomeIcon
                    className="iconStock"
                    icon={faCircle}
                    style={{ color: "#469521" }}
                />
            );
        } else {
            return (
                <FontAwesomeIcon
                    className="iconStock"
                    icon={faCircle}
                    style={{ color: "#e32626" }}
                />
            );
        }
    };

    return (
        <div className="HomePage">
            <NavBar className="NavHomePage"/>
            <div className="HomePage-Header">
                <img src={ImageHomePage} alt="imagehomepage" className="HomePageBackgroundImage"/>
                <a className="HomePage-ButtonShop" href="/product">Trouver des produits → </a>
            </div>
            <div className="HomePage-Product-Container">
                <div className="homePage-title-container">
                    <h1>Sélection de produit</h1>
                    <hr/>
                </div>
                <div className="HomePage-Product-Content">
                {randomProduct.length > 0 ? (
                    randomProduct.map(randomProduct => (
                        <Link to={`/product/${randomProduct.id}`} key={randomProduct.id}
                              className="HomePage-Product-Product">
                            <div className="HomePage-Product-image">
                                <img className="HomePage-Product-Content-Image" src={randomProduct.image}
                                     alt={randomProduct.title}/>
                            </div>
                            <h2>{randomProduct.title}</h2>
                            <p className="hommepage-quantityText">{randomProduct.price} €</p>

                            <div className="homepage-quantity">
                                <p className="hommepage-quantityText">{`${getStatusText(
                                    randomProduct.quantity
                                )}`}</p>
                                {getStatusColor(randomProduct.quantity)}
                            </div>
                        </Link>
                    ))
                ) : (
                    <p>Chargement des produits...</p>
                )}
                </div>
            </div>
            <div className="HomePage-Product-Container">
                <div className="homePage-title-container" >
                    <h1>Produits les mieux notés</h1>
                    <hr/>
                </div>
                <div className="HomePage-Product-Content">
                {topRatedProducts.length > 0 ? (
                    topRatedProducts.map(product => (
                        <Link to={`/product/${product.id}`} key={product.id} className="HomePage-Product-Product">
                            <div className="HomePage-Product-image">
                                <img className="HomePage-Product-Content-Image" src={product.image} alt={product.title}/>
                            </div >
                            <h2>{product.title}</h2>
                            <p className="hommepage-quantityText">{product.price} €</p>
                            <div className="homepage-quantity">
                                <p className="hommepage-quantityText">{`${getStatusText(
                                    product.quantity
                                )}`}</p>
                                {getStatusColor(product.quantity)}
                            </div>
                        </Link>
                    ))
                ) : (
                    <p>Chargement des produits...</p>
                )}
                </div>
            </div>
        </div>
    );
}

export default HomePage;
