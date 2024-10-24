import { useState } from 'react'
import { Routes, Route, Navigate, Router, Link, useNavigate } from 'react-router-dom';
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './css/App.css'
import Sidebar from './components/Sidebar';
import CustomerInformationPaige from './pages/CustomerInformationPaige';
import HomePaige from './pages/HomePaige';
import WarehousePage from "./pages/WarehousePage";

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    
      <Sidebar />

      <Routes>
        <Route path="/HomePaige" element={<HomePaige />} />
        <Route path="/CustomerInformationPaige" element={<CustomerInformationPaige />} />
        <Route path="/WarehousePage" element={<WarehousePage />} />
      </Routes>

    </>
  )
}

export default App
