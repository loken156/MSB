import "../css/PricesPage.css";

function PricesPage() {
  return (
<>
    <div className = "hero-section-pricepaige">
      <h1>Box selection</h1>
      <h2>Choose affordable plans for every need</h2>
      <button className = "hero-button-pricepaige">Book</button>
    </div>

    <div className = "pricepaige-paige-container">

        <div className = "media-scroller snaps-inline">

          <div className = "media-element">
            <p className = "box-title">Box S-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">5$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>  

          <div className = "media-element">
            <p className = "box-title">Box M-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">6$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

          <div className = "media-element">
            <p className = "box-title">Box L-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">7$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

          <div className = "media-element">
            <p className = "box-title">Box XL-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">8$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

          <div className = "media-element">
            <p className = "box-title">Box XXL-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">9$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

          <div className = "media-element">
            <p className = "box-title">Box XXXL-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">10$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

          <div className = "media-element">
            <p className = "box-title">Box MEGA-size</p>
            <p className = "box-measurments">37cm x 27cm x 17cm</p>
            <img  className ="box-pictures" src = "../src/assets/box-image.png" alt = "Picture of a box"></img>
            <p className = "price-per-month">11$ box/month</p>
            <label>Select the number of boxes:
            <input type="number" id="quantity" name="quantity" min="0" max="100" value="0" />
            </label>
            <button className = "cart-button">Add to cart</button>
          </div>

        </div>
        
    </div>

</>
  );
}

export default PricesPage;
