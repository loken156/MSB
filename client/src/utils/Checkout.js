import { loadStripe } from '@stripe/stripe-js';

// Load your Stripe public key
const stripePromise = loadStripe('pk_test_erJ09UON7JgRje7RafKvywkw'); // Replace with your actual Stripe public key

// Single handleCheckout function that can handle both subscriptions and one-time payments
export const handleCheckout = async (priceId, quantity, isSubscription = false) => {
    try {
        const response = await fetch("https://localhost:5001/api/stripe/create-checkout-session", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                lineItems: [
                    {
                        priceId: priceId,
                        quantity: quantity
                    }
                ],
                mode: isSubscription ? "subscription" : "payment", // Set mode based on isSubscription flag
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const { sessionId } = await response.json();

        // Redirect to Stripe Checkout
        const stripe = await stripePromise;
        const result = await stripe.redirectToCheckout({
            sessionId: sessionId
        });

        if (result.error) {
            console.error(result.error.message);
        }
    } catch (error) {
        console.error("Error during checkout:", error);
    }
};
