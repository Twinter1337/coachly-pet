import { PaymentRequest } from "../Interfaces/Payment/PaymentRequestInterface";
import api from "./ApiService";

export interface PaymentResponse {
  clientSecret: string;
}

export const createPaymentRequest = async (
  payment: PaymentRequest
): Promise<PaymentResponse | null> => {
  try {
    const response = await api.post<PaymentResponse>(
      "StripePayment/create-intent",
      payment
    );

    if (response.status !== 200) {
      throw new Error("Error creating payment intent");
    }

    return response.data as PaymentResponse;
  } catch (error) {
    console.error("Payment request failed:", error);
    return null;
  }
};
