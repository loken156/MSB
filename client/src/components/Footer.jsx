import "../css/Footer.css";
import { Link } from "react-router-dom";

function Footer() {
    return(
        <footer>
            <div className = "paige-container-footer">
                <div className = "footer-section-container">
                    <div className = "footer-section-container-flex-top">

                        <img className = "contact-logos"  alt = "mail logo" src ="./src/assets/icon-mail.png"></img>

                        <div className = "contact-boxes">
                        <h2 className = "footer-header">Email</h2>
                        <a className = "footer-links-top" href="mailto:MSB@email.com">MSB@email.com</a>
                        </div>

                        <img className = "contact-logos"  alt = "phone logo" src ="./src/assets/icon-phone.png"></img>

                        <div className = "contact-boxes">
                        <h2 className = "footer-header">Phone</h2>
                        <a className = "footer-links-top" href="tel:+12332145600">+123 321 456 00</a>
                        </div>

                        <img className = "contact-logos"  alt = "phone logo" src ="./src/assets/icon-phone.png"></img>

                        <div className = "contact-boxes">
                        <h2 className = "footer-header">WhatsApp</h2>
                        <a  className = "footer-links-top" href="https://wa.me/+12332145600">+123 321 456 00</a>
                        </div>
                        
                        <img className = "contact-logos"  alt = "map logo" src ="./src/assets/icon-map.png"></img>

                        <div className = "contact-boxes">
                        <h2 className = "footer-header">Adress</h2>
                        <a className = "footer-links-top" href="https://www.google.com/maps?q=123+Main+st,+1233">123 Main st, 1233</a>
                        </div>
                    </div>
                        
                    <div className = "footer-section-container-flex-bottom">
                        <div className = "footer-logo">
                            <p>Magnificent<br></br>Store Buddies</p>
                        </div>

                        <div className = "footer-links">
                        <Link className = "footer-links-bottom" to = "/Login">Your Account</Link>
                        <Link className = "footer-links-bottom" to = "/faq">FAQ</Link>
                        <Link className = "footer-links-bottom" to = "/services">Services</Link>
                        <Link className = "footer-links-bottom" to = "/prices">Prices</Link>
                        <Link className = "footer-links-bottom" to = "/Contact-Us">Contact Us</Link>
                        </div>
                    </div>
                        
                </div>
            </div>
        </footer>
    );
}

export default Footer