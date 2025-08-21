import "./Card.css"
import imagemCard from '../../assets/img/Captura de tela 2025-04-01 102632.png'
import imagemPan from '../../assets/img/pen.svg'
import imagemTrash from '../../assets/img/trash.svg'


export const Card = ({tituloCard}) => {
    return (
        <>
            <div className="cardDaImagem">
                <p>{tituloCard}</p>
                <img  className="imgDoCard"src={imagemCard} alt="" />

                <div className="icons">
                    <img src={imagemPan} alt="" />
                    <img src={imagemTrash} alt="" />
                </div>
            </div>
        </>
    ) 
}