import React, { useState, useEffect } from 'react';
import "./FilterComponent.css"

const FilterComponent = ({ onFilterChange }) => {
    const [filterOptions, setFilterOptions] = useState({});
    useEffect(() => {
        const fetchFilterOptions = async () => {
            try {
                const response = await fetch('https://localhost:7139/RailRideBmx/Product/GetAllProductProperties');
                const data = await response.json();
                setFilterOptions(data);
            } catch (error) {
                console.error('Failed to fetch filter options', error);
            }
        };

        fetchFilterOptions();
    }, []);
    const handleCheckboxChange = (filterType, value) => {
        onFilterChange(filterType, value);
    };
    const filterSections = Object.entries(filterOptions).map(([filterType, options]) => {
        return (
            <div key={filterType} className="filterSection">
                <h3 className="filter-title">{filterType}</h3>
                {options.map((option) => (
                    <div className="filter-Container-input" key={option}>
                        <label className="filter-name" htmlFor={`input-${option}`}>{option}</label>
                        <input id={`input-${option}`}
                            className="filter-input"
                            type="checkbox"
                            onChange={() => handleCheckboxChange(filterType, option)}
                        />
                    </div>
                ))}
            </div>
        );
    });

    return <div className="filterComponent">{filterSections}</div>;
};

export default FilterComponent;
