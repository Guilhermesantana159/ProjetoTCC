import { Tarefa } from "./Tarefas";

export interface GridAtvTarefas
{
    position: number,
    atividade: string;
    dataInicial: string;
    dataFim: string;
    listTarefas: Array<Tarefa>
}
