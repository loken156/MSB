import React, { useState } from 'react';
import "../css/ShippingPage.css";
import arrowImage from "../assets/arrow_input_fields.png"; // Импортируем изображение стрелки
import ProgressBar from '../components/ProgressBar'; // Импортируем компонент прогресс-бара



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
      <div className="shipping_greeting">
        <h1 className='h1_shipping'>Shipping</h1>
        <h2 className='h2_shipping'>Almost there!</h2>
      </div>

      <div className="shipping_content">
        <ProgressBar activeStep={1} /> {/* Передаем текущий шаг */}


        {/* Contact Information Section */}
        <div className="form_section_shipping">
          <div className="section_header_shipping" onClick={toggleContact}>
            <h2>Contact Information</h2>
            <img 
              src={arrowImage} 
              alt="Toggle arrow" 
              className={`arrow_shipping ${isContactOpen ? 'open' : ''}`} 
            />
          </div>
          <div className={`form_content_shipping ${isContactOpen ? 'open' : 'closed'}`}>
            <div className='left_side_shipping'>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Phone number</label>
                  <input
                    type="text"
                    name="phone"
                    placeholder="+00 000 00 00 00"
                    value={contactInfo.phone}
                    onChange={handleContactChange}
                  />                  
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Email</label>
                  <input
                    type="email"
                    name="email"
                    placeholder="example@email.com"
                    value={contactInfo.email}
                    onChange={handleContactChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>First name</label>
                  <input
                    type="text"
                    name="firstName"
                    placeholder="Name"
                    value={contactInfo.firstName}
                    onChange={handleContactChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Last name</label>
                  <input
                    type="text"
                    name="lastName"
                    placeholder="Last"
                    value={contactInfo.lastName}
                    onChange={handleContactChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>             
              </div>
            </div>

            <div className="save_button_container">
              <button className="save_btn_shipping" disabled={!isContactValid}>
                Save
              </button>
            </div>   
          </div>
        </div>

        {/* Delivery Address Section */}
        <div className="form_section_shipping">
          <div className="section_header_shipping" onClick={toggleDelivery}>
            <h2>Delivery Address</h2>
            <img 
              src={arrowImage} 
              alt="Toggle arrow" 
              className={`arrow_shipping ${isDeliveryOpen ? 'open' : ''}`} 
            />
          </div>
          <div className={`form_content_shipping ${isDeliveryOpen ? 'open' : 'closed'}`}>
            <div className='left_side_shipping'>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Street name</label>
                  <input
                    type="text"
                    name="street"
                    placeholder="Example street"
                    value={deliveryInfo.street}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Building number</label>
                  <input
                    type="text"
                    name="building"
                    placeholder="1"
                    value={deliveryInfo.building}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Apartment</label>
                  <input
                    type="text"
                    name="apartment"
                    placeholder="1"
                    value={deliveryInfo.apartment}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Zip code</label>
                  <input
                    type="text"
                    name="zip"
                    placeholder="123456"
                    value={deliveryInfo.zip}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Floor</label>
                  <input
                    type="text"
                    name="floor"
                    placeholder="1"
                    value={deliveryInfo.floor}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>City</label>
                  <input
                    type="text"
                    name="city"
                    placeholder="City"
                    value={deliveryInfo.city}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Province / State</label>
                  <input
                    type="text"
                    name="state"
                    placeholder="State"
                    value={deliveryInfo.state}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>

              </div>
              <div className="group_shipping">
                <div className='form_div_shipping'>
                  <label>Country</label>
                  <input
                    type="text"
                    name="country"
                    placeholder="Country"
                    value={deliveryInfo.country}
                    onChange={handleDeliveryChange}
                  />
                </div>

                <div className='edit_div_shipping'>
                  <span>Edit</span>                  
                </div>
              </div>
            </div>

            <div className="save_button_container">
              <button className="save_btn_shipping" disabled={!isDeliveryValid}>
                Save
              </button>
            </div>
          </div>
        </div>

        <button className="continue_btn save_btn_shipping" disabled={!(isContactValid && isDeliveryValid)}>
          Continue
        </button>
      </div>
    </>
  );
};

export default ShippingPage;
