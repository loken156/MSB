import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import '../css/CheckoutPage.css';

const CheckoutPage = () => {
  const items = [
    { type: "Box M-size", amount: 2, pricePerBox: 9, totalPerBox: 18 },
    { type: "Box L-size", amount: 1, pricePerBox: 11, totalPerBox: 22 }
  ];

  const total = items.reduce((acc, item) => acc + item.totalPerBox, 0);

  return (
    <>
      <div className="checkout_header">
        <h1 className="checkout_h1">Check out</h1>
        <p className="checkout_h2">Secure Your Space with Effortless Checkout</p>
      </div>

      <div className="checkout_content">
        <table className="checkout_table">
          <thead>
            <tr>
              <th>Type of Box</th>
              <th>Amount</th>
              <th>Box Plan</th>
              <th>Total per Box</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item, index) => (
              <tr key={index}>
                <td>{item.type}</td>
                <td>{item.amount}</td>
                <td>{item.pricePerBox}$ per month</td>
                <td>{item.totalPerBox}$ per month</td>
              </tr>
            ))}
          </tbody>
        </table>

        <div className="checkout_total">
          <Link className='add_more_boxes' to="/services">Add More Boxes</Link>            
          <div className="total_summary">
            <span>Total: </span>
            <span>{total}$ per Month</span>
          </div>
          <Link className='payment_button' to="/shipping">Payment</Link>
        </div>
      </div>
    </>
  );
};

export default CheckoutPage;
