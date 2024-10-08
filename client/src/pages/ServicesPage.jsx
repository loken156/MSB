import "../css/ServicesPage.css";

function  ServicesPage() {

return (
<>
    <div className = "hero-section-servicepaige">
        <div className = "servicepaige-header">
            <h1 className = "servicepaige-header-text">Our Services</h1>
            <h2 className = "servicepaige-header-text">Tailored Storage Solution for Every Need</h2>
        </div>

        <div className = "cycle-section-servicepaige">
            <article className="cycle-cards-servicepaige">
                <p className = "cycle-cards-text">Storage</p>
                <img  className = "cycle-images-1-servicepaige" src ="../src/assets/icon-storage.png" alt = "storage logo"></img>
                <p className = "cycle-arrows-servicepaige">&rarr;</p>
            </article>

            <article className="cycle-cards-servicepaige">
                <p className = "cycle-cards-text">Delivery</p>
                <img  className = "cycle-images-1-servicepaige" src ="../src/assets/icon-truck.png" alt = "disposal logo"></img>
                <p className = "cycle-arrows-servicepaige">&rarr;</p>
            </article>

            <article className="cycle-cards-servicepaige">
                <p className = "cycle-cards-text">Disposal</p>
                <img  className = "cycle-images-1-servicepaige" src ="../src/assets/icon-disposal.png" alt = "repair logo"></img>
                <p className = "cycle-arrows-servicepaige">&rarr;</p>
            </article>

            <article className="cycle-cards-servicepaige">
                <p className = "cycle-cards-text">Repair</p>
                <img  className = "cycle-images-1-servicepaige" src ="../src/assets/icon-repair.png" alt = "repair logo"></img>
                <p className = "cycle-arrows-servicepaige">&rarr;</p>
            </article>
        </div>
    </div>

    <div className = "boxprice-section-servicepaige">

        <h1 className = "boxprice-header-text">Choose the option that suits you!</h1>

        <div className = "media-scroller-servicepaige snaps-inline-servicepaige">

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box S-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">5$ box/month</p>
          </div>  

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box M-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">6$ box/month</p>
          </div>

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box L-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">7$ box/month</p>
          </div>

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box XL-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">8$ box/month</p>
          </div>

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box XXL-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">9$ box/month</p>
          </div>

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box XXXL-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">10$ box/month</p>
          </div>

          <div className = "media-element-servicepaige">
            <p className = "box-title-servicepaige">Box MEGA-size</p>
            <p className = "box-measurments-servicepaige">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures-servicepaige" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month-servicepaige">11$ box/month</p>
          </div>

        </div>  
    </div>

    <div className = "how-we-work-section-container">

        <h1 className = " grid-title">How we work</h1>

        <div className = "how-we-work-section">
            <article className="work-cards-1">
            <span className="number-badge-servicepaige">1</span>
                <p className = "card-text-servicepaige">Select the items<br></br>you want to store</p>
                <img  className = "work-images" src ="../src/assets/icon-mouse.png" alt = "mouse logo"></img>
            </article>
            <p className = "arrows-1">
                &rarr;
            </p>
            <article className="work-cards-2">
            <span className="number-badge-servicepaige">2</span>
                <p className = "card-text-servicepaige">Choose the best box<br></br>plan for your needs</p>
                <img  className = "work-images" src ="../src/assets/icon-storage.png" alt = "storage logo"></img>
            </article>
            <p className = "arrows-2">
                &rarr;
            </p>
            <article className="work-cards-3">
            <span className="number-badge-servicepaige">3</span>
                <p className = "card-text-servicepaige">Schedule a convinient<br></br>date for us to pick up<br></br>your items for storage</p>
                <img  className = "work-images" src ="../src/assets/icon-schecuele.png" alt = "schecuele logo"></img>
            </article>
            <p className = "arrows-3">
                &uarr;
            </p>
            <p className = "arrows-4">
                &larr;
            </p>
            <article className="work-cards-4">
            <span className="number-badge-servicepaige">6</span>
                <p className = "card-text-servicepaige">Upon request, we'll<br></br>deliver your items<br></br>straight to your doorstep</p>
                <img  className = "work-images" src ="../src/assets/icon-clock-truck.png" alt = "clock truck logo"></img>
            </article>
            <p className = "arrows-5">
                &larr;
            </p>
            <article className="work-cards-5">
            <span className="number-badge-servicepaige">5</span>
                <p className = "card-text-servicepaige">Enjoy extras, including<br></br>repairs, disposal of<br></br>unwanted items, and<br></br>much more</p>
                <img  className = "work-images" src ="../src/assets/icon-repair.png" alt = "repair logo"></img>
            </article>
            <p className = "arrows-6">
                &darr;
            </p>
            <article className="work-cards-6">
                <span className="number-badge-servicepaige">4</span>
                <p className = "card-text-servicepaige">We will safely transport<br></br>your belongings to our<br></br>secure, climate-<br></br>controlled warehouse</p>
                <img  className = "work-images" src ="../src/assets/icon-truck.png" alt = "truck logo"></img>
            </article>
        </div>
    </div>

    <div className = "something-else-container-servicepaige">

        <h1 className = "something-else-header-text">Do you already know what you need?</h1>

        <div className = "something-else-info-boxes">
            <article className = "something-else-cards">
                <h2 className = "something-else-header-text">Collect</h2>
                <img className = "something-else-images" src ="../src/assets/box-image.png" alt = "Box Image"></img>
                <p className = "something-else-text">Give us a chance to make you forget<br />
                about this headache! We will come<br />
                and help you pick up the boxes!</p>
            </article>
            <article className = "something-else-cards">
                <h2 className = "something-else-header-text">Return</h2>
                <img className = "something-else-images" src ="../src/assets/box-image.png" alt = "Box Image"></img>
                <p className = "something-else-text">Need your stuff? We'll bring it right<br />
                to your doorstep in a few clicks!</p>
            </article>
        </div>

        <h1 className = "something-else-header-text">Something more?</h1>

        <div className = "something-else-info-boxes">
        <article className = "something-else-cards">
                <h2 className = "something-else-header-text">Disposal</h2>
                <img className = "something-else-images" src ="../src/assets/box-image.png" alt = "Box Image"></img>
                <p className = "something-else-text">For thoose who want to get rid of<br />
                their things forever!</p>
            </article>          
            <article className = "something-else-cards">
                <h2 className = "something-else-header-text">Repair</h2>
                <img className = "something-else-images" src ="../src/assets/box-image.png" alt = "Box Image"></img>
                <p className = "something-else-text">If you want to give your things a<br />
                pristine lock - we will help you!</p>
            </article>
        </div>

    </div>
</>
);

}

export default ServicesPage 