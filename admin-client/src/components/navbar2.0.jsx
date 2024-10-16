import { Link, useNavigate } from 'react-router-dom';
import "../css/navbar2.0.css";

const Navbar = () => {

    const navigate = useNavigate();
  
    const goToHomePage = () => {
      navigate('/')
    };
  
    const goToLoginPage = () => {
      navigate('/login');
    };
  
    const goToOrderPage = () => {
      console.log("Go to orderpage.");
    };

    const handleLogout = () => {
      localStorage.removeItem('token');
      navigate('/login');
    };
  
    const isAdmin = () => {
      const token = localStorage.getItem('token');
      if (token) {
        const decoded = jwtDecode(token);
        return decoded.role && decoded.role.includes('Admin');
      }
      return false;
    };
  
    return (
      <div className="top">
        <header>
          <div className="bar">
            <nav className="navbar">
              <div className="logo" onClick={goToHomePage}>
                <p>Magnificent Store Buddies</p>
              </div>
              <div className="nav-links-text">
                <ul className="nav-links">
                  <li><Link to="/services">Services</Link></li>
                  <li><Link to="/prices">Prices</Link></li>
                  <li><a href="#aboutus">About us</a></li>
                  <li><a href="#contact">Contact</a></li>
                </ul>
              </div>
              <div className="buttonBox">
                <div className="login" onClick={goToLoginPage}>
                  <svg width="60" height="40" xmlns="http://www.w3.org/2000/svg" viewBox="20 -20 100 120">
                    <rect x="100" y="11" width="40" height="60" stroke="black" fill="none" strokeWidth="3"/>
                    <circle cx="110" cy="41" r="5" fill="black"/>
                    <path d="M 6 30 L 50 30 L 50 17 L 80 41 L 50 65 L 50 53 L 6 53 Z" fill="black"/>
                  </svg>
                  <p>Login</p>
                </div>
                <div className="order" onClick={goToOrderPage}>
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 200 200" width="53" height="55" stroke="black" fill="none" strokeWidth="3">
                    <polygon points="50,50 150,50 175,75 75,75"/>
                    <polygon points="75,75 175,75 175,150 75,150"/>
                    <polygon points="50,50 75,75 75,150 50,125"/>
                  </svg>
                  <p>Order</p>
                </div>
              </div>
            </nav>
          </div>
        </header>
      </div>
    );
  };

  export default Navbar;