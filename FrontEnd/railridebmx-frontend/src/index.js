import React from "react";
import ReactDOM from "react-dom/client";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Register from "./Components/Register/Register.js";
import Login from "./Components/Login/Login.js";
import AllProduct from "./Components/Product/ListProduct/AllProduct.js";
import ProductDetails from "./Components/Product/ProductInDetails/ProductDetails.js";
import CreateProduct from "./Components/Product/CreateProduct/CreateProduct.js";
import Cart from "./Components/Cart/Cart.js";
import NavBar from "./Components/NavBar/NavBar.js";
import Comment from "./Components/Comment/Comment.js";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from 'react-toastify';
import WishList from "./Components/Like/WishList";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <BrowserRouter>
    <ToastContainer/>
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/navbar" element={<NavBar />} />
      <Route path="/register" element={<Register />} />
      <Route path="/product/:id" element={<ProductDetails />} />
      <Route path="/productCreate" element={<CreateProduct />} />
      <Route path="/" element={<AllProduct />} />
      <Route path="/productCart" element={<Cart />} />
      <Route path="/comment" element={<Comment />} />
        <Route path="/wishList" element={<WishList />} />
    </Routes>
  </BrowserRouter>
);
reportWebVitals();
