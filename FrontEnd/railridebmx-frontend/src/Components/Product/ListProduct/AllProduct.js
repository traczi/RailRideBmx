import { useEffect, useState } from "react";
import "./Product.css";
import NavBar from "../../NavBar/NavBar";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircle } from "@fortawesome/free-solid-svg-icons";
import {Link} from "react-router-dom";
import FilterComponent from "./FilterComponent";

function AllProduct() {
  const [products, setProducts] = useState([]);
  const [filters, setFilters] = useState({
    searchItem: '',
    page: 1,
    pageSize: 30,
    color: '',
    brand: '',
    frameSize: '',
    handlebarSize: '',
    wheelSize: '',
    showInStockOnly: false
  });

  const fetchProdcut = async () => {
    const queryParams = new URLSearchParams(filters).toString();
    try {
      const res = await fetch(`https://localhost:7139/RailRideBmx/Product/Filter?${queryParams}`);
      const data = await res.json();
      setProducts(data);
      console.log(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    fetchProdcut();
  }, [filters]);

  const goToNextPage = () => {
    setFilters((filters) => ({
      ...filters,
      page: filters.page + 1,
    }));
  };

  // Fonction pour revenir à la page précédente
  const goToPreviousPage = () => {
    setFilters((filters) => ({
      ...filters,
      page: Math.max(filters.page - 1, 1), // Empêche la page de devenir inférieure à 1
    }));
  };

  const handleSearchChange = (e) => {
    setFilters({ ...filters, searchItem: e.target.value });
  };

  const handleInStockChange = (e) => {
    setFilters({ ...filters, showInStockOnly: e.target.checked });
  };
  const getStatusText = (quantity) => {
    if (quantity > 0) {
      return "En stock";
    } else {
      return "Pas en stock";
    }
  };
  const getStatusColor = (quantity) => {
    if (quantity > 0) {
      return (
        <FontAwesomeIcon
          className="iconStock"
          icon={faCircle}
          style={{ color: "#469521" }}
        />
      );
    } else {
      return (
        <FontAwesomeIcon
          className="iconStock"
          icon={faCircle}
          style={{ color: "#e32626" }}
        />
      );
    }
  };
  return (
      <div className="productCss">
        <NavBar/>
        <h1 className="title">BMX</h1>
        <div className="productCss-container">
          <div className="filter-Container">
            <div className="filter-Container-border">
              <input
                  className="search-input"
                  type="text"
                  value={filters.searchItem}
                  onChange={handleSearchChange}
                  placeholder="Rechercher"
              />
              <div className="filter-Container-input">
                <label className="filter-name">Stock</label>
                <input
                    type="checkbox"
                    checked={filters.showInStockOnly}
                    onChange={handleInStockChange}
                />
              </div>
              <FilterComponent onFilterChange={(filterType, value) => {
                setFilters({...filters, [filterType]: filters[filterType] === value ? '' : value});
              }}/>
            </div>
          </div>
          <div>
            <section className="productSection">
              {products.map((product) => {
                return (
                    <Link to={`/product/${product.id}`} className="productList" key={product.id}>
                      <div className="product-productImage">
                        <img
                            alt="imageOfProduct"
                            className="image"
                            src={product.image}
                        ></img>
                      </div>
                      <div className="productProps">
                        <h1 className="productTitle">{product.title}</h1>
                        <div className="quantitySection">
                          <p className="productQuantity">
                            {`${getStatusText(product.quantity)}`}
                          </p>
                          {getStatusColor(product.quantity)}
                        </div>
                        <p className="productPrice">{product.price}€</p>
                      </div>
                    </Link>
                );
              })}
            </section>
            <div className="pagination-controls">
              <button onClick={goToPreviousPage} disabled={filters.page === 1}>
                Précédent
              </button>
              <span>Page {filters.page}</span>
              <button onClick={goToNextPage}>
                Suivant
              </button>
            </div>
          </div>
        </div>
      </div>
  );
}

export default AllProduct;
