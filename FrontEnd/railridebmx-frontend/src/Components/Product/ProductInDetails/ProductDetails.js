import { useParams } from "react-router-dom";
import NavBar from "../../NavBar/NavBar";
import Comment from "../../Comment/Comment";
import CommentList from "../../Comment/CommentList";
import React, {Suspense, useEffect, useState} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircle } from "@fortawesome/free-solid-svg-icons";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import "./ProductDetails.css";
import QuantitySelector from "./QuantitySelector";
import AnimatedTitle from "./AnimationTitle";
import AddToCartButton from "./AddCartButton";
import Loading from "../../Loading/Loading";
const LikeButton = React.lazy(() =>  import("../../Like/LikeButton"));

function ProductDetails() {
  const [productsDetails, setProductsDetails] = useState([]);
  const { id } = useParams();
  const [quantity, setQuantity] = useState(1);
  const handleQuantityChange = (newQuantity) => {
    setQuantity(newQuantity);
  };

  const fetchProductDetails = async () => {
    try {
      const res = await fetch(
        `https://localhost:7139/RailRideBmx/Product/${id}`
      );
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
    <div className="productDetailCss">
      <Suspense fallback={<Loading/>}>
      <NavBar />
      <section className="productDetailsSection">
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
            <div className="like-button">

              <LikeButton productId={productsDetails.id}/>
            </div>
            <div className="quantityDetails">
              <p className="quantityText">{`${getStatusText(
                productsDetails.quantity
              )}`}</p>
              {getStatusColor(productsDetails.quantity)}
            </div>
            <p className="productPrice">{productsDetails.price}€</p>
            <QuantitySelector
              product={productsDetails}
              onQuantityChange={handleQuantityChange}
            />
            <AddToCartButton
              productId={productsDetails.id}
              quantity={quantity}
            />
            <div className="descriptionProduct">
              <AnimatedTitle product={productsDetails} />
            </div>
          </div>
        </div>
      </section>
      <hr />
      <div className="comment-contenair">
        <Comment productId={productsDetails.id} />
        <CommentList productId={productsDetails.id} />
      </div>
      </Suspense>
    </div>
  );
}

export default ProductDetails;
