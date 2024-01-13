import React, { useState, useEffect } from 'react';
import { loadStripe } from '@stripe/stripe-js';
import { CardElement, Elements, useStripe, useElements } from '@stripe/react-stripe-js';

const stripePromise = loadStripe("pk_test_51OVAt3ClaClP8ajKEFDfGk64TXh0XMKIziLzYDzsBCLl4uvqGMh5DJzMzqiLQgIpCZixCpFsrAfmcjVeAcaie8Xa00io82FHkc");
const options = {
    clientSecret: '', // Remplacez par votre client secret
    appearance: {
        theme: 'stripe',
    },
};
const CheckoutForm = () => {
    const stripe = useStripe();
    const elements = useElements();
    const [clientSecret, setClientSecret] = useState("");

    useEffect(() => {
        const token = localStorage.getItem("jwtToken");
        fetch("https://localhost:7139/RailRideBmx/Stripe/CreatePaymentIntent", { method: "POST",
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            }, })
            .then(res => res.json())
            .then(data => setClientSecret(data.clientSecret));
    }, []);

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!stripe || !elements) {
            return;
        }

        const result = await stripe.confirmCardPayment(clientSecret, {
            payment_method: {
                card: elements.getElement(CardElement),
                billing_details: {
                    name: 'Nom du Client',
                },
            },
        });

        if (result.error) {
            console.log(result.error.message);
        } else {
            if (result.paymentIntent.status === 'succeeded') {
                console.log('Paiement réussi !');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <CardElement/>
            <button type="submit" disabled={!stripe}>
                Payer
            </button>
        </form>
    );
};

const StripeWrapper = () => {
    return (
        <Elements stripe={stripePromise} options={options}>
            <CheckoutForm />
        </Elements>
    );
}

export default StripeWrapper;
