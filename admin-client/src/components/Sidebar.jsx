import { Link, useNavigate } from 'react-router-dom';
import "../css/Sidebar.css";

function Sidebar() {
  
    return (
      <>
        <div className="sidebar">
            <h3 className="sidebar-title">Menu</h3>
            <ul className="sidebar-links">
                <li><Link to="/HomePaige">Home</Link></li>
                <li><Link to="/CustomerInformationPaige">Customer Information</Link></li>
                <li><Link to="/services">Employee Information</Link></li>
                <li><Link to="/contact">Order Information</Link></li>
                <li><Link to="/contact">Admin Maker</Link></li>
                <li><Link to="/contact">Coupon and Deals</Link></li>
            </ul>
        </div>
      </>
    )
  }
  
  export default Sidebar;