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
    <h1>FAQs</h1>
    <h1>Got Questions? We got answers!</h1>
  </div>

  <div className = "FAQ-paige-container">
    <div className = "FAQ-grid-container">


      <div className = "wrapper">
      <h2 className = "accordian-header">Find answers to commonly asked<br></br> questions about our services!</h2>
        <div className = "accordian">

        {data.map((item, i) =>(
            <div className = "item">
                <div className = "title" onClick={() => toogle(i)}>
                  <h2>{item.question}</h2>
                  <span>{selected === i ? "-" : "+"}</span>
                </div>
                <div className = {selected === i ? "content show" : "content"}>{item.answer}</div>
            </div>
        ))}

        </div>
      </div>

    </div>
  </div>

</>
  );
}

export default FAQPage;
