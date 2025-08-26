import { useNavigate } from "react-router-dom";
import "./Botao.css";

export const Botao = ({ nomeBotao }) => {
  const navigate = useNavigate();

  return (
    <button className="botao" onClick={() => navigate("/galeria")}>
      {nomeBotao}
    </button>
  );
};




