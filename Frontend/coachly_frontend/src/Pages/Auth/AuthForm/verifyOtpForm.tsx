import React from "react";
import { useNavigate } from "react-router-dom";
import * as userService from "../../../Services/AuthService";
import { useUser } from "../../../Contexts/User/UserContext";
import "./AuthForm.css";

interface Props {
  email: string;
  otpCode: string;
  setOtpCode: (otp: string) => void;
}

const VerifyOtpForm = ({ email, otpCode, setOtpCode }: Props) => {
  const { setUser } = useUser();
  const navigate = useNavigate();

  const handleVerifyOtp = async () => {
    try {
      const response = await userService.verifyOtp({ email, otpCode });
      if (response.user) {
        setUser(response.user, response.token);
        navigate("/my-account");
      }
    } catch {
      console.error("OTP verification failed");
    }
  };

  return (
    <div className="auth-options">
      <h2>Verify OTP</h2>
      <input
        type="text"
        placeholder="Enter OTP code"
        value={otpCode}
        onChange={(e) => setOtpCode(e.target.value)}
      />
      <button onClick={handleVerifyOtp} className="verify-otp-button">
        Verify OTP
      </button>
    </div>
  );
};

export default VerifyOtpForm;
