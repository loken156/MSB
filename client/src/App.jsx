import { useState } from 'react'
import Footer from "./Footer";
import ServicesPage from "./ServicesPage";

import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <h1>Magnificent Store Buddies</h1>
      <h2>Welcome to our Webpage</h2>
      <ServicesPage />
      <Footer />
    </>

  );
}

export default App
