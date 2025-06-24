import { useState } from "react";
import Card from "../../../Components/CardContainer/Card";
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import VerifyOtpForm from "./verifyOtpForm";
import "./AuthForm.css";

const AuthForm = () => {
  const [step, setStep] = useState(0); // 0 - login, 1 - register, 2 - OTP
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [otpCode, setOtpCode] = useState("");

  const handleBackButtonClick = () => {
    if (step === 2) {
      setStep(0);
      return;
    }
    if (step > 0) {
      setStep(step - 1);
    }
  };

  return (
    <Card>
      <div className="auth-form">
        {step === 0 && (
          <LoginForm
            email={email}
            password={password}
            setEmail={setEmail}
            setPassword={setPassword}
            setStep={setStep}
          />
        )}
        {step === 1 && (
          <RegisterForm email={email} setEmail={setEmail} setStep={setStep} />
        )}
        {step === 2 && (
          <VerifyOtpForm
            email={email}
            otpCode={otpCode}
            setOtpCode={setOtpCode}
          />
        )}
        {step > 0 && (
          <button className="back-button" onClick={handleBackButtonClick}>
            Back
          </button>
        )}
      </div>
    </Card>
  );
};

export default AuthForm;
