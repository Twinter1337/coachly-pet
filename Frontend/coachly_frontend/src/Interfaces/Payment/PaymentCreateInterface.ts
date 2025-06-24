import { CurrencyType } from "../Enums/CurrensyType";
import { PaymentMethod } from "../Enums/PaymentMethod";
import { PaymentStatus } from "../Enums/PaymentStatus";

export interface Payment {
  amount: number;
  method: PaymentMethod;
  currency: CurrencyType;
  stripePaymentId: string;
}
