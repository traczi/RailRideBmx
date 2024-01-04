import {toast} from "react-toastify";

const API_BASE_URL = "https://localhost:7139/RailRideBmx";
export const likeProduct = async (productId, token) => {
    const url = `${API_BASE_URL}/Like/Like?productId=${productId}`;
    return await sendLikeRequest(url,token);
};

export const unlikeProduct = async (productId, token) => {
    const url = `${API_BASE_URL}/Like/UnLike?productId=${productId}`;
    return await sendLikeRequest(url, token);
};

export const isProductLiked = async (productId, token) => {
    try{
        const res = await fetch(`${API_BASE_URL}/Like/IsLiked?productId=${productId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });
        if (!res.ok){
            throw new Error(`HTTP error! status: ${res.status}`)
        }
        return await res.json();
    }
    catch (error){
        console.log("Impossible de vérifier le status" + error);
        throw error;
    }
}

export const getWishList = async (token) => {
    try{
        const res = await fetch(`${API_BASE_URL}/Like/GetLike`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });
        if (!res.ok){
            throw new Error(`HTTP error! status: ${res.status}`)
        }
        const product = await res.json();
        return product;
    }
    catch (error){
        console.error("Could not get liked products:", error);
        throw error;
    }
}

const sendLikeRequest = async (url,token) => {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });

        if (response.status === 401 || response.status === 403) {
            // Gérer spécifiquement les erreurs d'autorisation
            toast.error("Vous devez créer un compte afin de pouvoir aimer un produit");
            throw new Error('Non autorisé');
        }

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return await response.ok;
    } catch (error) {
        console.error("Could not perform the operation:", error);
        throw error;
    }
}

