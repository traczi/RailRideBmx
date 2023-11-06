import React from "react";
import './NavBar.css'
import Img from "../../Img/Logo.png"

const NavBar = () => {
    return(
        <nav>
            <div className="low-navbar">
                <a href="#mainpage">RailRideBMX</a>
                <div>
                    <a href="#like"><i></i></a>
                    <a href="#kart"><i></i></a>
                    <a href="#profil"><i></i></a>
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
    )
}

export default NavBar;