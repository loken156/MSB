import React from 'react';
import '../css/ProgressBar.css';

const ProgressBar = ({ activeStep }) => {
  return (
    <div className='progress_bar'>
      <div className="progress_steps">
        <div className={`step ${activeStep >= 1 ? 'active' : ''}`}>
          <div className="circle">1</div>
        </div>
        <div className="line"></div>
        <div className={`step ${activeStep >= 2 ? 'active' : ''}`}>
          <div className="circle">2</div>
        </div>
        <div className="line"></div>
        <div className={`step ${activeStep >= 3 ? 'active' : ''}`}>
          <div className="circle">3</div>
        </div>
      </div>

      <div className="progress_labels">
        <div className={`step ${activeStep >= 1 ? 'active' : ''}`}>
          <p>Shipping</p>
        </div>
        <div className={`step ${activeStep >= 2 ? 'active' : ''}`}>
          <p>Payment</p>
        </div>
        <div className={`step ${activeStep >= 3 ? 'active' : ''}`}>
          <p>Review</p>
        </div>
      </div>
    </div>
  );
};

export default ProgressBar;
