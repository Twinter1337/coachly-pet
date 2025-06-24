import { SessionStatus } from "../Enums/SessionStatus";
import { SessionType } from "../Enums/SessionType";

export interface SessionCreate {
  trainerId: number;
  durationTime: number;
  maxParticipants: number;
  price: number;
  scheduledAt: Date;
  paymentId: number | null;
  sessionStatus: SessionStatus;
  sessionType: SessionType;
}
