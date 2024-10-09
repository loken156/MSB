import React, { useState } from 'react';
import "../css/PaymentPage.css";
import ProgressBar from '../components/ProgressBar'; // Импортируем компонент прогресс-бара
import CartSummary from '../components/CartSummary';  // Импортируем компонент корзины

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

  const [sameAsDelivery, setSameAsDelivery] = useState(false);

  // Проверка на заполненность полей
  const isCardValid = Object.values(cardDetails).every(value => value !== "");
  const isBillingValid = sameAsDelivery || Object.values(billingAddress).every(value => value !== "");

  // Обработчики изменений
  const handleCardChange = (e) => {
    const { name, value } = e.target;
    setCardDetails((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleBillingChange = (e) => {
    const { name, value } = e.target;
    setBillingAddress((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  return (
    <>
      <div className="payment_header">
        <h1 className="h1_payment">Payment</h1>
        <h2 className="h2_payment">Almost there!</h2>
      </div>

      <div className="payment_content">
        <ProgressBar activeStep={2} />

        <div className='left_block_payment'>
          <div className="payment_form_and_cart">
            {/* Данные карты */}
            <div className="form_section_payment">
              <h2>Card Details</h2>
              <div className="form_group_payment cardname_input">
                <label>Cardholder Name</label>
                <input
                  type="text"
                  name="cardholderName"
                  placeholder="Full Name"
                  value={cardDetails.cardholderName}
                  onChange={handleCardChange}
                />
              </div>
              <div className='second_row_carddetail'>
                <div className="form_group_payment cardnumber_input">
                  <label>Card Number</label>
                  <input
                    type="text"
                    name="cardNumber"
                    placeholder="1111 2222 3333 4444"
                    value={cardDetails.cardNumber}
                    onChange={handleCardChange}
                  />
                </div>
                <div className="form_group_payment carddate_input">
                  <label>Expiration</label>
                  <input
                    type="text"
                    name="expiration"
                    placeholder="MM/YY"
                    value={cardDetails.expiration}
                    onChange={handleCardChange}
                  />
                </div>
                <div className="form_group_payment cardcvv_input">
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
            </div>

            {/* Адрес для биллинга */}
            <div className="form_section_payment">
              <h2>Billing Address</h2>
              <div className="form_group_payment checkbox_container">
                <label>Same address as for delivery</label>
                <input
                  type="checkbox"
                  checked={sameAsDelivery}
                  onChange={() => setSameAsDelivery(!sameAsDelivery)}
                />
              </div>

              {!sameAsDelivery && (
                <>
                  {["street", "building", "apartment", "zip", "floor", "city", "state", "country"].map((field, index) => (
                    <div className="group_billing" key={index}>
                      <div className="form_div_payment">
                        <label>{field.charAt(0).toUpperCase() + field.slice(1)}</label>
                        <input
                          type="text"
                          name={field}
                          placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
                          value={billingAddress[field]}
                          onChange={handleBillingChange}
                        />
                      </div>
                      <div className="edit_div_payment">
                        <span>Edit</span>
                      </div>
                    </div>
                  ))}
                </>
              )}
            </div>
          </div> 

          {/* Компонент корзины */}
          <CartSummary />                  
        </div>



        {/* Кнопка "Pay" */}
        <button className="pay_btn_payment" disabled={!(isCardValid && isBillingValid)}>
          Pay
        </button>
      </div>
    </>
  );
};

export default PaymentPage;
