import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './ProductList.css';
import NavBar from "../NavBar/NavBar";

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchProducts = async () => {
            const response = await fetch('https://localhost:7139/RailRideBmx/Product');
            const data = await response.json();
            setProducts(data);
        };

        fetchProducts();
    }, []);

    const handleDelete = async (guid) => {
        // Remplacez par votre URL d'API réelle
        await fetch(`https://localhost:7139/RailRideBmx/Product/DeleteProduct?guid=${guid}`, {
            method: 'DELETE',
        });
        setProducts(products.filter(product => product.id !== guid));
    };

    return (
        <div>
            <NavBar/>
            <div className="ProductList-container"><div className="ProductList">
            <h1>Liste des Produits</h1>
            <table className="product-list-table">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Title</th>
                    <th>Quantité</th>
                    <th>Prix</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                {products.map(product => (
                    <tr key={product.id}>
                        <td>{product.id}</td>
                        <td>{product.title}</td>
                        <td>{product.quantity}</td>
                        <td>{product.price}</td>
                        {/* autres cellules de données */}
                        <td>
                            <button onClick={() => navigate(`/modifyproduct/${product.id}`)}>Modifier</button>
                            <button onClick={() => handleDelete(product.id)}>Supprimer</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
            </div></div>
        </div>
    );
};

export default ProductList;
