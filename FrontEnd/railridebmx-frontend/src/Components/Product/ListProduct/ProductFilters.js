import React from 'react';

const Filters = ({ filters, onFilterChange }) => {
    return (
        <div className="filters">
            {Object.keys(filters).map((filterKey) => (
                <div key={filterKey}>
                    <h3>{filterKey}</h3>
                    {filters[filterKey].map((value) => (
                        <label key={value}>
                            <input
                                type="checkbox"
                                onChange={(e) => onFilterChange(filterKey, value, e.target.checked)}
                            />
                            {value}
                        </label>
                    ))}
                </div>
            ))}
        </div>
    );
};

export default Filters;
