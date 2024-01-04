import { useEffect, useState } from "react";
import "./Product.css";
import NavBar from "../../NavBar/NavBar";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircle } from "@fortawesome/free-solid-svg-icons";

function AllProduct() {
  const [products, setProducts] = useState([]);

  const fetchProdcut = async () => {
    try {
      const res = await fetch("https://localhost:7139/RailRideBmx/Product");
      const data = await res.json();
      setProducts(data);
      console.log(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    fetchProdcut();
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
    <div className="productCss">
      <NavBar />
      <h1 className="title">BMX</h1>
      <section className="productSection">
        {products.map((product) => {
          return (
            <div className="productList" key={product.id}>
              <div className="productImage">
                <img
                  alt="imageOfProduct"
                  className="image"
                  src={product.image}
                ></img>
              </div>
              <div className="productProps">
                <a href={`/product/${product.id}`}>
                  <h1 className="productTitle">{product.title}</h1>
                </a>
                <div className="quantitySection">
                  <p className="productQuantity">
                    {`${getStatusText(product.quantity)}`}
                  </p>
                  {getStatusColor(product.quantity)}
                </div>
                <p className="productPrice">{product.price}€</p>
              </div>
            </div>
          );
        })}
      </section>
    </div>
  );
}

export default AllProduct;