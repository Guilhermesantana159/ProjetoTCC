import { RetornoPadrao } from "../RetornoPadrao";

export interface EstruturaMenu extends RetornoPadrao
{
    data: DataEstruturaMenu
}

export interface DataEstruturaMenu
{
    lModulos: Array<Modulo>
}

export interface Modulo
{
    nome: string,
    icone: string,
    descricaoLabel: string,
    descricaoModulo: string,
    lMenus: Array<Menu> 
}

export interface Menu
{
    nome: string,
    link: string,
    descricaoMenu: string
}
