import { User } from "./UserInterface";
import { Trainer } from "../Trainer/TrainerInterface";

export interface VerifyResponse {
  user: User;
  trainer: Trainer | null;
  token: string;
}
