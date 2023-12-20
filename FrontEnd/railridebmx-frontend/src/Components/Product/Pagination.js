import React, { useState } from "react";
import ReactPaginate from "react-paginate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircle } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";

const Pagination = ({ products }) => {
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
  console.log(products);
  const [currentPage, setCurrentPage] = useState(0);
  const per_page = 2;
  const offset = currentPage * per_page;
  const currentPageProduct = products
    .slice(offset, offset + per_page)
    .map((product) => (
      <div className="productList" key={product.id}>
        <div className="productImage">
          <img alt="imageOfProduct" className="image" src={product.image}></img>
        </div>
        <div className="productProps">
          <Link to={`/product/${product.id}`}>
            <h1 className="productTitle">{product.title}</h1>
          </Link>
          <div className="quantitySection">
            <p className="productQuantity">
              {`${getStatusText(product.quantity)}`}
            </p>
            {getStatusColor(product.quantity)}
          </div>
          <p className="productPrice">{product.price}€</p>
        </div>
      </div>
    ));
  const pageCount = Math.ceil(products.length / per_page);

  function handlePageClick({ selected: selectedPage }) {
    setCurrentPage(selectedPage);
  }
  return (
    <div>
      {currentPageProduct}
      <ReactPaginate
        previousLabel={"← Previous"}
        nextLabel={"Next →"}
        pageCount={pageCount}
        onPageChange={handlePageClick}
        containerClassName={"pagination"}
        previousLinkClassName={"pagination__link"}
        nextLinkClassName={"pagination__link"}
        disabledClassName={"pagination__link--disabled"}
        activeClassName={"pagination__link--active"}
      />
    </div>
  );
};

export default Pagination;
