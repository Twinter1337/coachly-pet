import Page from "../Page/Page";
import AuthForm from "./AuthForm/AuthForm";

import "./AuthPage.css";

const AuthPage = () => {
  return (
    <Page className="auth-page">
      <AuthForm />
    </Page>
  );
};

export default AuthPage;
