import React, {useEffect, useState} from "react";
import FormValidation from "../FormValidation/FormValidation";
import { Formik, Form } from "formik";
import NavBar from "../NavBar/NavBar";
import "../Product/CreateProduct/CreateProduct.css"
import {useNavigate, useParams} from "react-router-dom";

const UpdateProduct = () => {
    const [product, setProduct] = useState(null);
    const { productId } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        // Vous devriez avoir une fonction qui fait la requête API pour récupérer les détails du produit
        const fetchProductDetails = async () => {
            const response = await fetch(`https://localhost:7139/RailRideBmx/Product/${productId}`);
            const data = await response.json();
            setProduct(data);
        };

        fetchProductDetails();
    }, [productId]);

    const onUpdateProduct = async (data) => {
        console.log(data)
        // La requête PUT pour mettre à jour le produit
        const response = await fetch(`https://localhost:7139/RailRideBmx/Product/UpdateProduct?guid=${productId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            navigate('/productList');
        } else {
            console.log("Erreur lors de la mise à jour du produit", response.status);
        }
    };

    if (!product) {
        return <div>Chargement...</div>;
    }

    return (
        <>
            <NavBar/>
            <div className="creatProduct-title-container">
                < h1 className="creatProduct-title">Modification d'un produit</h1>
            </div>
            <div className="creatProduct-container">

                <div className="creatProduct-content">
                    <Formik
                        initialValues={{
                            title: product.title,
                            category: product.category,
                            subcategory: product.subcategory,
                            geometry: product.geometry,
                            image: product.image,
                            description: product.description,
                            color: product.color,
                            brand: product.brand,
                            frameSize: product.frameSize,
                            handlebarSize: product.handlebarSize,
                            wheelSize: product.wheelSize,
                            price: product.price,
                            quantity: product.quantity,
                        }}
                        onSubmit={(values) => {
                            onUpdateProduct(values);
                        }}
                    >

                        <Form>
                            <FormValidation
                                type="text"
                                name="title"
                                title="Title"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="category"
                                title="Category"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="subcategory"
                                title="Subcategory"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="geometry"
                                title="Geometry"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="url"
                                name="image"
                                title="Image URL"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="description"
                                title="Description"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="color"
                                title="Color"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="text"
                                name="brand"
                                title="Brand"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="number"
                                name="frameSize"
                                title="FrameSize"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="number"
                                name="handlebarSize"
                                title="HandlebarSize"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="number"
                                name="wheelSize"
                                title="WheelSize"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="number"
                                name="price"
                                title="Price"
                                className="create-field"
                            ></FormValidation>
                            <FormValidation
                                type="number"
                                name="quantity"
                                title="Quantity"
                                className="create-field"
                            ></FormValidation>
                            <div className="creatProduct-button-container">
                                <button type="submit" className="creatProduct-button">
                                    Modifier Produit
                                </button>
                            </div>
                        </Form>

                    </Formik>
                </div>
            </div>
        </>
    );
};
export default UpdateProduct;
