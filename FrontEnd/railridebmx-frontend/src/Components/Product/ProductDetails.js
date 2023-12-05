import { useParams } from "react-router-dom";
import NavBar from "../NavBar/NavBar";
import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircle } from "@fortawesome/free-solid-svg-icons";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import "./ProductDetails.css";
import QuantitySelector from "./QuantitySelector";
import AnimatedTitle from "./AnimationTitle";

function ProductDetails() {
  const [productsDetails, setProductsDetails] = useState([]);
  const { id } = useParams();
  const fetchProductDetails = async () => {
    try {
      const res = await fetch(`https://localhost:7139/api/Product/${id}`);
      const data = await res.json();
      setProductsDetails(data);
      console.log(data);
    } catch (err) {
      console.log(err);
    }
  };
  useEffect(() => {
    fetchProductDetails();
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
    <>
      <NavBar />
      <section>
        <div className="product">
          <div className="productImage">
            <img
              alt="imageOfProduct"
              className="image"
              src={productsDetails.image}
            ></img>
          </div>
          <div className="detailProduct">
            <h1 className="titleProduct">{productsDetails.title}</h1>
            <div>
              <FontAwesomeIcon className="iconStar" icon={faStar} />
              <FontAwesomeIcon className="iconStar" icon={faStar} />
              <FontAwesomeIcon className="iconStar" icon={faStar} />
              <FontAwesomeIcon className="iconStar" icon={faStar} />
              <FontAwesomeIcon className="iconStar" icon={faStar} />
            </div>
            <div className="quantityDetails">
              <p className="quantityText">{`${getStatusText(
                productsDetails.quantity
              )}`}</p>
              {getStatusColor(productsDetails.quantity)}
            </div>
            <p className="productPrice">{productsDetails.price}€</p>
            <QuantitySelector product={productsDetails} />
            <div className="descriptionProduct">
              {/* <div className="descriptionContenair">
                <div className="descriptionSubContenair">
                  <h1 className="descriptionTitle">Description du produit</h1>
                  <button className="descriptionButton">+</button>
                </div>
                <p className="descriptionText">{productsDetails.description}</p>
              </div>
              <div className="descriptionContenair">
                <div className="descriptionSubContenair">
                  <h1 className="descriptionTitle">Spécificités techniques</h1>
                  <button className="descriptionButton">+</button>
                </div>
                <p className="descriptionText"></p>
              </div>
              <div className="descriptionContenair">
                <div className="descriptionSubContenair">
                  <h1 className="descriptionTitle">
                    Caractéristique du produit
                  </h1>
                  <button className="descriptionButton">+</button>
                </div>
                <p className="descriptionText"></p>
              </div> */}
              <AnimatedTitle product={productsDetails} />
            </div>
          </div>
        </div>
      </section>
      <hr />
    </>
  );
}

export default ProductDetails;
