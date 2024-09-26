import React, { useState } from 'react';
import "../css/ShippingPage.css";

const ShippingPage = () => {
  const [isContactOpen, setIsContactOpen] = useState(true);
  const [isDeliveryOpen, setIsDeliveryOpen] = useState(false);

  // Управление состоянием для полей формы
  const [contactInfo, setContactInfo] = useState({
    phone: "",
    email: "",
    firstName: "",
    lastName: "",
  });

  const [deliveryInfo, setDeliveryInfo] = useState({
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
  const isContactValid = Object.values(contactInfo).every(value => value !== "");
  const isDeliveryValid = Object.values(deliveryInfo).every(value => value !== "");

  const toggleContact = () => setIsContactOpen(!isContactOpen);
  const toggleDelivery = () => setIsDeliveryOpen(!isDeliveryOpen);

  // Функции для обновления значений в формах
  const handleContactChange = (e) => {
    const { name, value } = e.target;
    setContactInfo((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleDeliveryChange = (e) => {
    const { name, value } = e.target;
    setDeliveryInfo((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };




  

    

  return (
    <>
      <div className="shipping_page_greeting">
        <h1>Shipping</h1>
        <h2>Almost there!</h2>
        <img className="greeting_photo" src="../src/assets/GreetingPagePhoto.jpg" alt="greeting_photo" />
        <div className="scroll-container">
          <p>Scroll down</p>
          <div className="circle">
            <img className='arrow_down' src="../src/assets/icons8-arrow-down-50.png" alt="arrow_down" />
          </div>
        </div>
      </div>

      <div className="shipping_page_content">



        <div className="progress-bar">
          <div className="step active">
            <div className="progress_circle">1</div>
            <p>Shipping</p>
          </div>
          <div className="line"></div>
          <div className="step">
            <div className="progress_circle">2</div>
            <p>Payment</p>
          </div>
          <div className="line"></div>
          <div className="step">
            <div className="progress_circle">3</div>
            <p>Review</p>
          </div>
        </div>


        {/* Contact Information Section */}
        <div className="form_section">
          <div className="section_header" onClick={toggleContact}>
            <h2>Contact Information</h2>
            <div className={`arrow ${isContactOpen ? 'arrow_up' : 'arrow_down'}`}></div>
          </div>
          {isContactOpen && (
            <div className="form_content">
              <div className="form_group">
                <label>Phone number</label>
                <input
                  type="text"
                  name="phone"
                  placeholder="+00 000 00 00 00"
                  value={contactInfo.phone}
                  onChange={handleContactChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Email</label>
                <input
                  type="email"
                  name="email"
                  placeholder="example@email.com"
                  value={contactInfo.email}
                  onChange={handleContactChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>First name</label>
                <input
                  type="text"
                  name="firstName"
                  placeholder="Name"
                  value={contactInfo.firstName}
                  onChange={handleContactChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Last name</label>
                <input
                  type="text"
                  name="lastName"
                  placeholder="Last"
                  value={contactInfo.lastName}
                  onChange={handleContactChange}
                />
                <span>Edit</span>
              </div>
              <button className="save_btn" disabled={!isContactValid}>
                Save
              </button>
            </div>
          )}
        </div>

        {/* Delivery Address Section */}
        <div className="form_section">
          <div className="section_header" onClick={toggleDelivery}>
            <h2>Delivery Address</h2>
            <div className={`arrow ${isDeliveryOpen ? 'arrow_up' : 'arrow_down'}`}></div>
          </div>
          {isDeliveryOpen && (
            <div className="form_content">
              <div className="form_group">
                <label>Street name</label>
                <input
                  type="text"
                  name="street"
                  placeholder="Example street"
                  value={deliveryInfo.street}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Building number</label>
                <input
                  type="text"
                  name="building"
                  placeholder="1"
                  value={deliveryInfo.building}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Apartment</label>
                <input
                  type="text"
                  name="apartment"
                  placeholder="1"
                  value={deliveryInfo.apartment}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Zip code</label>
                <input
                  type="text"
                  name="zip"
                  placeholder="123456"
                  value={deliveryInfo.zip}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Floor</label>
                <input
                  type="text"
                  name="floor"
                  placeholder="1"
                  value={deliveryInfo.floor}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>City</label>
                <input
                  type="text"
                  name="city"
                  placeholder="City"
                  value={deliveryInfo.city}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Province / State</label>
                <input
                  type="text"
                  name="state"
                  placeholder="State"
                  value={deliveryInfo.state}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <div className="form_group">
                <label>Country</label>
                <input
                  type="text"
                  name="country"
                  placeholder="Country"
                  value={deliveryInfo.country}
                  onChange={handleDeliveryChange}
                />
                <span>Edit</span>
              </div>
              <button className="save_btn" disabled={!isDeliveryValid}>
                Save
              </button>
            </div>
          )}
        </div>

        <button className="continue_btn save_btn" disabled={!(isContactValid && isDeliveryValid)}>
          Continue
        </button>
      </div>
    </>
  );
};

export default ShippingPage;
