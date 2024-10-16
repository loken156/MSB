import { useState } from 'react'
import { Routes, Route, Navigate, Router, Link, useNavigate } from 'react-router-dom';
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './css/App.css'
import Navbar from './components/navbar2.0'
import Footer from './components/Footer'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <Navbar />
      <Footer />
    </>
  )
}

export default App
