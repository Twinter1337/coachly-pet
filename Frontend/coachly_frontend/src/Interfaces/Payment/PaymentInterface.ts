import { CurrencyType } from "../Enums/CurrensyType";
import { PaymentMethod } from "../Enums/PaymentMethod";
import { PaymentStatus } from "../Enums/PaymentStatus";

export interface Payment {
  id: number;
  amount: number;
  paymentDate: string;
  method: PaymentMethod;
  status: PaymentStatus;
  currency: CurrencyType;
}
