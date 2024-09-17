import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const RegisterPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState({
    streetName: '',
    streetNumber: '',
    apartment: '',
    zipCode: '',
    floor: '',
    city: '',
    state: '',
    country: '',
    latitude: 0,
    longitude: 0
  });
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('https://localhost:5001/api/Account/Register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email,
          password,
          firstName,
          lastName,
          phoneNumber,
          address
        }),
      });

      if (response.ok) {
        navigate('/login');
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Registration failed');
      }
    } catch (error) {
      setError('An error occurred during registration.');
    }
  };

  return (
    <div className="registerContainer">
      <h1 className="heading">Register</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <form onSubmit={handleSubmit} className="registerForm">
        <div className="formGroup">
          <label htmlFor="firstName">First Name:</label>
          <input
            type="text"
            id="firstName"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="lastName">Last Name:</label>
          <input
            type="text"
            id="lastName"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="phoneNumber">Phone Number:</label>
          <input
            type="tel"
            id="phoneNumber"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
            required
          />
        </div>

        <div className="formGroup">
          <label htmlFor="streetName">Street Name:</label>
          <input
            type="text"
            id="streetName"
            value={address.streetName}
            onChange={(e) => setAddress({ ...address, streetName: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="streetNumber">Street Number:</label>
          <input
            type="text"
            id="streetNumber"
            value={address.streetNumber}
            onChange={(e) => setAddress({ ...address, streetNumber: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="apartment">Apartment:</label>
          <input
            type="text"
            id="apartment"
            value={address.apartment}
            onChange={(e) => setAddress({ ...address, apartment: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="zipCode">Zip Code:</label>
          <input
            type="text"
            id="zipCode"
            value={address.zipCode}
            onChange={(e) => setAddress({ ...address, zipCode: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="zipCode">floor:</label>
          <input
            type="text"
            id="floor"
            value={address.floor}
            onChange={(e) => setAddress({ ...address, floor: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="city">City:</label>
          <input
            type="text"
            id="city"
            value={address.city}
            onChange={(e) => setAddress({ ...address, city: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="state">State:</label>
          <input
            type="text"
            id="state"
            value={address.state}
            onChange={(e) => setAddress({ ...address, state: e.target.value })}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="country">Country:</label>
          <input
            type="text"
            id="country"
            value={address.country}
            onChange={(e) => setAddress({ ...address, country: e.target.value })}
            required
          />
        </div>

        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default RegisterPage;
