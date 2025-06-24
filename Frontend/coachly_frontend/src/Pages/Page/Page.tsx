import React, { ReactNode } from "react";
import "./Page.css";

interface PageProps {
  children: ReactNode;
  className?: string;
}

const Page = ({ children, className = "" }: PageProps) => {
  return <div className={`page-container ${className}`}>{children}</div>;
};

export default Page;
