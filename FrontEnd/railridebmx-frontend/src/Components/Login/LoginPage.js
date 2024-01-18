import React, {useEffect, useState} from 'react';
import NavBar from '../NavBar/NavBar.js';
import { isAuthenticated, logout } from './Auth';
import Login from "./Login";
import {getUser, modifyEmail, modifyName, modifyPassword} from "../../Services/UserService";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faUser} from "@fortawesome/free-regular-svg-icons";
import {faPencil} from "@fortawesome/free-solid-svg-icons";

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
    const submitCancelChange = () => {
        setIsEditing(false);
        setIsEditingPassword(false);
        setIsEditingEmail(false);
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
                <div className="loginPage">
                    <div className="loginPage-Container">
                        <div className="loginPage-userLogo-Container">
                            <FontAwesomeIcon icon={faUser} className="loginPage-userLogo"/>
                        </div>
                        {isEditing ? (
                            <div className="loginPage-userEmail-container">
                                <div className="loginPage-EmailField-Modify">
                                    <div className="loginPage-EmailText">
                                        <p>Nom : </p>
                                    </div>
                                    <input className="loginPage-userEmail-input-noReadonly"
                                           name="firstName"
                                           value={newName.firstName}
                                           onChange={handleNameChange}
                                    />
                                </div>
                                <div className="loginPage-userEmail-container">
                                    <div className="loginPage-EmailText">
                                        <p>Prénom: </p>
                                    </div>
                                    <input className="loginPage-userEmail-input-noReadonly"
                                           name="lastName"
                                           value={newName.lastName}
                                           onChange={handleNameChange}
                                    />
                                </div>
                                <div className="loginPage-SaveAndModifyButton">
                                    <button onClick={submitNameChange}>Sauvegarder</button>
                                    <button onClick={submitCancelChange}>Annuler</button>
                                </div>
                            </div>
                        ) : (
                            <div className="loginPage-userName-container">
                                <p>{user?.firstname}</p>
                                <p> {user?.lastname}</p>
                                <button className="loginPage-user-modifyButton" onClick={() => setIsEditing(true)}><FontAwesomeIcon icon={faPencil  }/></button>
                            </div>
                        )}
                        {/* Modification de l'email */}
                        <div>
                            {!isEditingEmail ? (
                                <>
                                    <div className="loginPage-userEmail-container">
                                        <div className="loginPage-EmailText">
                                            <p>Email: </p>
                                        </div>
                                        <div className="loginPage-EmailField">
                                            <input className="loginPage-userEmail-input" type="email" value={email}
                                                    readOnly/>
                                            <button className="loginPage-user-modifyButton"
                                                    onClick={() => setIsEditingEmail(true)}><FontAwesomeIcon
                                                icon={faPencil}/>
                                            </button>
                                        </div>
                                    </div>
                                </>
                            ) : (
                                <>
                                    <div className="loginPage-userEmail-container">
                                        <div className="loginPage-EmailText">
                                            <p>Email: </p>
                                        </div>
                                        <div className="loginPage-EmailField-Modify">
                                            <input className="loginPage-userEmail-input-noReadonly" type="email" value={email}
                                                   onChange={handleEmailChange}/>
                                            <div className="loginPage-SaveAndModifyButton">
                                                <button onClick={submitEmailChange}>Sauvegarder</button>
                                                <button onClick={submitCancelChange}>Annuler</button>
                                            </div>
                                        </div>
                                    </div>
                                </>
                            )}
                        </div>

                        {/* Modification du mot de passe */}
                        <div>
                            {!isEditingPassword ? (
                                <button className="loginPage-ModifyPasswordButton" onClick={() => setIsEditingPassword(true)}>Modifier le mot de passe</button>
                            ) : (
                                <>
                                    <div className="loginPage-userEmail-container">
                                        <label>Ancien mot de passe :</label>
                                        <input className="loginPage-userEmail-input-noReadonly" type="password" value={oldPassword}
                                               onChange={(e) => setOldPassword(e.target.value)}/>
                                    </div>
                                    <div className="loginPage-userEmail-container">
                                        <label>Nouveau mot de passe :</label>
                                        <input className="loginPage-userEmail-input-noReadonly" type="password" value={newPassword}
                                               onChange={(e) => setNewPassword(e.target.value)}/>
                                    </div>
                                    <div className="loginPage-userEmail-container">
                                        <label>Confirmer le nouveau mot de passe :</label>
                                        <input className="loginPage-userEmail-input-noReadonly" type="password" value={confirmNewPassword}
                                               onChange={(e) => setConfirmNewPassword(e.target.value)}/>
                                    </div>
                                    <div className="loginPage-SaveAndModifyButton">
                                        <button onClick={submitPasswordChange}>Sauvegarder</button>
                                        <button onClick={submitCancelChange}>Annuler</button>
                                    </div>
                                </>
                            )}

                        </div>
                        <button className="loginPage-logoutButoon" onClick={logout}>Se Déconnecter</button>
                    </div>
                </div>
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
