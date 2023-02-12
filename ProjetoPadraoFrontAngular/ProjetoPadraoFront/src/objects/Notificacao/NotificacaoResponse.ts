import { ELido } from "../../enums/ELido";
import { RetornoPadrao } from "../RetornoPadrao";

export interface NotificacaoResponse extends RetornoPadrao
{
    data: {
        itens: Array<DataNotificacao>
    }
}

export interface DataNotificacao
{
    idNotificacao: number
    corpo: string,
    titulo: string,
    lido: ELido
}


