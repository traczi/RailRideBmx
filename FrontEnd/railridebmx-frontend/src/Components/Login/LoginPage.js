import React, {useEffect, useState} from 'react';
import NavBar from '../NavBar/NavBar.js';
import { isAuthenticated, logout } from './Auth';
import Login from "./Login";
import {getUser, modifyEmail, modifyName, modifyPassword} from "../../Services/UserService";

function LoginPage() {
    const [user, setUser] = useState(null);
    const [isEditing, setIsEditing] = useState(false);
    const [isEditingEmail, setIsEditingEmail] = useState(false);
    const [isEditingPassword, setIsEditingPassword] = useState(false);
    const [newName, setNewName] = useState({ firstName: '', lastName: '' });
    const [email, setEmail] = useState('');
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [confirmNewPassword, setConfirmNewPassword] = useState('');

    useEffect(() => {
        if (isAuthenticated()) {
            const token = localStorage.getItem('jwtToken');
            getUser(token).then(data => {
                if (data) {
                    setUser(data);
                    setEmail(data.email);
                    setNewName({ firstName: data.firstname, lastName: data.lastname });
                }
            });
        }
    }, []);


    const handleNameChange = (e) => {
        setNewName({ ...newName, [e.target.name]: e.target.value });
    };
    const handleEmailChange = (e) => {
        setEmail(e.target.value);
    };

    const submitNameChange = async () => {
        try {
            const token = localStorage.getItem('jwtToken');
            const updatedNames = {
                firstName: newName.firstName,
                lastName: newName.lastName,
            };
            await modifyName( updatedNames,token);
            setIsEditing(false);
        } catch (error) {
            console.error("Erreur lors de la mise à jour du nom/prénom :", error);
        }
    };

    const submitEmailChange = async () => {
        try {
            const token = localStorage.getItem('jwtToken');
            await modifyEmail(email, token);
            alert("Email mis à jour avec succès !");
        } catch (error) {
            console.error("Erreur lors de la mise à jour de l'email:", error);
            alert("Erreur lors de la mise à jour de l'email.");
        }
    };

    const submitPasswordChange = async () => {
        if (newPassword !== confirmNewPassword) {
            alert("Les mots de passe ne correspondent pas.");
            return;
        }

        try {
            const token = localStorage.getItem('jwtToken');
            await modifyPassword(oldPassword, newPassword, token);
            alert("Mot de passe mis à jour avec succès !");
            setOldPassword('');
            setNewPassword('');
            setConfirmNewPassword('');
        } catch (error) {
            console.error("Erreur lors de la mise à jour du mot de passe:", error);
            alert("Erreur lors de la mise à jour du mot de passe.");
        }
    };

    console.log(email);
    if (isAuthenticated()) {
        return (
            <div>
                <NavBar/>
                {isEditing ? (
                    <div>
                        <input
                            name="firstName"
                            value={newName.firstName}
                            onChange={handleNameChange}
                        />
                        <input
                            name="lastName"
                            value={newName.lastName}
                            onChange={handleNameChange}
                        />
                        <button onClick={submitNameChange}>Sauvegarder</button>
                    </div>
                ) : (
                    <div>
                        <p>{user?.firstname}</p>
                        <p> {user?.lastname}</p>
                        <button onClick={() => setIsEditing(true)}>Modifier le nom</button>
                    </div>
                )}
                {/* Modification de l'email */}
                <div>
                    {!isEditingEmail ? (
                        <>
                            <p>Email: {email}</p>
                            <button onClick={() => setIsEditingEmail(true)}>Modifier l'email</button>
                        </>
                    ) : (
                        <>
                            <input type="email" value={email} onChange={handleEmailChange}/>
                            <button onClick={submitEmailChange}>Sauvegarder</button>
                        </>
                    )}
                </div>

                {/* Modification du mot de passe */}
                <div>
                    {!isEditingPassword ? (
                        <button onClick={() => setIsEditingPassword(true)}>Modifier le mot de passe</button>
                    ) : (
                        <>
                            <div>
                                <label>Ancien mot de passe :</label>
                                <input type="password" value={oldPassword}
                                       onChange={(e) => setOldPassword(e.target.value)}/>
                            </div>
                            <div>
                                <label>Nouveau mot de passe :</label>
                                <input type="password" value={newPassword}
                                       onChange={(e) => setNewPassword(e.target.value)}/>
                            </div>
                            <div>
                                <label>Confirmer le nouveau mot de passe :</label>
                                <input type="password" value={confirmNewPassword}
                                       onChange={(e) => setConfirmNewPassword(e.target.value)}/>
                            </div>
                            <button onClick={submitPasswordChange}>Sauvegarder</button>

                        </>
                    )}

                </div>
                <button onClick={logout}>Se Déconnecter</button>
            </div>
        );
    } else {
        return (
            <div className="login">
                <Login/>
            </div>
        );
    }
}

export default LoginPage;
