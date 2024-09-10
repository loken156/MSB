import { Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import Prices from './pages/PricesPage';
import Services from './pages/ServicesPage';
import FAQ from './pages/FAQPage';
import './App.css';



function App() {
  return (
    <>
      <Navbar />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/prices" element={<Prices />} />
        <Route path="/services" element={<Services />} />
        <Route path="/faq" element={<FAQ />} />
      </Routes>
    </>
  );
}

export default App;
