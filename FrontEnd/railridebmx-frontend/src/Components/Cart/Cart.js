import React, { useEffect, useState } from "react";
import {
  fetchProductCart,
  updateProductQuantity,
} from "../../Services/CartService.js";
import NavBar from "../NavBar/NavBar";
import  "./Cart.css";

function Cart() {
  const [productsCart, setProductsCart] = useState([]);
  const [total, setTotal] = useState(0);
  const [cartId, setCartId] = useState('');

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
    const fetchCartId = async () => {
      try {
        const response = await fetch('https://localhost:7139/RailRideBmx/Cart/CartId');
        const data = await response.json();
        setCartId(data);
      } catch (error) {
        console.error("Erreur lors de la récupération du cartId:", error);
      }
    };

    fetchCartId();
    initFetch();
  }, []);

  const deleteProductFromCart = async (productId) => {
    try {
      const response = await fetch(
          `https://localhost:7139/RailRideBmx/Cart/DeleteProduct?cartId=${cartId}&productId=${productId}`,
          {
            method: 'DELETE',
            headers: {
              'Content-Type': 'application/json',
              // Supposons que vous passiez le jeton JWT dans les en-têtes de la requête
              'Authorization': `Bearer ${localStorage.getItem("jwtToken")}`,
            },
          }
      );

      if (!response.ok) {
        throw new Error('Error deleting product from cart');
      }

      // Filtrer le produit supprimé de l'état local pour mettre à jour l'UI
      setProductsCart(currentProducts =>
          currentProducts.filter(p => p.id !== productId)
      );

      console.log("Product removed from cart:", productId);
    } catch (error) {
      console.error("Error during cart product deletion:", error);
    }
  };

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
    <div className="cartpage">
      <NavBar />

      <div className="cartCss">
        <h1 className="cartpage-title">Cart</h1>
        <hr className="cart-hr" />
        <section className="cartSection">
          <div className="allProduct">
            {productsCart.map((productCart, index) => (
              <div key={index} className="cart">
                <div className="cartItem">
                  <div className="imageProduct">
                    <img
                      className="image"
                      src={productCart.image}
                      alt="ProductImage"
                    />
                  </div>
                  <div className="productDetail">
                    <div className="productDetailText">
                      <h2 className="titleProduct">
                        {productCart.title}
                      </h2>
                      <p className="quantityProduct">
                        Quantity : {productCart.cartQuantity}
                      </p>
                      <p className="productPrice">
                        {productCart.price} €
                      </p>
                    </div>
                    <select
                        className="selectQuantity"
                        value={productCart.cartQuantity}
                        onChange={(e) => handleQuantityChange(e, productCart)}
                    >
                      {[...Array(productCart.quantity).keys()].map((num) => (
                          <option key={num} value={num + 1}>
                            {num + 1}
                          </option>
                      ))}
                    </select>
                    <button
                        className="deleteButton"
                        onClick={() => deleteProductFromCart(productCart.id)}
                    >
                      Supprimer
                    </button>
                    <p className="productTotalPrice">
                      {productCart.price * productCart.cartQuantity} €
                    </p>
                  </div>
                </div>
              </div>
            ))}
          </div>
          <div className="cartSummary">
            <h2 className="orderTitle">ORDER TOTAL</h2>
            <p className="totalPrice">{total.toFixed(2)} €</p>
            <button className="orderButton"><a href="/stripe">checkout</a></button>
          </div>
        </section>
      </div>
    </div>
  );
}

export default Cart;
