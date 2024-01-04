const API_BASE_URL = "https://localhost:7139/RailRideBmx";

export const fetchProductCart = async () => {
  const response = await fetch(`${API_BASE_URL}/Cart/GetProducts`);
  if (!response.ok) {
    throw new Error("Erreur lors de la récupération des produits du panier");
  }
  return await response.json();
};

export const updateProductQuantity = async (productId, newQuantity, token) => {
  const response = await fetch(
    `${API_BASE_URL}/cart/UpdateProductQuantity?productId=${productId}&newQuantity=${newQuantity}`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    }
  );
  if (!response.ok) {
    throw new Error("Erreur lors de la mise à jour de la quantité du produit");
  }
  return response.ok;
};
