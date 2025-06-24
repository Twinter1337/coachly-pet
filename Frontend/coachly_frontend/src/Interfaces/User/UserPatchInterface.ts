import { UserRole } from "../Enums/UserRole";

export interface UserPatch {
  firstName?: string;
  lastName?: string;
  email?: string;
  passwordHash?: string;
  phone?: string;
  UserRole?: UserRole;
}
