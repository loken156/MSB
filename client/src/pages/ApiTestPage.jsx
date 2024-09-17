import React, { useState } from 'react';
import '../css/ApiTestPage.css';

function ApiTestPage() {
  const [addressId, setAddressId] = useState('');
  const [addressData, setAddressData] = useState({
    streetName: '',
    streetNumber: '',
    apartment: '',
    zipCode: '',
    floor: '',
    city: '',
    state: '',
    country: '',
  });
  const [response, setResponse] = useState(null);

  const handleGetAllAddresses = async () => {
    try {
      const token = localStorage.getItem('token');
      if (!token) {
        throw new Error("User not authenticated");
      }

      const res = await fetch('https://localhost:5001/api/Address/GetAllAddresses', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`
        },
      });

      if (!res.ok) {
        throw new Error(`Error fetching addresses: ${res.status}`);
      }

      const data = await res.json();
      setResponse(data);
    } catch (error) {
      console.error('Error fetching addresses:', error.message);
      setResponse('Error fetching addresses');
    }
  };

  const handleGetAddressById = async () => {
    try {
      const res = await fetch(`https://localhost:5001/api/Address/${addressId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      const data = await res.json();
      setResponse(data);
    } catch (error) {
      setResponse('Error fetching address by ID');
    }
  };

  const handleAddAddress = async () => {
    try {
      const res = await fetch('https://localhost:5001/api/Address/AddAddress', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(addressData),
      });
      const data = await res.json();
      setResponse(data);
    } catch (error) {
      setResponse('Error adding address');
    }
  };

  const handleUpdateAddress = async () => {
    try {
      const res = await fetch(`https://localhost:5001/api/Address/${addressId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          addressId,
          ...addressData,
        }),
      });
      const data = await res.json();
      setResponse(data);
    } catch (error) {
      setResponse('Error updating address');
    }
  };

  const handleDeleteAddress = async () => {
    try {
      await fetch(`https://localhost:5001/api/Address/${addressId}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      setResponse(`Address with ID ${addressId} deleted successfully`);
    } catch (error) {
      setResponse('Error deleting address');
    }
  };

  return (
    <div className="apiTestContainer">
      <h1 className="heading">API Test Page</h1>
      <p>Welcome to the API testing page. Here you can test your API endpoints for Address management.</p>

      <div>
        <h2>Add Address</h2>
        <input
          type="text"
          placeholder="Street Name"
          value={addressData.streetName}
          onChange={(e) => setAddressData({ ...addressData, streetName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Street Number"
          value={addressData.streetNumber}
          onChange={(e) => setAddressData({ ...addressData, streetNumber: e.target.value })}
        />
        <input
          type="text"
          placeholder="Apartment"
          value={addressData.apartment}
          onChange={(e) => setAddressData({ ...addressData, apartment: e.target.value })}
        />
        <input
          type="text"
          placeholder="Zip Code"
          value={addressData.zipCode}
          onChange={(e) => setAddressData({ ...addressData, zipCode: e.target.value })}
        />
        <input
          type="text"
          placeholder="Floor"
          value={addressData.floor}
          onChange={(e) => setAddressData({ ...addressData, floor: e.target.value })}
        />
        <input
          type="text"
          placeholder="City"
          value={addressData.city}
          onChange={(e) => setAddressData({ ...addressData, city: e.target.value })}
        />
        <input
          type="text"
          placeholder="State"
          value={addressData.state}
          onChange={(e) => setAddressData({ ...addressData, state: e.target.value })}
        />
        <input
          type="text"
          placeholder="Country"
          value={addressData.country}
          onChange={(e) => setAddressData({ ...addressData, country: e.target.value })}
        />
        <button onClick={handleAddAddress}>Add Address</button>
      </div>

      <div>
        <h2>Get All Addresses</h2>
        <button onClick={handleGetAllAddresses}>Get All Addresses</button>
      </div>

      <div>
        <h2>Get Address by ID</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <button onClick={handleGetAddressById}>Get Address by ID</button>
      </div>

      <div>
        <h2>Update Address</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Street Name"
          value={addressData.streetName}
          onChange={(e) => setAddressData({ ...addressData, streetName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Street Number"
          value={addressData.streetNumber}
          onChange={(e) => setAddressData({ ...addressData, streetNumber: e.target.value })}
        />
        <input
          type="text"
          placeholder="Apartment"
          value={addressData.apartment}
          onChange={(e) => setAddressData({ ...addressData, apartment: e.target.value })}
        />
        <input
          type="text"
          placeholder="Zip Code"
          value={addressData.zipCode}
          onChange={(e) => setAddressData({ ...addressData, zipCode: e.target.value })}
        />
        <input
          type="text"
          placeholder="Floor"
          value={addressData.floor}
          onChange={(e) => setAddressData({ ...addressData, floor: e.target.value })}
        />
        <input
          type="text"
          placeholder="City"
          value={addressData.city}
          onChange={(e) => setAddressData({ ...addressData, city: e.target.value })}
        />
        <input
          type="text"
          placeholder="State"
          value={addressData.state}
          onChange={(e) => setAddressData({ ...addressData, state: e.target.value })}
        />
        <input
          type="text"
          placeholder="Country"
          value={addressData.country}
          onChange={(e) => setAddressData({ ...addressData, country: e.target.value })}
        />
        <button onClick={handleUpdateAddress}>Update Address</button>
      </div>

      <div>
        <h2>Delete Address</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <button onClick={handleDeleteAddress}>Delete Address</button>
      </div>

      {response && (
        <div>
          <h2>Response</h2>
          <pre>{JSON.stringify(response, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}

export default ApiTestPage;
