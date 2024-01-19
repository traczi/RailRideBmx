import "./NavBar.css";
import Logo from "../../Img/Logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart } from "@fortawesome/free-regular-svg-icons";
import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { faUser } from "@fortawesome/free-regular-svg-icons";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { faX } from "@fortawesome/free-solid-svg-icons";
import React, { useState } from "react";
import {Link} from "react-router-dom";

const NavBar = () => {
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [openCategoryId, setOpenCategoryId] = useState(null);

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen); // Change l'état pour ouvrir ou fermer le menu
  };
  const toggleCategory = (categoryId) => {
    if (openCategoryId === categoryId) {
      // Si la catégorie est déjà ouverte, la fermer
      setOpenCategoryId(null);
    } else {
      // Sinon, ouvrir la catégorie sélectionnée
      setOpenCategoryId(categoryId);
    }
  };

  return (
    <nav className="nav-contenair">
      <header className="header-mobile">
        <button className="menu-button" onClick={toggleMobileMenu}>
          ☰
        </button>
        <img className="logo-site" alt="imageofsite" src={Logo} />
        <button className="menu-button">
          <FontAwesomeIcon icon={faMagnifyingGlass} />
        </button>
        <div className={`all-sidemenu ${isMobileMenuOpen ? "active" : ""}`}>
          <div className={`sidebar ${isMobileMenuOpen ? "active" : ""}`}>
            <button className="close-button" onClick={toggleMobileMenu}>
              <FontAwesomeIcon icon={faX} />
            </button>
            <h2 className="title-navbar">RailRideBMX</h2>
            <div className="menu-category">
              <h3 onClick={() => toggleCategory(1)} className="category-navbar">
                + BMX Freestyle
              </h3>
              <ul
                className={
                  openCategoryId === 1 ? "list-visible" : "list-hidden"
                }
              >
                <li>Cadre</li>
                <li>Roue</li>
                <li>Guidon</li>
                <li>Pneu</li>
              </ul>
            </div>
            <div className="menu-category">
              <h3 onClick={() => toggleCategory(2)} className="category-navbar">
                + Tous les produits
              </h3>
              <ul
                className={
                  openCategoryId === 2 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
            <div className="menu-category">
              <h3 onClick={() => toggleCategory(3)} className="category-navbar">
                + Configuration
              </h3>
              <ul
                className={
                  openCategoryId === 3 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
            <div className="menu-category">
              <h3 onClick={() => toggleCategory(4)} className="category-navbar">
                + Contact
              </h3>
              <ul
                className={
                  openCategoryId === 4 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
            <div className="bottom-icons">
              <a href="/wishlist">
                <FontAwesomeIcon icon={faHeart} />
              </a>
              <a href="/productCart">
                <FontAwesomeIcon icon={faCartShopping} />
              </a>
              <a href="/login">
                <FontAwesomeIcon icon={faUser} />
              </a>
            </div>
          </div>
        </div>
      </header>
      <header className="header-tablette">
        <div className="upper-navbar">
          <div className="logo-contenair">
            <img className="logo-site" alt="imageofsite" src={Logo} />
          </div>
          <h2 className="title-navbar">RailRideBMX</h2>
          <div className="icon-tablette">
            <a href="/wishlist">
              <FontAwesomeIcon icon={faHeart} />
            </a>
            <a href="/productCart">
              <FontAwesomeIcon icon={faCartShopping} />
            </a>
            <a href="/login">
              <FontAwesomeIcon icon={faUser} />
            </a>
          </div>
        </div>
        <div className="lower-navbar-contenair">
          <div className="lower-navbar">
            <h3 onClick={() => toggleCategory(1)} className="category-navbar">
              BMX Freestyle
            </h3>
            <ul
              className={openCategoryId === 1 ? "list-visible" : "list-hidden"}
            >
              <li>Cadre</li>
              <li>Roue</li>
              <li>Guidon</li>
              <li>Pneu</li>
            </ul>
          </div>
          <div className="lower-navbar">
            <h3 onClick={() => toggleCategory(2)} className="category-navbar">
              Tous les produits
            </h3>
            <ul
              className={openCategoryId === 2 ? "list-visible" : "list-hidden"}
            ></ul>
          </div>
          <div className="lower-navbar">
            <h3 onClick={() => toggleCategory(3)} className="category-navbar">
              Configuration
            </h3>
            <ul
              className={openCategoryId === 3 ? "list-visible" : "list-hidden"}
            ></ul>
          </div>
          <div className="lower-navbar">
            <h3 onClick={() => toggleCategory(4)} className="category-navbar">
              Contact
            </h3>
            <ul
              className={openCategoryId === 4 ? "list-visible" : "list-hidden"}
            ></ul>
          </div>
          <div className="lower-navbar">
            <button className="menu-button">
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>
      </header>
      <header className="header-computer">
        <div className="upper-navbar-computer">
          <a className="main-page" href="/">
          <h2 className="title-navbar">RailRideBMX</h2></a>
          <div className="icons-computer">
            <a href="/wishlist">
              <FontAwesomeIcon icon={faHeart} />
            </a>
            <a href="/productCart">
              <FontAwesomeIcon icon={faCartShopping} />
            </a>
            <a href="/login">
              <FontAwesomeIcon icon={faUser} />
            </a>
          </div>
        </div>
        <div className="lower-navbar-computer-contenair">
          <img className="logo-site" alt="imageofsite" src={Logo} />
          <div className="lower-navbar-computer-category">
            <div className="lower-navbar-computer">
              <h3 onClick={() => toggleCategory(1)} className="category-navbar">
                BMX Freestyle
              </h3>
              <ul
                className={
                  openCategoryId === 1 ? "list-visible" : "list-hidden"
                }
              >
                <li>Cadre</li>
                <li>Roue</li>
                <li>Guidon</li>
                <li>Pneu</li>
              </ul>
            </div>
            <div className="lower-navbar-computer">
              <h3 onClick={() => toggleCategory(2)} className="category-navbar">
                <a href="/product">Tous les produits</a>
              </h3>
              <ul
                className={
                  openCategoryId === 2 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
            <div className="lower-navbar-computer">
              <h3 onClick={() => toggleCategory(3)} className="category-navbar">
                Configuration
              </h3>
              <ul
                className={
                  openCategoryId === 3 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
            <div className="lower-navbar-computer">
              <h3 onClick={() => toggleCategory(4)} className="category-navbar">
                Contact
              </h3>
              <ul
                className={
                  openCategoryId === 4 ? "list-visible" : "list-hidden"
                }
              ></ul>
            </div>
          </div>
          <div className="searshBar-contenaire">
            <input
              type="text"
              className="searshBar"
              placeholder="recherche"
            ></input>
            <FontAwesomeIcon className="searshIcon" icon={faMagnifyingGlass} />
          </div>
        </div>
        <div className="messageofday">
          <p className="message">message du jour</p>
        </div>
      </header>
    </nav>
  );
};

export default NavBar;
