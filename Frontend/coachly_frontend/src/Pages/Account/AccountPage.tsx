import { useUser } from "../../Contexts/User/UserContext";
import { useNavigate } from "react-router-dom";
import { use, useEffect, useState } from "react";
import "./AccountPage.css";
import Page from "../Page/Page";
import Card from "../../Components/CardContainer/Card";

const AccountPage = () => {
  const { user, isAuthorized, logout } = useUser();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthorized) {
      navigate("/login");
    }
  }, [isAuthorized]);

  return (
    <Page>
      <h1>My Account</h1>
      <div className="horizontal-line" />
      <Card className="account-card">
        {isAuthorized ? (
          <div>
            <p>Welcome, {user!.firstName}!</p>
            <p>Email: {user!.email}</p>
            <button onClick={logout}>Log out</button>
          </div>
        ) : (
          <p>Loading user information...</p>
        )}
      </Card>
    </Page>
  );
};

export default AccountPage;
