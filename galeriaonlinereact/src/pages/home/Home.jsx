import { Botao } from "../../components/botao/Botao";
import "./Home.css";

export const Home = () => {
  return (
    <div className="home-container">
      <div className="home-caixa">
        <h1>
          Bem-vindo a Galeria Online
        </h1>
      </div>
      <Botao nomeBotao="Entrar" />
    </div>
  );
};
