// ProductService.js
const API_BASE_URL = "https://localhost:7139/RailRideBmx";

export const getRandomProduct = async () => {
    const response = await fetch(`${API_BASE_URL}/Product/GetRandomProduct`);
    if (!response.ok) {
        throw new Error('Erreur lors du chargement du produit aléatoire.');
    }
    return response.json();
};

export const getTopRatedProducts = async () => {
    const response = await fetch(`${API_BASE_URL}/Product/top-rated`);
    if (!response.ok) {
        throw new Error('Erreur lors du chargement des produits les mieux notés.');
    }
    return response.json();
};

export const getAllProductProperties = async () => {
    const response = await fetch(`${API_BASE_URL}/Product/GetAllProductProperties`);
    if (!response.ok) {
        throw new Error('Erreur lors de la récupération des propriétés des produits.');
    }
    return response.json();
};

export const getFilteredProducts = async (filters) => {
    const queryParams = new URLSearchParams({
        ...filters,
    }).toString();

    const response = await fetch(`${API_BASE_URL}/Product/Filter?${queryParams}`);
    if (!response.ok) {
        throw new Error('Erreur lors de la récupération des produits filtrés.');
    }
    return response.json();
};
