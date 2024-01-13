import React from 'react';
import NavBar from '../NavBar/NavBar.js';
import { isAuthenticated, logout } from './Auth';
import Login from "./Login";

function LoginPage() {
    if (isAuthenticated()) {
        return (
            <div>
                <NavBar />
                <button onClick={logout}>Se Déconnecter</button>
            </div>
        );
    } else {
        return (
            <div className="login">
                <Login />
            </div>
        );
    }
}

export default LoginPage;
