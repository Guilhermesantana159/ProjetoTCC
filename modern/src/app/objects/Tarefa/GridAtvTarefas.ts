import { EStatusAtividade } from "src/app/enums/EStatusAtividade";
import { Tarefa } from "./Tarefas";

export interface GridAtvTarefas
{
    idAtividade: number | undefined
    position: number,
    atividade: string,
    dataInicial: string,
    dataFim: string,
    listTarefas: Array<Tarefa>,
    statusAtividade: EStatusAtividade
}
