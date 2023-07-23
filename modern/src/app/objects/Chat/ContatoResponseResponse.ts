import { EStatusContato } from "src/app/enums/EStatusContato";
import { RetornoPadrao } from "../RetornoPadrao";

export interface ContatoResponse extends RetornoPadrao
{
    data: DataContatoResponse
}

export interface DataContatoResponse
{
    nome: string,
    foto: string,
    statusContato: EStatusContato,
    idUsuarioContato: number
    idContatoChat: number
}


