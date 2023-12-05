import { useEffect, useState } from "react";

const AnimatedTitle = ({ product }) => {
  const [items, setItems] = useState([
    {
      id: 1,
      showText: true,
      isPlus: false,
      title: "Description du produit",
      text: "",
    },
    {
      id: 2,
      showText: false,
      isPlus: true,
      title: "Spécificités techniques",
      text: "",
    },
    {
      id: 3,
      showText: false,
      isPlus: true,
      title: "Caractéristique du produit",
      text: "",
    },
  ]);

  const toggleText = async (itemId) => {
    if (itemId === 1 && !items[0].text) {
      const description = await product.description;
      setItems((prevItems) => [
        { ...prevItems[0], text: description },
        ...prevItems.slice(1),
      ]);
    }
    setItems((prevItems) =>
      prevItems.map((item) => {
        if (item.id === itemId) {
          return { ...item, showText: !item.showText, isPlus: !item.isPlus };
        } else {
          return { ...item, showText: false, isPlus: true };
        }
      })
    );
  };

  useEffect(() => {
    toggleText(1);
  }, []);

  return (
    <div className="descriptionContenair">
      {items.map((item) => (
        <div className="descriptionSubContenair" key={item.id}>
          <h1 className="descriptionTitle">{item.title}</h1>
          <button
            className="descriptionButton"
            onClick={() => toggleText(item.id)}
          >
            {item.isPlus ? "+" : "-"}
          </button>
          {item.showText && <p className="descriptionText">{item.text}</p>}
        </div>
      ))}
    </div>
  );
};

export default AnimatedTitle;
