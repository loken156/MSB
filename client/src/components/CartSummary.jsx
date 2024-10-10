import React from 'react';
import '../css/CartSummary.css';

const CartSummary = () => {
    return (
      <div className="cart_summary">
        <div className="cart_header">
          <h2>2 Items</h2>
          <span className="edit_link">Edit</span>
        </div>
        <hr />
        <div className="cart_item">
          <img src="src\assets\box-image.png" alt="Box M-size" className="item_image" />
          <div className="item_details">
            <h3>Box M-size</h3>
            <p>9$ per Month</p>
            <p>37cm x 27cm x 17cm</p>
            <p>Quantity: 2</p>
          </div>
        </div>
        <div className="cart_item">
          <img src="src\assets\box-image.png" alt="Box L-size" className="item_image" />
          <div className="item_details">
            <h3>Box L-size</h3>
            <p>11$ per Month</p>
            <p>37cm x 27cm x 17cm</p>
            <p>Quantity: 1</p>
          </div>
        </div>
        <hr />
        <div className="cart_total">
          <span>Total:</span>
          <span>40$ per Month</span>
        </div>
      </div>
    );
  };
  
  export default CartSummary;
  