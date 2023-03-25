import { GridAtvTarefas } from "./GridAtvTarefas";
import { Tarefa } from "./Tarefas";

export interface ProjetoRequest
{
    IdProjeto: number | undefined,
    Titulo: string,
    DataInicio: string,
    DataFim: string,
    Descricao: string | undefined,
    ListarParaParticipantes: boolean,
    Atividade: Array<AtividadeRequest>, 
    IdUsuarioCadastro: number,
    Tarefa: Array<any>,
    Foto: string | undefined
}

export interface AtividadeRequest
{
    Atividade: string,
    DataInicial: Date | undefined,
    DataFim: Date | undefined,
    ListTarefas: Array<Tarefa>
}
