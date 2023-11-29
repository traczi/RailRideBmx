import React from "react";
import "./NavBar.css";
import Img from "../../Img/Logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-regular-svg-icons";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { faUser } from "@fortawesome/free-regular-svg-icons";
import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <nav>
      <div className="low-navbar">
        <Link to="/product">RailRideBMX</Link>
        <div>
          <a>
            <FontAwesomeIcon icon={faHeart} />
          </a>
          <a>
            <FontAwesomeIcon icon={faCartShopping} />
          </a>
          <Link to="/">
            <FontAwesomeIcon icon={faUser} />
          </Link>
        </div>
      </div>
      <div className="main-navbar">
        <img alt="logo of the site" src={Img}></img>
        <div className="cat-navbar">
          <a href="#cat1">Catégorie 1</a>
          <a href="#cat2">Catégorie 2</a>
          <a href="#cat3">Catégorie 3</a>
          <a href="#cat4">Catégorie 4</a>
          <a href="#cat5">Catégorie 5</a>
        </div>
        <div className="search-bar">
          <input type="text" placeholder="Search.."></input>
        </div>
      </div>
      <div className="navbar-message">
        <p>Message du jour</p>
      </div>
    </nav>
  );
};

export default NavBar;
