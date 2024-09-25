import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import '../css/LoginPage.css';


function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch('https://localhost:5001/api/Account/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: email,
          password: password,
        }),
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem('token', data.token);
        navigate('/');
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Invalid login attempt');
      }
    } catch (error) {
      setError('An error occurred while logging in.');
    }
  };

  return (
    <div className = "paige-container-loginpage">
      <div className = "paige-container-grid-loginpage">
        <div className = "login-section">

          <div className="login-container">
            
            <h1 className="heading">Magnificent<br></br>Store Buddies</h1>

            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit} className="loginForm">
              <div className="formGroup">
                <label className = "character-field" htmlFor="email">Email<br></br></label>
                <input className = "input-field-loginpaige"
                  type="email"
                  id="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              <div className="formGroup">
                <label className = "character-field" htmlFor="password">Password<br></br></label>
                <input className = "input-field-loginpaige"
                  type="password"
                  id="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
              </div>
              <p className="heading-2">Don't have an account? <a className = "register-link" href = "http://localhost:5175/register">Register</a></p> 
              <button  className = "login-button" type="submit">Login</button>
            </form>
          </div>

          <img className = "login-image" src = "src\assets\Magnificent Store Buddies.png" alt = "Picture of boxes"></img>

        </div>

      </div>
    </div>
  );
}

export default LoginPage;
