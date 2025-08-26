import "./Card.css"


import imagemPan from '../../assets/img/pen.svg'
import imagemTrash from '../../assets/img/trash.svg'


export const Card = ({tituloCard, imagemCard, funcDeletar, funcEditar}) => {
    return (
        <>
            <div className="cardDaImagem">
                <p>{tituloCard}</p>
                <img  className="imgDoCard"src={imagemCard} alt="" />

                <div className="icons">
                    <img  src={imagemPan} onClick={funcEditar}alt="" />
                    <img src={imagemTrash} onClick={funcDeletar} alt="" />
                </div>
            </div>
        </>
    ) 
}