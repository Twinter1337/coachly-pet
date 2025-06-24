import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./App/App";
import { loadStripe } from "@stripe/stripe-js";
import { Elements } from "@stripe/react-stripe-js";

const stripePromise = loadStripe(
  "pk_test_51RPsbRRxQL5T5ZcXonTy6S0fLmVl7TsxWqAHBHROCfzLJSDILJkOYrdK6Ik7xXbLFTDvvdylueBkx3ig8hL1YF1800iNSZIE6M"
);

ReactDOM.createRoot(document.getElementById("root")!).render(
  // <React.StrictMode>
  <Elements stripe={stripePromise}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Elements>
  // </React.StrictMode>
);
