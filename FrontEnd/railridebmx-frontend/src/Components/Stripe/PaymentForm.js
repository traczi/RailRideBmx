import React, { useState, useEffect } from 'react';
import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import CheckoutForm from './CheckoutForm';
import './Stripe.css'

const stripePromise = loadStripe('pk_test_51OVAt3ClaClP8ajKEFDfGk64TXh0XMKIziLzYDzsBCLl4uvqGMh5DJzMzqiLQgIpCZixCpFsrAfmcjVeAcaie8Xa00io82FHkc');

const PaymentForm = () => {
    const [clientSecret, setClientSecret] = useState('');
    const [cartId, setCartId] = useState('');

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        console.log(token)
        fetch("https://localhost:7139/RailRideBmx/Stripe/CreatePaymentIntent", { method: "POST",
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            }, })
            .then(res => res.json())
            .then(data => {
                setClientSecret(data.clientSecret);
                setCartId(data.stringCartId);
                console.log('Cart ID from fetch:', data.stringCartId)})
            .catch(error => {
                console.error("Error fetching client secret", error);

            });

        console.log(cartId);
    }, []);

    return clientSecret ? (
        <Elements stripe={stripePromise} options={{ clientSecret }}>
            <CheckoutForm clientSecret={clientSecret} cartId={cartId}/>
        </Elements>
    ) : (
        <div>Chargement...</div>
    );
};

export default PaymentForm;
