import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { environment } from "src/environments/environment.prod";

@Injectable({
    providedIn: 'root'
})

export class BaseService{
    
    //Variaveis
    request : HttpClient;
    rota: string

    //Constructor
    constructor(http: HttpClient){
        this.request = http;
        this.rota = environment.link;
    }

    Get(controller: string,metodo: string){
        return this.request.get<any>(this.rota + controller + '/' + metodo)
    };
    
    Post(controller: string,metodo: string,objetoEnvio: any){
        return this.request.post<any>(this.rota + controller + '/' + metodo,objetoEnvio)
    };

    PostUrl(url: string,objetoEnvio: any){
        return this.request.post<any>(this.rota + url,objetoEnvio)
    };

    PostRelatorio(url: string,objetoEnvio: any){
        return this.request.post(this.rota + url,objetoEnvio,{observe:'response',responseType:'blob'})
    };

    GetRelatorio(url: string){
        return this.request.get(this.rota + url,{observe:'response',responseType:'blob'})
    };
};