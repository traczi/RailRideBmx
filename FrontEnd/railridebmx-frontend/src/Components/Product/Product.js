import { useEffect, useState } from "react";
import "./Product.css";
import NavBar from "../NavBar/NavBar";

function Product() {
  const [products, setProducts] = useState([]);

  const fetchProdcut = async () => {
    try {
      const res = await fetch("https://localhost:7139/api/Product");
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

  return (
    <>
      <NavBar />
      <h1 className="title">BMX</h1>
      <section>
        {products.map((product) => {
          return (
            <div className="productList">
              <div className="productImage">
                <img className="image" src={product.image}></img>
              </div>
              <div className="productProps">
                <h1 className="productTitle">{product.title}</h1>
                <p className="productQuantity">
                  {`${getStatusText(product.quantity)}`}
                </p>
                <i class="fa-solid fa-circle" />
                <p className="productPrice">{product.price}€</p>
              </div>
            </div>
          );
        })}
      </section>
    </>
  );
}

export default Product;
