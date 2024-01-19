import React, { useState, useEffect } from 'react';
import UserDataChart from './UserDataChart';
import NavBar from "../NavBar/NavBar";
import PaidCartsChart from "./PaidsCartsChart";
import {useNavigate} from "react-router-dom";
import "./Dashboard.css"
import {getUserRoleFromJWT} from "../Login/Auth";

const Dashboard = () => {
    let navigate = useNavigate();
    const [userData, setUserData] = useState([]);
    const [cartData, setCartData] = useState([]);
    const [role, setrole] = useState('');

    useEffect(() => {
        // Simuler le chargement des données
        const loadUserData = async () => {
            // Remplacez ceci par le chargement réel des données
            const data = [
                { date: '2023-06-12', userCount: 5 },
                { date: '2023-08-12', userCount: 2 },
                { date: '2023-06-12', userCount: 8 },
                { date: '2023-06-12', userCount: 4 },
                { date: '2023-06-12', userCount: 5 },
                { date: '2023-06-12', userCount: 2 },
                { date: '2023-06-12', userCount: 8 },
                { date: '2023-06-12', userCount: 4 },
                { date: '2023-06-12', userCount: 8 },
                { date: '2023-06-12', userCount: 4 },
                { date: '2023-06-12', userCount: 5 },
                { date: '2023-06-12', userCount: 2 },
                // ...plus de données
            ];
            setUserData(data);
        };
        const loadCartData = async () => {
            const data = [
                { date: '2023-06-12', paidCartsCount: 4 },
                { date: '2023-08-12', paidCartsCount: 9 },
                { date: '2023-06-12', paidCartsCount: 2 },
                { date: '2023-06-12', paidCartsCount: 4 },
                { date: '2023-06-12', paidCartsCount: 5 },
                { date: '2023-06-12', paidCartsCount: 4 },
                { date: '2023-06-12', paidCartsCount: 8 },
                { date: '2023-06-12', paidCartsCount: 4 },
                { date: '2023-06-12', paidCartsCount: 5 },
                { date: '2023-06-12', paidCartsCount: 3 },
                { date: '2023-06-12', paidCartsCount: 5 },
                { date: '2023-06-12', paidCartsCount: 10 },
            ];
            setCartData(data);
        };
        const roleDecode = getUserRoleFromJWT();
        setrole(roleDecode);
        loadCartData();
        loadUserData();
    }, []);

    return (
        <div>

            <NavBar/>
            {role === 'Admin' && (
            <div className="dashboard">
                <div className="dashboard-main">
                    <h1>Statistiques des Utilisateurs</h1>
                    <UserDataChart userData={userData}/>
                    <PaidCartsChart cartData={cartData}/>
                </div>
                <div className="dashboard-sidebar">
                    <button onClick={() => navigate('/ProductCreate')}>Créer Produit</button>
                    <button onClick={() => navigate('/userlist')}>Liste des Utilisateurs</button>
                    <button onClick={() => navigate('/productList')}>Liste des produits</button>
                    <button onClick={() => navigate('/paidcarts')}>Paniers Payés</button>
                </div>
            </div>
            )}
        </div>
    );
};

export default Dashboard;
