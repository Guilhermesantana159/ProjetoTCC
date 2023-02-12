import { RetornoPadrao } from "../RetornoPadrao"
import { Skill } from './Skill';

export interface UsuarioResponse extends RetornoPadrao
{
    data: DataUsuarioResponse
}

export interface DataUsuarioResponse
{
    idUsuario: number,
    nome: string,
    foto: string,
    email: string,
    cpf: string,
    telefone: string,
    senha: string,
    perfilAdministrador: boolean,
    dedicacao: number,
    cep: string,
    estado: string,
    cidade: string,
    pais: string,
    rua: string,
    bairro: string,
    numero: number,
    nomeMae: string,
    nomePai: string,
    observacao: string,
    rg: string,
    dataNascimento: string,
    genero: string,
    idUsuarioCadastro: number,
    idProfissao: string,
    lSkills: Array<Skill>
}