import "../css/PricesPage.css";
import { handleCheckout } from '../utils/Checkout.js';
import Box from '../components/boxPriceDisplay.jsx';

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

          <Box priceId="price_1Q8LzwHAui1dwuXtYTt9aZW2" title="Box S-size" price="S$15" />
          <Box priceId="price_1Q8M0tHAui1dwuXtcne0sI9N" title="Box M-size" price="S$20" />
          <Box priceId="price_1Q8MCsHAui1dwuXtvdd7ynp8" title="Box L-size" price="S$25" />
          <Box priceId="price_1Q8gkMHAui1dwuXtRxn2HU8J" title="Box XL-size" price="S$30" />

        </div>
        
    </div>

</>
  );
}


export default PricesPage;
