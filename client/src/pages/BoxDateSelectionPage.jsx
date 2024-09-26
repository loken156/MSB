import Calender from "../components/Calender";
import "../css/BoxDateSelectionPage.css";

function BoxDateSelectionPage() {
    return (
<>
<div className = "hero-section-boxdateselectionpage">
    <h1>Box Pick-up Date</h1>
    <h2>Choose a Date that is convenient for you</h2>
    <button className = "hero-button-boxdateselectionpage">Book</button>
</div>

<div className = "paige-container-boxedateselectionpage">
    <div className = "paige-container-grid-boxedateselectionpage">      
        <div className = "date-selection-container">

            <h3 className = "date-selection-header">
                Choose a date for us to collect your packed boxes/items!
            </h3>

            <div className = "date-selection-calender-section">
                <div className = "calender">
                <Calender/>
                </div>
                <div className = "timeslot">
                    <p>Select box delivery timeslot:</p>
                    <input type="checkbox" id = "timeslot-1" name = "timeslot-1" value = "11-2"></input>
                    <label for = "timeslot-1">11 am - 2pm</label><br />
                    <input type="checkbox" id = "timeslot-2" name = "timeslot-2" value = "11-2"></input>
                    <label for = "timeslot-2">2pm -4pm</label><br />
                    <input type="checkbox" id = "timeslot-3" name = "timeslot-3" value = "11-2"></input>
                    <label for = "timeslot-3">4pm -6pm</label><br />
                    <input type="checkbox" id = "timeslot-4" name = "timeslot-4" value = "11-2"></input>
                    <label for = "timeslot-2">6pm -8pm</label><br />
                    <p className = "selected-time-result">You have selected:</p>
                </div>
            </div>

            <button className = "date-selection-button">
                Proceed to the next step!
            </button>

        </div>
    </div>
</div>
</>
    );
}
  
  export default BoxDateSelectionPage;