import { useEffect, useState } from "react";

const QuantitySelector = ({ product }) => {
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    setQuantity(1);
  }, []);

  const increment = () => {
    if (quantity < (product.quantity || 1)) {
      setQuantity((prev) => prev + 1);
    }
  };
  const decrement = () => {
    if (quantity > 1) {
      setQuantity((prev) => prev - 1);
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
      <button type="button" className="buttonCart">
        ADD TO CART
      </button>
    </div>
  );
};
export default QuantitySelector;
