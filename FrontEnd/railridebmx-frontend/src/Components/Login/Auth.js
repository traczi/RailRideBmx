import {jwtDecode} from "jwt-decode";

const isAuthenticated = () => {
    const token = localStorage.getItem('jwtToken');
    return !!token;
};

const logout = () => {
    localStorage.removeItem('jwtToken');
    window.location.reload();
};


const getUserIdFromJWT = () => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
        const decodedToken = jwtDecode(token);
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    }
    return null;
};

export { isAuthenticated, logout, getUserIdFromJWT };
