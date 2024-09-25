import Footer from "./components/Footer";
import { Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import Prices from './pages/PricesPage';
import Services from './pages/ServicesPage';
import FAQ from './pages/FAQPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import MyAccountPage from './pages/MyAccountPage';
import AdminPage from './pages/AdminPage';
import ApiTestPage from './pages/ApiTestPage';
import BoxDateSelectionPage from "./pages/BoxDateSelectionPage";
import ContactUsPage from "./pages/ContactUsPage";
import { jwtDecode } from 'jwt-decode';
import './css/App.css';

const isAuthenticated = () => {
  return !!localStorage.getItem('token');
};

const isAdmin = () => {
  const token = localStorage.getItem('token');
  if (token) {
    const decoded = jwtDecode(token);
    return decoded.role && decoded.role.includes('Admin');
  }
  return false;
};

const PrivateRoute = ({ children }) => {
  return isAuthenticated() ? children : <Navigate to="/login" />;
};

const AdminRoute = ({ children }) => {
  return isAuthenticated() && isAdmin() ? children : <Navigate to="/" />;
};

function App() {
  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/prices" element={<Prices />} />
        <Route path="/services" element={<Services />} />
        <Route path="/faq" element={<FAQ />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/testing-apis" element={<ApiTestPage />} />
        <Route path="/box-date-selection-page" element={<BoxDateSelectionPage />} />
        <Route path="/Contact-Us" element={<ContactUsPage/>} />

        <Route
          path="/my-account"
          element={
            <PrivateRoute>
              <MyAccountPage />
            </PrivateRoute>
          }
        />

        <Route
          path="/admin"
          element={
            <AdminRoute>
              <AdminPage />
            </AdminRoute>
          }
        />
      </Routes>

      <Footer />
    </>
  );
}

export default App;
