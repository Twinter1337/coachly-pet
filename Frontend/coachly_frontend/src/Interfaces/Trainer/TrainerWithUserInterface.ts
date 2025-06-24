import { User } from "../User/UserInterface";
import { Trainer } from "../Trainer/TrainerInterface";

export interface TrainerWithUser {
  user: User;
  trainer: Trainer;
}
