import React from 'react';
import { Line } from 'react-chartjs-2';
import Chart from 'chart.js/auto';

const PaidCartsChart = ({ cartData }) => {
    const data = {
        labels: cartData.map(data => data.date), // Les dates des paiements de panier
        datasets: [
            {
                label: 'Paniers payés',
                data: cartData.map(data => data.paidCartsCount), // Les nombres de paniers payés
                fill: false,
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgba(255, 99, 132, 0.2)',
            },
        ],
    };

    const options = {
        scales: {
            y: {
                beginAtZero: true,
            },
        },
    };

    return <Line data={data} options={options} />;
};

export default PaidCartsChart;
