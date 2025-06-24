import { CurrencyType } from "../Enums/CurrensyType";

export interface PaymentRequest {
  amount: number;
  currency: CurrencyType;
  userId: number;
  sessionId: number | null;
  subscriptionId: number | null;
}
