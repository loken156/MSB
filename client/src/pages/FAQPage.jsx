import "../css/FAQ.css";
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

function FAQPage() {

  const [selected, setSelected] = useState(null)

  const toogle = (i) => {
    if (selected === i) {
      return setSelected(null)
    }
  
    setSelected(i)
  
  }

  return (
<>

  <div className = "hero-section-FAQ">
    <h1 className ="hero-section-header">FAQs</h1>
  </div>

  <div className = "FAQ-paige-container">
    <div className = "wrapper-FAQ">
    <h2 className = "accordian-header-FAQ">Find answers to commonly asked<br></br> questions about our services!</h2>
      <div className = "accordian-FAQ">

      {data.map((item, i) =>(
          <div className = {`item-FAQ ${selected === i ? 'selected-FAQ' : ''}`}>
              <div className = "title-FAQ" onClick={() => toogle(i)}>
                <h2 className = "question-text-FAQ">{item.question}</h2>
                <span>{selected === i ? "-" : "+"}</span>
              </div>
              <div className = {selected === i ? "content-FAQ show-FAQ" : "content-FAQ"}>{item.answer}</div>
          </div>
      ))}

      </div>
    </div>
  </div>

  <div className = "email-container-FAQ">

    <h1 className = "email-section-header">Still have a question? We would love<br />
    to answer it for you!</h1>

      <div className = "email-section-FAQ">
        <div className = "callback-form-section">
          <p>Callback form</p>
          <input className = "email-input-fields-FAQ-name" placeholder="Name"></input>
          <input className = "email-input-fields-FAQ-phonenumber" placeholder="Phone-number"></input>
          <div className = "checkbox-container">
            <input type = "checkbox" id = "email-checkbox-anytime" className = "email-input-fields-FAQ-checkbox"></input>
            <label for = "email-checkbox-anytime">Anytime</label>
          </div>
          <div className = "date-and-time-selection-callback-form">
            <input type="date" className="email-input-fields-FAQ-date" placeholder="Select date" />
            <input type="time" className="email-input-fields-FAQ-time" placeholder="Select time" />
          </div>
          <button className = " call-me-button">Call me</button>

          <div className = "subscribe-to-newsletter-section">
            <p>Subscribe to the newsletters!</p>
            <input className = "email-input-fields-FAQ-newsletter" placeholder="Email"></input>
            <button className ="subscribe-button">Subscribe</button>
          </div>
        </div>

        <div className = "send-us-a-letter-section">
          <p>Send us a letter!</p>
          <input className = "email-input-fields-letter-name" placeholder="Name"></input>
          <input className = "email-input-fields-letter-email" placeholder="Email"></input>
          <textarea className = "email-input-fields-letter-question" placeholder="Question..."></textarea>
          <button className = "send-us-a-letter-button">Send</button>
        </div>
      </div>
    
  </div>

</>
  );
}

export default FAQPage;
