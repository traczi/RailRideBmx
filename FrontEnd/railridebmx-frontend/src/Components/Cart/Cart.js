import React, { useEffect, useState } from "react";
import {
  fetchProductCart,
  updateProductQuantity,
} from "../../Services/CartService.js";
import NavBar from "../NavBar/NavBar";
import styles from "./Cart.module.css";

function Cart() {
  const [productsCart, setProductsCart] = useState([]);
  const [total, setTotal] = useState(0);

  useEffect(() => {
    const initFetch = async () => {
      try {
        const cartProducts = await fetchProductCart();
        setProductsCart(cartProducts);
        calculateTotal(cartProducts);
      } catch (err) {
        console.log(
          "Erreur lors de la récupération des produits du panier:",
          err
        );
      }
    };
    initFetch();
  }, []);

  const handleQuantityChange = async (e, product) => {
    const newQuantity = parseInt(e.target.value);
    const updatedProducts = productsCart.map((p) => {
      if (p.id === product.id) {
        return { ...p, cartQuantity: newQuantity };
      }
      return p;
    });

    setProductsCart(updatedProducts);
    calculateTotal(updatedProducts);

    try {
      const token = localStorage.getItem("jwtToken");
      await updateProductQuantity(product.id, newQuantity, token);
      console.log("Quantité mise à jour:", product.id, newQuantity);
    } catch (error) {
      console.error("Erreur lors de la mise à jour de la quantité:", error);
    }
  };

  const calculateTotal = (products) => {
    const newTotal = products.reduce(
      (sum, product) => sum + product.price * product.cartQuantity,
      0
    );
    setTotal(newTotal);
  };

  return (
    <div className={styles.cartpage}>
      <NavBar />

      <div className={styles.cartCss}>
        <h1 className={styles.title}>Cart</h1>
        <hr />
        <section className={styles.cartSection}>
          <div className={styles.allProduct}>
            {productsCart.map((productCart, index) => (
              <div key={index} className={styles.cart}>
                <div className={styles.cartItem}>
                  <div className={styles.imageProduct}>
                    <img
                      className={styles.image}
                      src={productCart.image}
                      alt="ProductImage"
                    />
                  </div>
                  <div className={styles.productDetail}>
                    <div className={styles.productDetailText}>
                      <h2 className={styles.titleProduct}>
                        {productCart.title}
                      </h2>
                      <p className={styles.quantityProduct}>
                        Quantity : {productCart.cartQuantity}
                      </p>
                      <p className={styles.productPrice}>
                        {productCart.price} €
                      </p>
                    </div>
                    <select
                      className={styles.selectQuantity}
                      value={productCart.cartQuantity}
                      onChange={(e) => handleQuantityChange(e, productCart)}
                    >
                      {[...Array(productCart.quantity).keys()].map((num) => (
                        <option key={num} value={num + 1}>
                          {num + 1}
                        </option>
                      ))}
                    </select>
                    <p className={styles.productTotalPrice}>
                      {productCart.price * productCart.cartQuantity} €
                    </p>
                  </div>
                </div>
              </div>
            ))}
          </div>
          <div className={styles.cartSummary}>
            <h2 className={styles.orderTitle}>ORDER TOTAL</h2>
            <p className={styles.totalPrice}>{total.toFixed(2)} €</p>
            <button className={styles.orderButton}><a href="/stripe">checkout</a></button>
          </div>
        </section>
      </div>
    </div>
  );
}

export default Cart;
