import { useState } from 'react'
import Footer from "./Footer";
import ServicesPage from "./ServicesPage";
import { Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import Prices from './pages/PricesPage';
import Services from './pages/ServicesPage';
import FAQ from './pages/FAQPage';
import LoginPage from './pages/LoginPage';
import MyAccountPage from './pages/MyAccountPage';
import AdminPage from './pages/AdminPage'; // Импортируем страницу для админов
import { jwtDecode } from 'jwt-decode';

import './App.css';

// Функция для проверки авторизации
const isAuthenticated = () => {
  return !!localStorage.getItem('token'); // Проверяет наличие токена
};

// Функция для проверки роли
const isAdmin = () => {
  const token = localStorage.getItem('token');
  if (token) {
    const decoded = jwtDecode(token); // Декодируем токен
    return decoded.role && decoded.role.includes('Admin'); // Проверяем наличие роли Admin
  }
  return false;
};

// Компонент для защиты маршрутов
const PrivateRoute = ({ children }) => {
  return isAuthenticated() ? children : <Navigate to="/login" />;
};

// Компонент для защиты админских маршрутов
const AdminRoute = ({ children }) => {
  return isAuthenticated() && isAdmin() ? children : <Navigate to="/" />;
};

function App() {
  return (
    <>
      <h1>Magnificent Store Buddies</h1>
      <h2>Welcome to our Webpage</h2>
      <ServicesPage />
      <Footer />

      <Navbar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/prices" element={<Prices />} />
        <Route path="/services" element={<Services />} />
        <Route path="/faq" element={<FAQ />} />
        <Route path="/login" element={<LoginPage />} />

        {/* Защищенный маршрут для страницы "My Account" */}
        <Route
          path="/my-account"
          element={
            <PrivateRoute>
              <MyAccountPage />
            </PrivateRoute>
          }
        />

        {/* Защищенный маршрут для страницы админа */}
        <Route
          path="/admin"
          element={
            <AdminRoute>
              <AdminPage />
            </AdminRoute>
          }
        />
      </Routes>
    </>
  );
}

export default App;
