import { DecimalPipe } from "@angular/common";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { BaseService } from "src/factorys/services/base.service";
import { ActivatedRoute } from "@angular/router";
import { DataTarefaKambamResponse, TarefaKambamResponse } from "src/app/objects/Tarefa/TarefaKambamResponse";
import { Column } from "src/app/objects/Atividade/Column";
import { Subject } from "rxjs";

@Component({
  selector: 'kamban-tarefas-root',
  templateUrl: './kamban-tarefas.component.html',
  styleUrls: ['../kamban-tarefas.component.scss'],
  providers: [DecimalPipe]
})

export class KambamTarefasComponent implements OnInit,OnDestroy{
  breadCrumbItems!: Array<{}>;
  loading = false;
  submitColumn: boolean = false;
  idProjeto!: number;
  idUsuario:number = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '0')
  dataSource!: DataTarefaKambamResponse;
  destroy$ = new Subject<boolean>();
  columnsTotal: Array<Column> = [new Column('Aguardando','bg-dark',[]),new Column('Fazendo','bg-info',[]),new Column('Concluído','bg-success',[])];
  
  constructor(private response: BaseService,private toastr: ToastrService,private route: ActivatedRoute) {
    this.breadCrumbItems = [
      { label: 'Atividades' },
      { label: 'Tarefas'},
      { label: 'Registro', active: true }
    ];
  }
  ngOnDestroy(): void {
    this.destroy$.next(true);
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if(params['id'] != undefined){
        this.loading = true;
        this.response.Get("Tarefa","ConsultarTarefasRegistro/" + params['id'] + '/' + this.idUsuario).subscribe(
        (response: TarefaKambamResponse) =>{        
          if(response.sucesso){
            response.data.listTarefas.forEach(element => {
              if(element.statusEnum == 0){
                this.columnsTotal[0].tasks.push(element);
              }else if(element.statusEnum == 1){
                this.columnsTotal[1].tasks.push(element);
              }else{
                this.columnsTotal[2].tasks.push(element);
              };
            });
          }else{
            this.toastr.error(response.mensagem, 'Mensagem:');
          }
          this.loading = false;
      });
    }});
  }

  applyFilterAtividadesTarefas(event: Event) {
    let filterValue = (event.target as HTMLInputElement).value;

    filterValue = filterValue.toLocaleLowerCase();

    if(filterValue ==  undefined || filterValue == '' || filterValue == null){
      return;
    }  
  }
}

