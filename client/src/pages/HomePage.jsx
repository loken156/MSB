
import { Link, useNavigate } from 'react-router-dom';
import "../css/HomePage.css";
import { useState } from "react";

const data = [
  {
    question: "How does the order and delivery process work?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What payment methods do you accept",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "Can I cancel or change my order after placing it?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What are your return and exchange policies?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What should I do if I encounter issues with my order?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "How does the order and delivery process work?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What payment methods do you accept?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "Can I cancel or change my order after placing it?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What are your return and exchange policies?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  {
    question: "What should I do if I encounter issues with my order?",
    answer: "Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!Heres the answer text!",
  },
  ]

function HomePage() {

  const [selected, setSelected] = useState(null)

  const toogle = (i) => {
    if (selected === i) {
      return setSelected(null)
    }
  
    setSelected(i)
  
  }

  return (
<>
    
      <div className = "hero-section-homepaige">
        <div className = "header-text-homepaige">
        <h1>Reimagine Space.</h1>
        <h1>The Magnificent way.</h1>
        <h2>Declutter your living space with us!</h2>
        <button className = "hero-button-homepaige">Book</button>
        </div>
        <img className = "header-image-homepaige" src = "\image_1 1.png"></img>
      </div>



      <div className = "services-section-container">
        <div className = "services-section-position">

          <article className="services-cards">
            <div className = "service-cards-text">
              <h3>General Delivery Services</h3>
              <p>Our last mile logistics solutions offer delivery<br />
                on demand with white glove service to ensure<br />
                that items are transported safely</p>
                <a href = "#LearnMorePage">Learn More</a>
            </div>
            <img className = "services-images-1" src = "\Services-cards-Image-1.png"></img>
          </article>
          <article className="services-cards">
            <div className = "service-cards-text">
              <h3>Valet Box Storage</h3>
              <p>Magnificent Store Buddies will transport<br />
                your belongings to a storage facility. All<br />
                our warehouses are equipped with 24/7<br />
                air-conditioning to keep your belongings<br />
                dry safe and secure.</p>
                <a href = "#LearnMorePage">Learn More</a>
            </div>
            <img className = "services-images-2" src = "\Services-cards-Image-2.png.png"></img>
          </article>
          <article className="services-cards">
            <div className = "service-cards-text">
              <h3>Repair of items</h3>
              <p>Have an item that needs repair?<br />
                Request a quote now!</p>
                <a href = "#LearnMorePage">Learn More</a>
            </div>
            <img className = "services-images-3" src = "\Services-cards-Image-3.png.png"></img>
          </article>
          <article className="services-cards">
            <div className = "service-cards-text">
              <h3>Request Return</h3>
              <p>Upon Request, Magnificent Store<br />
                Buddies will return stored items to you<br />
                using our in-house delivery service right<br />
                to your doorstep.<br />
                convenience at best!</p>
                <a href = "#LearnMorePage">Learn More</a>
            </div>
            <img className = "services-images-4" src = "\Services-cards-Image-4.png.png"></img>
          </article>

        </div> 
      </div>

      <div className = "what-we-do-section-container">
        <div className = "what-we-do-section-position">

          <h3 className = "What-we-do-header-1">What do we do?</h3>
          <div className = "What-we-do-header-2">
            <p className = "What-we-do-header-text-1">We provide valet<br />
              Storage Solutions</p>
              <img className = "what-we-do-image-1" src = "\Services-cards-Image-1.png"></img>
              <p className = "What-we-do-header-text-2">Our warehouses are the perfect buddy<br />
                location for you to store away unwanted<br />
                items. Homes deserve space too!</p>
          </div>
          <div className = "what-we-do-section-cards">
            <article className = "work-cards-homepaige-1">
              <div className = "card-header">
                <h2>Store</h2>
                <img className = "card-header-image" src = "\what-we-do-cards-2.png" alt = "MSB collecting"></img>
              </div>
                <p>
                  1. Select the items you want to store<br />
                  2. Choose a suitible box plan<br />
                  3. Arrange for a pickup to collect the boxes<br />
                  for storage and pickup</p>
            </article>
            <article className = "work-cards-homepaige-2">
            <div className = "card-header">
                <h2>Collect</h2>
                <img className = "card-header-image" src = "\what-we-do-cards-3.png" alt = "Warehouse"></img>
              </div>
              <p>
                Magnificent Store Buddies will transport your belongings<br></br>
                to a storage facility. All our warehouses are equipped with <br></br>
                24/7 air-conditioning to keep your belongings dry, safe and secure.
              </p>
            </article>
            <article className = "work-cards-homepaige-3">
            <div className = "card-header">
                <h2>Request Return</h2>
                <img className = "card-header-image" src = "\what-we-do-cards-1.png" alt = "Store Buddies loading cargo"></img>
              </div>
              <p>
                Upon Request, Magnificent +Store Buddies will return stored items<br></br>to you using our in-house delivery service right to your doorstep.<br></br>
                Convenience at best! 
              </p>
            </article>
          </div>
          <div className = "services-buttons">
            <button className = "services-button-1">Prices</button>
            <button className = "services-button-2">Book</button>
          </div>
            
        </div>
      </div>

      <div className = "how-we-work-section-container-homepaige">

        <h3 className = "grid-title-homepaige">How we work</h3>

        <div className = "how-we-work-section-homepaige">

            <article className="work-cards-1-homepaige">
            <span className="number-badge">1</span>
                <p className = "how-we-work-text">Select the items<br></br>you want to store</p>
                <img  className = "work-images-homepaige" src ="/icon-mouse.png" alt = "mouse logo"></img>
            </article>
            <p className = "arrows-1-homepaige">
                &rarr;
            </p>
            <article className="work-cards-2-homepaige">
            <span className="number-badge">2</span>
                <p className = "how-we-work-text"> Choose the best box<br></br>plan for your needs</p>
                <img  className = "work-images-homepaige" src ="/icon-storage.png" alt = "storage logo"></img>
            </article>
            <p className = "arrows-2-homepaige">
                &rarr;
            </p>
            <article className="work-cards-3-homepaige">
            <span className="number-badge">3</span>
                <p className = "how-we-work-text">Schedule a convinient<br></br>date for us to pick up<br></br>your items for storage</p>
                <img  className = "work-images-homepaige" src ="/icon-schecuele.png" alt = "schecuele logo"></img>
            </article>
            <p className = "arrows-3-homepaige">
                &uarr;
            </p>
            <p className = "arrows-4-homepaige">
                &larr;
            </p>
            <article className="work-cards-4-homepaige">
            <span className="number-badge">6</span>
                <p className = "how-we-work-text">Upon request, we'll<br></br>deliver your items<br></br>straight to your doorstep</p>
                <img  className = "work-images-homepaige" src ="/icon-clock-truck.png" alt = "clock truck logo"></img>
            </article>
            <p className = "arrows-5-homepaige">
                &larr;
            </p>
            <article className="work-cards-5-homepaige">
            <span className="number-badge">5</span>
                <p className = "how-we-work-text">Enjoy extras, including<br></br>repairs, disposal of<br></br>unwanted items, and<br></br>much more</p>
                <img  className = "work-images-homepaige" src ="/icon-repair.png" alt = "repair logo"></img>
            </article>
            <p className = "arrows-6-homepaige">
                &darr;
            </p>
            <article className="work-cards-6-homepaige">
            <span className="number-badge">4</span>
                <p className = "how-we-work-text">We will safely transport<br></br>your belongings to our<br></br>secure, climate-<br></br>controlled warehouse</p>
                <img  className = "work-images-homepaige" src ="/icon-truck.png" alt = "truck logo"></img>
            </article>
        </div>

    </div>

    <div className = "FAQ-container-homepaige">
      <div className = "wrapper-homepaige">
      <h2 className = "accordian-header-homepaige">Find answers to commonly asked<br></br> questions about our services!</h2>
      
      <div className = "accordian-homepaige">

        {data.map((item, i) =>(
            <div className = {`item-homepaige ${selected === i ? 'selected-FAQ' : ''}`}>
                <div className = "title-homepaige" onClick={() => toogle(i)}>
                  <h2 className = "question-text-FAQ-homepaige">{item.question}</h2>
                  <span>{selected === i ? "-" : "+"}</span>
                </div>
                <div className = {selected === i ? "content-homepaige show-homepaige" : "content-homepaige"}>{item.answer}</div>
            </div>
        ))}

        </div>
      </div>
    </div>

    <div className = "newletter-container">

      <h3 className = "newsletter-header">
        Subscribe to our Newsletters!
      </h3>    
      <div className = "email-section">
          <input className = "email-input-homepaige"></input>
          <button className = "email-button-homepaige">Subscribe</button>
      </div>

    </div>

  </>
  );
}

export default HomePage;
