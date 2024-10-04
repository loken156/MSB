import { Link, useNavigate } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

const Navbar = () => {
  const navigate = useNavigate();

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

  const isAuthenticated = () => {
    return !!localStorage.getItem('token');
  };

  return (
    <nav>
      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/prices">Prices</Link></li>
        <li><Link to="/services">Our Services</Link></li>
        <li><Link to="/faq">FAQ</Link></li>
        <li><Link to="/testing-apis">API Test</Link></li>
        <li><Link to="/box-date-selection-page">Box Date Page</Link></li>
        <li><Link to="/Contact-Us">Contact Us</Link></li>
        {isAdmin() && <li><Link to="/admin">Admin</Link></li>}
        <li>
          {isAuthenticated() ? (
            <>
              <Link to="/my-account">My Account</Link>
              <button onClick={handleLogout} style={{ marginLeft: '10px' }}>Logout</button>
            </>
          ) : (
            <>
              <Link to="/login">Login</Link>
              <Link to="/register" style={{ marginLeft: '10px' }}>Register</Link>
            </>
          )}
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
