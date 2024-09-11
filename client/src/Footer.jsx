import "./Footer.css";

function Footer() {
    return(
        <footer>
            <div className = "footer-top">

                <div className = "contact-boxes">
                <h2 className = "footer-header">Email</h2>
                <img className = "contact-logos"  alt = "mail logo" src ="./src/assets/icon-mail.png"></img>
                <a className = "footer-links-top" href="mailto:MSB@email.com">MSB@email.com</a>
                </div>

                <div className = "contact-boxes">
                <h2 className = "footer-header">Phone</h2>
                <img className = "contact-logos"  alt = "phone logo" src ="./src/assets/icon-phone.png"></img>
                <a className = "footer-links-top" href="tel:+12332145600">+123 321 456 00</a>
                </div>

                <div className = "contact-boxes">
                <h2 className = "footer-header">WhatsApp</h2>
                <img className = "contact-logos"  alt = "phone logo" src ="./src/assets/icon-phone.png"></img>
                <a  className = "footer-links-top" href="https://wa.me/+12332145600">+123 321 456 00</a>
                </div>
                
                <div className = "contact-boxes">
                <h2 className = "footer-header">Adress</h2>
                <img className = "contact-logos"  alt = "map logo" src ="./src/assets/icon-map.png"></img>
                <a className = "footer-links-top" href="https://www.google.com/maps?q=123+Main+st,+1233">123 Main st, 1233</a>
                </div>

                </div>

                <div className = "footer-bottom">

                <div className = "footer-logo">
                    <p>Magnificent<br></br>Store Buddies</p>
                </div>

                <div className = "footer-links">
                <a className = "footer-links-bottom" href="Account-Page">Your Account</a>
                <a className = "footer-links-bottom" href="FAQ-Page">FAQ</a>
                <a className = "footer-links-bottom" href="Services-Page">Services</a>
                <a className = "footer-links-bottom" href="#Prices-Page">Prices</a>
                <a className = "footer-links-bottom" href="#Contact-Us-Page">Contact Us</a>
                </div>

                </div>
                
        </footer>
    );
}

export default Footer