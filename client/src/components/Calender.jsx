import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import "../css/Calender.css";
import React, { useState } from 'react';

function Calender() {

    return (
      <div className="calender">
        <Calendar 
            showFixedNumberOfWeeks={true}
        />
      </div>
    );
  }
  
  export default Calender;
  