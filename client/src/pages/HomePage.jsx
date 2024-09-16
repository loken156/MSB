import "../css/HomePage.css";

function HomePage() {
  return (
<>
    
      <div className = "hero-section-homepaige">
        <h1>Reimagine Space.</h1>
        <h1>The Magnificent way.</h1>
        <h2>Declutter your living space with us!</h2>
        <button className = "hero-button-homepaige">Book</button>
      </div>

<div className = "paige-container-homepaige">

      <div className = "services-section-container">
        <div className = "services-section">

          <article className="services-cards">
            <p>Book</p>
            <img  className = "services-images-1" src ="../src/assets/icon-storage.png" alt = "storage logo"></img>
            <p className = "services-arrows">&rarr;</p>
            </article>
          <article className="services-cards">
            <p>Disposal</p>
            <img  className = "services-images-1" src ="../src/assets/icon-disposal.png" alt = "disposal logo"></img>
            <p className = "services-arrows">&rarr;</p>
          </article>
          <article className="services-cards">
            <p>Repair</p>
            <img  className = "services-images-1" src ="../src/assets/icon-repair.png" alt = "repair logo"></img>
            <p className = "services-arrows">&rarr;</p>
          </article> 

        </div> 
      </div>

      <div className = "how-we-work-section-homepaige-container">
        <div className = "how-we-work-section-homepaige-center-grid">
          <div className = "how-we-work-section-homepaige-grid">
            <article className = "work-cards-homepaige-1">
              <h2>Collect</h2>
                <ol>
                  <li>Select the items you want to store</li>
                  <li>Choose a suitible box plan</li>
                  <li>Arrange for a pickup to collect the boxes<br></br>
                      for storage and pickup</li>
                </ol>
              <button className = "work-cards-homepaige-button">Book</button>
            </article>
            <article className = "work-cards-homepaige-2">
            <h2>Store</h2>
              <p>
                Magnificent Store Buddies will transport your belongings<br></br>
                to a storage facility. All our warehouses are equipped with <br></br>
                24/7 air-conditioning to keep your belongings dry, safe and secure.
              </p>
              <button className = "work-cards-homepaige-button">Book</button>
            </article>
            <article className = "work-cards-homepaige-3">
            <h2>Request Return</h2>
              <p>
                Upon Request, Magnificent Store Buddies will return stored items<br></br>to you using our in-house delivery service right to your doorstep.<br></br>
                Convenience at best! 
              </p>
              <button className = "work-cards-homepaige-button">Book</button>
            </article>
            <img className = "work-cards-homepaige-image-1" src = "../src/assets/20200320_191503.jpg" alt = "Box"></img>
            <img className = "work-cards-homepaige-image-2" src = "../src/assets/20200320_191503.jpg" alt = "Box"></img>
            <img className = "work-cards-homepaige-image-3" src = "../src/assets/20200320_191503.jpg" alt = "Box"></img>
          </div>
          </div>
      </div>

  </div>
  </>
  );
}

export default HomePage;
