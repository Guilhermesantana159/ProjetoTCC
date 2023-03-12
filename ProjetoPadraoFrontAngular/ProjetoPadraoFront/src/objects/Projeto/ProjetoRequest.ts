import { GridAtvTarefas } from "./GridAtvTarefas";
import { TarefaReponsavel } from "./TarefaResponsavel";

export interface ProjetoRequest
{
    idProjeto: number | undefined,
    titulo: string,
    dataInicio: string,
    dataFim: string,
    descricao: string | undefined,
    listarParaParticipantes: boolean,
    atividade: Array<GridAtvTarefas>, 
    tarefa: Array<TarefaReponsavel>
}

