import React from 'react';
import { Line } from 'react-chartjs-2';
import Chart from 'chart.js/auto'; // Nécessaire pour Chart.js v3

const UserDataChart = ({ userData }) => {
    const data = {
        labels: userData.map(data => data.date), // Vos dates ici
        datasets: [
            {
                label: 'Nombre d\'utilisateurs',
                data: userData.map(data => data.userCount), // Vos nombres d'utilisateurs ici
                fill: false,
                backgroundColor: 'rgb(75, 192, 192)',
                borderColor: 'rgba(75, 192, 192, 0.2)',
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

export default UserDataChart;
