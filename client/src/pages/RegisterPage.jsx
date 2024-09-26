import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import "../css/RegisterPage.css";

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
    <div className ="paige-container-registerpage">
      <div className = "paige-container-grid-registerpage">
        <div className = "register-section">
          {error && <p style={{ color: 'red' }}>{error}</p>}

          <form onSubmit={handleSubmit} className="register-form">
          <h1 className="heading-registerpage">Register</h1>

            <div className="form-group">
              <label htmlFor="firstName">First Name<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="firstName"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="lastName">Last Name<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="lastName"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="email">Email<br></br></label>
              <input className = "input-field-registerpage"
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="password">Password<br></br></label>
              <input className = "input-field-registerpage"
                type="password"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="phoneNumber">Phone Number<br></br></label>
              <input className = "input-field-registerpage"
                type="tel"
                id="phoneNumber"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="streetName">Street Name<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="streetName"
                value={address.streetName}
                onChange={(e) => setAddress({ ...address, streetName: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="streetNumber">Street Number<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="streetNumber"
                value={address.streetNumber}
                onChange={(e) => setAddress({ ...address, streetNumber: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="apartment">Apartment<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="apartment"
                value={address.apartment}
                onChange={(e) => setAddress({ ...address, apartment: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="zipCode">Zip Code<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="zipCode"
                value={address.zipCode}
                onChange={(e) => setAddress({ ...address, zipCode: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="zipCode">floor<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="floor"
                value={address.floor}
                onChange={(e) => setAddress({ ...address, floor: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="city">City<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="city"
                value={address.city}
                onChange={(e) => setAddress({ ...address, city: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="state">State<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="state"
                value={address.state}
                onChange={(e) => setAddress({ ...address, state: e.target.value })}
                required
              />
            </div>

            <div className="form-group">
              <label htmlFor="country">Country<br></br></label>
              <input className = "input-field-registerpage"
                type="text"
                id="country"
                value={address.country}
                onChange={(e) => setAddress({ ...address, country: e.target.value })}
                required
              />
            </div>

            <button className = "register-button" type="submit">Register</button>
          </form>

          <img className = "register-image" src = "src\assets\Magnificent Store Buddies.png" alt = "MSB Logo"></img>

        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
