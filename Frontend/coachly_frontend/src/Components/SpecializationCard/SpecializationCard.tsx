import { FC } from "react";
import { Specialization } from "../../Interfaces/Specialization/SpecializationInterface";
import Card from "../CardContainer/Card";

interface Props {
  name: string;
}

const SpecializationCard: FC<Props> = ({ name }) => {
  return <Card>{name}</Card>;
};

export default SpecializationCard;
