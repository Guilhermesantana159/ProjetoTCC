import { DataContatoResponse } from "./ContatoResponseResponse";

export interface ChatContatos {
    title: string;
    contacts: Array<DataContatoResponse>;
}