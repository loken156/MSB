import React from 'react';
import "../css/ConfirmationPage.css";
import ProgressBar from '../components/ProgressBar'; // Импортируем компонент прогресс-бара
import CartSummary from '../components/CartSummary';  // Импортируем компонент корзины
import DatesSummary from '../components/DatesSummary';  // Импортируем новый компонент для дат
import { Link, useNavigate } from 'react-router-dom';


const ConfirmationPage = () => {
  return (
    <>
      <div className="confirmation_header">
        <h1 className="h1_confirmation">All done!</h1>
      </div>

      <div className="confirmation_content">
        <ProgressBar activeStep={3} />

        <div className='boxes_confirmation'>

            {/* Компонент корзины */}
            <CartSummary />      

            <div className='right_content_confirmation'>
                {/* Новый компонент с датами */}
                <DatesSummary /> 

                <div className='buttons_confirmation'>
                    <Link className='service_button_confirmation button_confirmation' to="/services">Discover More About Our Services!</Link>                     
                    <Link className='home_button_confirmation button_confirmation' to="/">Back to Home Page!</Link>
                </div>
            </div>
        </div>
      </div>
    </>
  );
};

export default ConfirmationPage;
