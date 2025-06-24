import { SessionStatus } from "../Enums/SessionStatus";
import { SessionType } from "../Enums/SessionType";

export interface Session {
  id: number;
  trainerId: number;
  durationMinutes: number;
  maxParticipants: number;
  price: number;
  scheduledAt: string;
  paymentId: number | null;
  status: SessionStatus;
  type: SessionType;
}
