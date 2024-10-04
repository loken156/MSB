// components/MyAccountPage.jsx
import React, { useState } from 'react';
import '../css/MyAccountPage.css';
import MyDetails from '../components/MyDetails';
import MyAddressBook from '../components/MyAddressBook';
import MyOrders from '../components/MyOrders';

function MyAccountPage() {
  const [activeSection, setActiveSection] = useState('details');

  const renderContent = () => {
    switch (activeSection) {
      case 'details':
        return <MyDetails />;
      case 'address':
        return <MyAddressBook />;
      case 'orders':
        return <MyOrders />;
      default:
        return <MyDetails />;
    }
  };

  return (
    <div className="accountContainer">
      <h1 className="heading account_heading">My Account</h1>
      <p className='account_p'>Check all your information!</p>

      <div className="account_content">
        <div className="account_sidebar">
          <button onClick={() => setActiveSection('details')}>My Details</button>
          <button onClick={() => setActiveSection('address')}>My Address Book</button>
          <button onClick={() => setActiveSection('orders')}>My Orders</button>
        </div>

        <div className="account_main">
          {renderContent()}
        </div>
      </div>
    </div>
  );
}

export default MyAccountPage;
