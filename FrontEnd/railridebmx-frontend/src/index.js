import React from "react";
import ReactDOM from "react-dom/client";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Register from "./Components/Register/Register.js";
import AllProduct from "./Components/Product/ListProduct/AllProduct.js";
import ProductDetails from "./Components/Product/ProductInDetails/ProductDetails.js";
import CreateProduct from "./Components/Product/CreateProduct/CreateProduct.js";
import Cart from "./Components/Cart/Cart.js";
import NavBar from "./Components/NavBar/NavBar.js";
import Comment from "./Components/Comment/Comment.js";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from 'react-toastify';
import WishList from "./Components/Like/WishList";
import PaymentForm from "./Components/Stripe/PaymentForm";
import LoginPage from "./Components/Login/LoginPage";
import HomePage from "./Components/HomePage/Homepage";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <BrowserRouter>
    <ToastContainer/>
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/navbar" element={<NavBar />} />
      <Route path="/register" element={<Register />} />
      <Route path="/product/:id" element={<ProductDetails />} />
      <Route path="/productCreate" element={<CreateProduct />} />
      <Route path="/product" element={<AllProduct />} />
      <Route path="/productCart" element={<Cart />} />
      <Route path="/comment" element={<Comment />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/wishList" element={<WishList />} />
        <Route path="/stripe" element={<PaymentForm />} />
    </Routes>
  </BrowserRouter>
);
reportWebVitals();
