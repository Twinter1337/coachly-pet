import { UserRole } from "../Enums/UserRole";

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  UserRole: UserRole;
  createdAt: Date;
}
