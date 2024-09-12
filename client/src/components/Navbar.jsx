import { Link, useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

const Navbar = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('token'); // Удаляем токен из localStorage
    navigate('/login'); // Перенаправляем на страницу логина
  };

  // Проверка роли пользователя
  const isAdmin = () => {
    const token = localStorage.getItem('token');
    if (token) {
      const decoded = jwtDecode(token); // Декодируем токен
      return decoded.role && decoded.role.includes('Admin'); // Проверяем наличие роли Admin
    }
    return false;
  };

  return (
    <nav>
      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/prices">Prices</Link></li>
        <li><Link to="/services">Our Services</Link></li>
        <li><Link to="/faq">FAQ</Link></li>
        {isAdmin() && <li><Link to="/admin">Admin</Link></li>} {/* Ссылка видна только админам */}
        <li>
          {localStorage.getItem('token') ? (
            <>
              <Link to="/my-account">My Account</Link>
              <button onClick={handleLogout} style={{ marginLeft: '10px' }}>Logout</button>
            </>
          ) : (
            <Link to="/login">Login</Link>
          )}
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
