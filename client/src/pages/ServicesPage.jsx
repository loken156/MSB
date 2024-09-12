import "./ServicesPage.css";

function  ServicesPage() {

return (
<div className = "page-container">

    <div className = "hero-section">

        <h1>Our Services</h1>
        <h2>Tailored Storage Solution for Every Need</h2>
        <button className = "hero-button">Book</button>

    </div>

    <div className = "how-we-work-section-container">
            <div className = "grid-title">
                <p>How we work</p>
            </div>

        <div className = "how-we-work-section">



            <article className="work-cards-1">
                <p>Select the items<br></br>you want to store</p>
                <img  className = "work-images" src ="../src/assets/icon-mouse.png" alt = "mouse logo"></img>
            </article>

            <p className = "arrows-1">
                &rarr;
            </p>

            <article className="work-cards-2">
                <p>Choose the best box<br></br>plan for your needs</p>
                <img  className = "work-images" src ="../src/assets/icon-storage.png" alt = "storage logo"></img>
            </article>

            <p className = "arrows-2">
                &rarr;
            </p>

            <article className="work-cards-3">
                <p>Schedule a convinient<br></br>date for us to pick up<br></br>your items for storage</p>
                <img  className = "work-images" src ="../src/assets/icon-schecuele.png" alt = "schecuele logo"></img>
            </article>

            <p className = "arrows-3">
                &uarr;
            </p>

            <p className = "arrows-4">
                &larr;
            </p>

            <article className="work-cards-4">
                <p>Upon request, we'll<br></br>deliver your items<br></br>straight to your doorstep</p>
                <img  className = "work-images" src ="../src/assets/icon-clock-truck.png" alt = "clock truck logo"></img>
            </article>

            <p className = "arrows-5">
                &larr;
            </p>

            <article className="work-cards-5">
                <p>Enjoy extras, including<br></br>repairs, disposal of<br></br>unwanted items, and<br></br>much more</p>
                <img  className = "work-images" src ="../src/assets/icon-repair.png" alt = "repair logo"></img>
            </article>

            <p className = "arrows-6">
                &darr;
            </p>

            <article className="work-cards-6">
                <p>We will safely transport<br></br>your belongings to our<br></br>secure, climate-<br></br>controlled warehouse</p>
                <img  className = "work-images" src ="../src/assets/icon-truck.png" alt = "truck logo"></img>
            </article>
        </div>

    </div>

    <div className = "cycle-section-container">

        <div className = "cycle-section">

            <article className="cycle-cards">
                <p>Book</p>
                <img  className = "cycle-images-1" src ="../src/assets/icon-storage.png" alt = "storage logo"></img>
                <p className = "cycle-arrows">&rarr;</p>
            </article>

            <article className="cycle-cards">
                <p>Disposal</p>
                <img  className = "cycle-images-1" src ="../src/assets/icon-disposal.png" alt = "disposal logo"></img>
                <p className = "cycle-arrows">&rarr;</p>
            </article>

            <article className="cycle-cards">
                <p>Repair</p>
                <img  className = "cycle-images-1" src ="../src/assets/icon-repair.png" alt = "repair logo"></img>
                <p className = "cycle-arrows">&rarr;</p>
            </article>

        </div>

    </div>

</div>
);

}

export default ServicesPage 