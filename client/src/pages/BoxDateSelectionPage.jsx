import Calender from "../components/Calender";
import "../css/BoxDateSelectionPage.css";

function BoxDateSelectionPage() {
    return (
<>
<div className = "hero-section-boxDateSelectionpage">
    <h1>Box Selection</h1>
    <h2>Choose plans for every need</h2>
</div>

<div className = "boxPrices-container">
        <div className = "media-scroller-boxDate-paige snaps-inline">
          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">5$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">6$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">7$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">8$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">9$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">10$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  

          <div className = "media-element-boxDate-paige">
            <p className = "box-title-boxDate-paige">Box S-size</p>
            <p className = "box-measurments-boxDate-paige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-boxDate-paige" src = "/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-boxDate-paige">25$ box/month</p>
            <label className = "number-of-boxes-boxDate-paige">Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100"/>
            </label>
            <button className = "cart-button-boxDate-paige">Add to cart</button>
          </div>  
        </div>   

        <div className = "checkout-buttons-boxDate-paige">
           <p className = "checkout-noBoxButton-boxDate-paige">Continue without box-selection</p>
           <p className = "checkout-withBoxButton-boxDate-paige">Continue</p>
        </div>
</div>

<div className = "boxDelivery-section-boxDate-paige">     
    <div className = "boxDelivery-section-date-selection-container">

        <h3 className = "boxDelivery-date-selection-header">
            Choose a date for us to deliver the empty boxes to you!
        </h3>

        <div className = "boxDelivery-date-selection-calender-section">
            <div className = "calender">
            <Calender/>
            </div>
            <div className = "boxDelivery-timeslot">
                <p className = "boxDelivery-timeslot-text">Select box delivery timeslot:</p>
                <input type="checkbox" id = "boxDelivery-timeslot-1" name = "boxDelivery-timeslot-1" value = "11-2"></input>
                <label for = "boxDelivery-timeslot-1">11 am - 2pm</label><br />
                <input type="checkbox" id = "boxDelivery-timeslot-2" name = "boxDelivery-timeslot-2" value = "11-2"></input>
                <label for = "boxDelivery-timeslot-2">2pm -4pm</label><br />
                <input type="checkbox" id = "boxDelivery-timeslot-3" name = "boxDelivery-timeslot-3" value = "11-2"></input>
                <label for = "boxDelivery-timeslot-3">4pm -6pm</label><br />
                <input type="checkbox" id = "boxDelivery-timeslot-4" name = "boxDelivery-timeslot-4" value = "11-2"></input>
                <label for = "boxDelivery-timeslot-4">6pm -8pm</label><br />
                <p className = "boxDelivery-selected-time-result">You have selected:</p>
            </div>
        </div>

        <button className = "boxDelivery-date-selection-button">
            Proceed to the next step!
        </button>

    </div>
</div>

<div className = "boxCollect-section-boxDate-paige">
    <div className = "boxCollect-section-date-selection-container">

        <h3 className = "boxCollect-date-selection-header">
        Choose a date for us to collect your packed boxes/items!
        </h3>

        <div className = "boxCollect-date-selection-calender-section">
            <div className = "calender">
            <Calender/>
            </div>
            <div className = "boxCollect-timeslot">
                <p className = "boxCollect-timeslot-text">Select box delivery timeslot:</p>
                <input type="checkbox" id = "boxCollect-timeslot-1" name = "boxCollect-timeslot-1" value = "11-2"></input>
                <label for = "boxCollect-timeslot-1">11 am - 2pm</label><br />
                <input type="checkbox" id = "boxCollect-timeslot-2" name = "boxCollect-timeslot-2" value = "11-2"></input>
                <label for = "boxCollect-timeslot-2">2pm -4pm</label><br />
                <input type="checkbox" id = "boxCollect-timeslot-3" name = "boxCollect-timeslot-3" value = "11-2"></input>
                <label for = "boxCollect-timeslot-3">4pm -6pm</label><br />
                <input type="checkbox" id = "boxCollect-timeslot-4" name = "boxCollect-timeslot-4" value = "11-2"></input>
                <label for = "boxCollect-timeslot-4">6pm -8pm</label><br />
                <p className = "boxCollect-selected-time-result">You have selected:</p>
            </div>
        </div>

        <button className = "boxCollect-date-selection-button">
            Proceed to the next step!
        </button>

    </div>
</div>
</>
    );
}
  
  export default BoxDateSelectionPage;