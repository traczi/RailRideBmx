const AddToCartButton = ({ productId, quantity }) => {
  const addToCart = async () => {
    const token = localStorage.getItem("jwtToken");
    try {
      const response = await fetch(
        `https://localhost:7139/RailRideBmx/cart/AddProduct?productId=${productId}&quantity=${quantity}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.ok) {
        console.log("Produit ajouté au panier" + productId + quantity);
      } else {
        console.error("Erreur lors de l'ajout au panier");
      }
    } catch (error) {
      console.error("Erreur:", error);
    }
  };

  return <button onClick={addToCart}>Ajouter au Panier</button>;
};

export default AddToCartButton;
