import React from 'react';
import "../css/ConfirmationPage.css";
import ProgressBar from '../components/ProgressBar';
import CartSummary from '../components/CartSummary';
import DatesSummary from '../components/DatesSummary';
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

            <CartSummary />      

            <div className='right_content_confirmation'>
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
