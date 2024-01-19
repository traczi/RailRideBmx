import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import NavBar from "../NavBar/NavBar";
import './UserList.css';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('jwtToken');

        const fetchUsers = async () => {
            try {
                const response = await fetch('https://localhost:7139/RailRideBmx/User', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`,
                    },
                });

                if (!response.ok) {
                    throw new Error(`Erreur HTTP! statut: ${response.status}`);
                }

                const data = await response.json();
                setUsers(data);
            } catch (error) {
                console.error("Erreur lors de la récupération des utilisateurs:", error);
            }
        };

        fetchUsers();
    }, []);

    return (
        <div>
            <NavBar/>
            <div className="UserList-container">
                <div className="UserList">
                <h1>Liste des Utilisateurs</h1>
                <table className="user-list-table">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Prénom</th>
                        <th>Nom</th>
                        <th>Email</th>
                        {/* ... autres en-têtes ... */}
                    </tr>
                    </thead>
                    <tbody>
                    {users.map((user, index) => (
                        <tr key={user.id} className={index % 2 === 0 ? 'even' : 'odd'}>
                            <td>{user.id}</td>
                            <td>{user.lastname}</td>
                            <td>{user.firstname}</td>
                            <td>{user.email}</td>
                            {/* ... autres cellules ... */}
                        </tr>
                    ))}
                    </tbody>
                </table>
                </div>
            </div>
        </div>
    );
};

export default UserList;
