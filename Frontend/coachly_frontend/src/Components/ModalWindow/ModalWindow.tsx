import { useEffect, ReactNode } from "react";
import "./ModalWindow.css";

export interface ModalWindowProps {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
  className?: string;
}

const ModalWindow = ({
  isOpen,
  onClose,
  children,
  className = "",
}: ModalWindowProps) => {
  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === "Escape") {
        onClose();
      }
    };

    document.addEventListener("keydown", handleKeyDown);
    return () => {
      document.removeEventListener("keydown", handleKeyDown);
    };
  }, [onClose]);

  useEffect(() => {
    if (isOpen) {
      // Заборонити скролінг сторінки
      document.body.style.overflow = "hidden";
      // Можна додати pointer-events: none для всіх елементів, окрім модалки, але зазвичай overflow hidden достатньо
    } else {
      document.body.style.overflow = "";
    }

    // При розмонтуванні компонента теж прибрати стилі, на всяк випадок
    return () => {
      document.body.style.overflow = "";
    };
  }, [isOpen]);

  if (!isOpen) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <section
        className={`modal-window ${className}`}
        onClick={(e) => e.stopPropagation()}
      >
        {children}
      </section>
    </div>
  );
};

export default ModalWindow;
