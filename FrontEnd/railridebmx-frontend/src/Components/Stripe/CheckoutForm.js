import React from 'react';
import { useStripe, useElements, PaymentElement, AddressElement  } from '@stripe/react-stripe-js';

const CheckoutForm = ({ clientSecret, cartId }) => {
    const stripe = useStripe();
    const elements = useElements();

    const sendAddressToServer = async (stripeAddress) => {
        const addressData = {
            addressId: stripeAddress.id, // Supposons que Stripe vous donne un ID unique pour l'adresse
            name: stripeAddress.name,
            line1: stripeAddress.address.line1,
            line2: stripeAddress.address.line2,
            city: stripeAddress.address.city,
            state: stripeAddress.address.state,
            country: stripeAddress.address.country,
            postalCode: stripeAddress.address.postal_code
        };
        console.log(cartId)

        try {
            const response = await fetch(`https://localhost:7139/RailRideBmx/Address/AddAddress?cartId=${cartId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(addressData)
            });

            const responseData = await response.ok;
            console.log('Adresse envoyée avec succès:', responseData);
        } catch (error) {
            console.error('Erreur lors de l\'envoi de l\'adresse:', error);
        }
    };


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
            await sendAddressToServer(value);
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
