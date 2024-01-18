import React from "react";
import FormValidation from "../../FormValidation/FormValidation";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import NavBar from "../../NavBar/NavBar";
import "./CreateProduct.css"

const CreateProduct = () => {
  const onCreateProduct = async (data) => {
    try {
      const urlWithParams = `https://localhost:7139/RailRideBmx/Product?url=${encodeURIComponent(
        data.image
      )}`;
      const res = await fetch(urlWithParams, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (res.ok) {
        console.log("Le produit a bien été créé");
      }
    } catch (err) {
      console.log(err);
    }
  };
  return (
      <>
          <NavBar/>
          <div className="creatProduct-title-container">
              < h1 className="creatProduct-title">Création d'un produit</h1>
          </div>
          <div className="creatProduct-container">

              <div className="creatProduct-content">
                  <Formik
                      initialValues={{
                          title: "",
                          category: "",
                          subcategory: "",
                          geometry: "",
                          image: "",
                          description: "",
                          color: "",
                          brand: "",
                          frameSize: "0",
                          handlebarSize: "0",
                          wheelSize: "0",
                          price: "0",
                          quantity: "0",
                      }}
                      onSubmit={(values) => {
                          onCreateProduct(values);
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
                                  Create Produit
                              </button>
                          </div>
                      </Form>

                  </Formik>
              </div>
          </div>
      </>
  );
};
export default CreateProduct;
