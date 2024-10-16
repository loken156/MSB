import React, { useState } from 'react';
import { handleCheckout } from '../utils/Checkout.js'; // Import the utility function
import '../css/boxPriceDisplay.css';

const CheckoutBox = ({ priceId, title, price, initialQuantity = 1 }) => {
    const [quantity, setQuantity] = useState(initialQuantity);

    const incrementQuantity = () => {
        setQuantity(prevQuantity => prevQuantity + 1);
    };

    const decrementQuantity = () => {
        setQuantity(prevQuantity => (prevQuantity > 1 ? prevQuantity - 1 : 1));
    };

    return (
        <div className="box">
            <p className="box-name">{title}</p>
            <p className="box-mesurements">30cm x 30cm x 30cm</p>
            <img className ="box-picture" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className="box-price">{price}</p>
            <div className="quality-container">
                <button className="round-button" onClick={decrementQuantity}>-</button>
                <span className="quantity">{quantity}</span>
                <button className="round-button" onClick={incrementQuantity}>+</button>
            </div>
            <button
                onClick={() => handleCheckout(priceId, quantity, false)}
                className="checkout-button"
            >
                Checkout
            </button>
        </div>
    );
};
export default CheckoutBox;