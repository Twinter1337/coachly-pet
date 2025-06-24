import { UserRole } from "../Enums/UserRole";

export interface UserCreate {
  firstName: string;
  lastName: string;
  email: string;
  passwordHash: string;
  phone: string;
  UserRole: UserRole;
}
