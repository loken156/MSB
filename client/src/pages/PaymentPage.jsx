import React, { useState } from 'react';
import '../css/PaymentPage.css'; 

const PaymentPage = () => {
  const [cardDetails, setCardDetails] = useState({
    cardholderName: "",
    cardNumber: "",
    expiration: "",
    cvv: "",
  });

  const [billingAddress, setBillingAddress] = useState({
    street: "",
    building: "",
    apartment: "",
    zip: "",
    floor: "",
    city: "",
    state: "",
    country: "",
  });

  // Проверка на заполненность полей
  const isCardValid = Object.values(cardDetails).every(value => value !== "");
  const isBillingValid = Object.values(billingAddress).every(value => value !== "");

  // Обработчик изменения данных карты
  const handleCardChange = (e) => {
    const { name, value } = e.target;
    setCardDetails((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  // Обработчик изменения данных для биллинга
  const handleBillingChange = (e) => {
    const { name, value } = e.target;
    setBillingAddress((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  return (
    <>
      <div className="payment_page">
        <div className="progress-bar">
          <div className="step">
            <div className="progress_circle">1</div>
            <p>Shipping</p>
          </div>
          <div className="line active"></div>
          <div className="step active">
            <div className="progress_circle">2</div>
            <p>Payment</p>
          </div>
          <div className="line"></div>
          <div className="step">
            <div className="progress_circle">3</div>
            <p>Review</p>
          </div>
        </div>

        {/* Форма оплаты */}
        <div className="form_section">
          <h2>Card Details</h2>
          <div className="form_group">
            <label>Cardholder Name</label>
            <input
              type="text"
              name="cardholderName"
              placeholder="Arsenty Streltsov"
              value={cardDetails.cardholderName}
              onChange={handleCardChange}
            />
          </div>
          <div className="form_group">
            <label>Card Number</label>
            <input
              type="text"
              name="cardNumber"
              placeholder="1111 2222 3333 4444"
              value={cardDetails.cardNumber}
              onChange={handleCardChange}
            />
          </div>
          <div className="form_group short_input">
            <label>Expiration</label>
            <input
              type="text"
              name="expiration"
              placeholder="MM/YY"
              value={cardDetails.expiration}
              onChange={handleCardChange}
            />
          </div>
          <div className="form_group short_input">
            <label>CVV</label>
            <input
              type="text"
              name="cvv"
              placeholder="123"
              value={cardDetails.cvv}
              onChange={handleCardChange}
            />
          </div>
        </div>

        {/* Адрес для биллинга */}
        <div className="form_section">
          <h2>Billing Address</h2>
          <div className="form_group">
            <label>Street name</label>
            <input
              type="text"
              name="street"
              placeholder="Koggens Grand"
              value={billingAddress.street}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Building number</label>
            <input
              type="text"
              name="building"
              placeholder="3A"
              value={billingAddress.building}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Apartment</label>
            <input
              type="text"
              name="apartment"
              placeholder="1301"
              value={billingAddress.apartment}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Zip code</label>
            <input
              type="text"
              name="zip"
              placeholder="21113"
              value={billingAddress.zip}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Floor</label>
            <input
              type="text"
              name="floor"
              placeholder="4"
              value={billingAddress.floor}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>City</label>
            <input
              type="text"
              name="city"
              placeholder="Malmo"
              value={billingAddress.city}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Province / State</label>
            <input
              type="text"
              name="state"
              placeholder="Skane"
              value={billingAddress.state}
              onChange={handleBillingChange}
            />
          </div>
          <div className="form_group">
            <label>Country</label>
            <input
              type="text"
              name="country"
              placeholder="Sweden"
              value={billingAddress.country}
              onChange={handleBillingChange}
            />
          </div>
        </div>

        {/* Кнопка оплаты */}
        <button className="pay_btn" disabled={!(isCardValid && isBillingValid)}>
          Pay
        </button>
      </div>
    </>
  );
};

export default PaymentPage;
