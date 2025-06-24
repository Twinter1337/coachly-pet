import { useEffect, useState } from "react";
import ModalWindow from "../ModalWindow";
import "./TrainerShedule.css";
import { getSessionsByTrainerId } from "../../../Services/SessionService";
import { TrainerWithUser } from "../../../Interfaces/Trainer/TrainerWithUserInterface";
import { Session } from "../../../Interfaces/Session/SesionInterface";
import { createPaymentRequest } from "../../../Services/PaymentService";
import { PaymentRequest } from "../../../Interfaces/Payment/PaymentRequestInterface";
import { CardElement, useElements, useStripe } from "@stripe/react-stripe-js";
import { useUser } from "../../../Contexts/User/UserContext";

interface TrainerSheduleProps {
  isOpen: boolean;
  onClose: () => void;
  trainer?: TrainerWithUser;
}

const TrainerShedule = ({ isOpen, onClose, trainer }: TrainerSheduleProps) => {
  const [schedule, setSchedule] = useState<Session[] | null>(null);
  const [loading, setLoading] = useState(false);
  const [selectedEntry, setSelectedEntry] = useState<Session | null>(null);
  const stripe = useStripe();
  const elements = useElements();
  const { user } = useUser();

  useEffect(() => {
    if (!isOpen || !trainer?.trainer?.id) return;

    const fetchSchedule = async () => {
      setLoading(true);
      const scheduleRes = await getSessionsByTrainerId(trainer.trainer.id);
      setSchedule(scheduleRes);
      setSelectedEntry(null);
      setLoading(false);
    };

    fetchSchedule();
  }, [isOpen, trainer]);

  const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedId = Number(event.target.value);
    const entry = schedule?.find((s) => s.id === selectedId) || null;
    setSelectedEntry(entry);
  };

  if (!trainer) return null;

  return (
    <ModalWindow isOpen={isOpen} onClose={onClose}>
      <div className="trainer-schedule">
        <h2>
          Schedule for {trainer.user.firstName} {trainer.user.lastName}
        </h2>

        {loading ? (
          <p>Loading sessions...</p>
        ) : schedule && schedule.length > 0 ? (
          <>
            <select
              onChange={handleChange}
              value={selectedEntry?.id ?? ""}
              style={{ padding: "5px", fontSize: "1rem", width: "100%" }}
            >
              <option value="" disabled>
                Select a session
              </option>
              {schedule.map((entry) => (
                <option key={entry.id} value={entry.id}>
                  {formatDateTime(entry.scheduledAt)} | {entry.type}
                </option>
              ))}
            </select>

            {selectedEntry && (
              <div
                className="selected-entry-details"
                style={{ marginTop: "15px" }}
              >
                <h3>Selected Session Entry:</h3>
                <p>
                  <strong>Type:</strong> {selectedEntry.type}
                </p>
                <p>
                  <strong>Duration:</strong> {selectedEntry.durationMinutes}{" "}
                  minutes
                </p>
                <div className="payment-controls" style={{ marginTop: "15px" }}>
                  <h3>Payment</h3>
                  <form
                    onSubmit={async (e) => {
                      e.preventDefault();
                      if (!stripe || !elements || !selectedEntry) return;

                      const res = await fetch(
                        "http://localhost:5192/api/StripePayment/create-intent",
                        {
                          method: "POST",
                          headers: { "Content-Type": "application/json" },
                          body: JSON.stringify({
                            amount: selectedEntry.price ?? 0,
                            currency: "usd",
                            sessionId: selectedEntry.id,
                            userId: user?.id,
                          }),
                        }
                      );

                      const data = await res.json();
                      const clientSecret = data.clientSecret;

                      const result = await stripe.confirmCardPayment(
                        clientSecret,
                        {
                          payment_method: {
                            card: elements.getElement(CardElement)!,
                          },
                        }
                      );

                      if (result.error) {
                        alert("Payment failed: " + result.error.message);
                      } else {
                        if (result.paymentIntent?.status === "succeeded") {
                          alert("Payment succeeded!");
                        }
                      }
                    }}
                  >
                    <div className="card-field">
                      <CardElement />
                    </div>
                    <button
                      type="submit"
                      disabled={!stripe}
                      className="pay-button"
                    >
                      Pay Now
                    </button>
                  </form>
                </div>
              </div>
            )}
          </>
        ) : (
          <p>No sessions available.</p>
        )}
      </div>
    </ModalWindow>
  );
};

export default TrainerShedule;

const formatDateTime = (isoString: string) => {
  const date = new Date(isoString);
  return date.toLocaleString("uk-UA", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  });
};
