import React from 'react';
import { useStripe, useElements, PaymentElement, AddressElement  } from '@stripe/react-stripe-js';

const CheckoutForm = ({ clientSecret }) => {
    const stripe = useStripe();
    const elements = useElements();

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!stripe || !elements) {
            console.log("Stripe.js n'a pas encore été initialisé.");
            return;
        }

        const result = await stripe.confirmPayment({
            elements,
            confirmParams: {
                return_url: 'http://localhost:3000/',
            },
        });

        if (result.error) {
            console.log(result.error.message);
        } else {
            if (result.paymentIntent.status === 'succeeded') {
                console.log('Le paiement a été effectué avec succès!');
            }
        }
        const addressElement = elements.getElement(AddressElement);

        const {complete, value} = await addressElement.getValue();

        if (complete) {
            console.log(value);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <AddressElement  options={{mode: 'shipping'  }} appearance={{
                theme: 'night',
                labels: 'floating'}}/>
            <PaymentElement/>
            <button type="submit" disabled={!stripe || !clientSecret}>
                Payer
            </button>
        </form>
    );
};

export default CheckoutForm;
