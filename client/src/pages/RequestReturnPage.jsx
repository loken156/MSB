import Calender from "../components/Calender";
import "../css/RequestReturnPage.css";

function RequestReturnPage() {
    return (
<>
<div className = "boxReturn-section-boxDate-paige">
    <div className = "boxReturn-section-date-selection-container">

        <h3 className = "boxReturn-date-selection-header-main">
            Request Return
        </h3>
        <h2 className = "boxReturn-date-selection-header-second">
            Choose a date for us to deliver your boxes back to you!
        </h2>

        <div className = "boxReturn-date-selection-calender-section">
            <div className = "calender">
            <Calender/>
            </div>
            <div className = "boxReturn-timeslot">
                <p className = "boxReturn-timeslot-text">Select box delivery timeslot:</p>
                <input type="checkbox" id = "boxReturn-timeslot-1" name = "boxReturn-timeslot-1" value = "11-2"></input>
                <label for = "boxReturn-timeslot-1">11 am - 2pm</label><br />
                <input type="checkbox" id = "boxReturn-timeslot-2" name = "boxReturn-timeslot-2" value = "11-2"></input>
                <label for = "boxReturn-timeslot-2">2pm -4pm</label><br />
                <input type="checkbox" id = "boxReturn-timeslot-3" name = "boxReturn-timeslot-3" value = "11-2"></input>
                <label for = "boxReturn-timeslot-3">4pm -6pm</label><br />
                <input type="checkbox" id = "boxReturn-timeslot-4" name = "boxReturn-timeslot-4" value = "11-2"></input>
                <label for = "boxReturn-timeslot-4">6pm -8pm</label><br />
                <p className = "boxReturn-selected-time-result">You have selected:</p>
            </div>
        </div>

        <button className = "boxReturn-date-selection-button">
            Proceed to the next step!
        </button>

    </div>
</div>
</>
    );
}
  
  export default RequestReturnPage;