import { useState } from "react";

const QuantitySelector = ({ product, onQuantityChange }) => {
  const [quantity, setQuantity] = useState(1);

  const updateQuantity = (newQuantity) => {
    setQuantity(newQuantity);
    onQuantityChange(newQuantity); // Informer le composant parent de la nouvelle quantité
  };

  const increment = () => {
    if (quantity < (product.quantity || 1)) {
      updateQuantity(quantity + 1);
    }
  };
  const decrement = () => {
    if (quantity > 1) {
      updateQuantity(quantity - 1);
    }
  };

  return (
    <div className="bottomProductDetails">
      <div className="quantityChoise">
        <button
          onClick={decrement}
          type="button"
          className="decremanteButton"
          disabled={quantity <= 1 || product.quantity === 0}
        >
          -
        </button>
        <input
          type="number"
          id="quantity"
          name="quantity"
          value={product.quantity === 0 ? 0 : quantity}
          min="1"
          max={product.quantity || 1}
          readOnly
        />
        <button
          onClick={increment}
          type="button"
          className="incremanteButton"
          disabled={
            quantity >= (product.quantity || 1) || product.quantity === 0
          }
        >
          +
        </button>
      </div>
    </div>
  );
};
export default QuantitySelector;
