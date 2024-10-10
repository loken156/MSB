import React from 'react';
import '../css/DatesSummary.css';

const DatesSummary = () => {
  return (
    <div className="dates_summary">
        <div className="cart_header">
            <h2>2 Dates</h2>
            <span className="edit_link">Edit</span>
        </div>
        <hr />
        <div className="dates_item">
            <h3>Drop-off Date</h3>
            <p>1 May 2024 - 2pm-4pm</p>
        </div>
        <div className="dates_item">
            <h3>Pick-up Date</h3>
            <p>5 May 2024 - 12am-2pm</p>
        </div>
    </div>
  );
};

export default DatesSummary;
