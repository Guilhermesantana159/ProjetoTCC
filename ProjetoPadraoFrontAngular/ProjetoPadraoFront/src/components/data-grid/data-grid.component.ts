import { Component, ElementRef, EventEmitter, Injectable, Input, OnInit, Output } from '@angular/core';
import { BaseService } from 'src/factorys/base.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Action, GridOptions,Coluna } from 'src/objects/Grid/GridOptions';
import { ResponseData } from 'src/objects/Grid/GridResponse';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { DomSanitizer } from '@angular/platform-browser';
import { Filter } from 'src/objects/Grid/Filter';
import {debounce} from 'utils-decorators';
import { GridService } from './data-grid.service';
import { TypeActionButton } from 'src/enums/TypeActionButton';
import { TypeFilter } from 'src/enums/TypeFilter';
import { ETipoArquivo } from '../../enums/ETipoArquivo';

@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss']
})

export class DataGridComponent implements OnInit{
  @Input() gridOptions!: GridOptions;
  @Input() modal!: boolean;

  //GridEvents
  @Output() Editar: EventEmitter<any> = new EventEmitter;

  //variaveis grid
  displayedColumnsHeader: string[] = [];
  displayedColumnsFilter: string[] = [];
  displayedColumns: string[] = [];
  data: any = [];
  options: any = [];

  //variaveis paginação
  pageEvent: PageEvent = {
    pageIndex: 0,
    pageSize: 10,
    length: 0,
  }
  pageSizeOptions: number[] = [5, 10, 25, 100];
  
  //Variaveis Ordenação
  sort: Sort = {
    active: '',
    direction: ''
  }

  //Variaveis Filters
  QueryFilters: Filter[] = [];

  //Variaveis style e funcionais
  loading: boolean = true;
  styleTd: string = 'max-width: 30vh;';
  colunaSeletionModal!: Coluna; 

  constructor(private response: BaseService,private toastr: ToastrService,
    private router: Router,private paginator: MatPaginatorIntl,private elementRef: ElementRef,
    private sanitizer: DomSanitizer,private gridService: GridService) {
      paginator.itemsPerPageLabel = 'Itens por página';

      //Eventos da grid 
      this.gridService.recarregar.subscribe(() => {
        this.ConsultarGrid();
      });

      this.gridService.relatorio.subscribe((tipo: ETipoArquivo) => {
        this.EmitirRelatorio(tipo);
      });
    }

  ngOnInit(): void {
    //Setar colunas e configurações da grid
    if(this.gridOptions.Parametros.Modal != undefined){
      for (let index = 0; index < this.gridOptions.Colunas.length; index++) {
        if(this.gridOptions.Colunas[index].ActionButton != undefined){
          this.gridOptions.Colunas.splice(index,1);
        }
      }

      //Configuração da coluna de selecao da modal
      this.colunaSeletionModal = {
        Field: 'undefined',
        DisplayName: '',
        CellTemplate: undefined,
        ActionButton: [
          {
            TypeActionButton: 1,
            TypeButton: 1,
            ParametrosAction: {
              Conteudo: '<i class="bi bi-check-lg"></i>',
              ClassProperty: 'btn btn-success btn-sm',
              Disabled: {
                Disabled: false,
                PropertyDisabled: ''
              },
              Hidden: {
                Hidden: false,
                PropertyHidden: ''
              },
              Target: undefined,
              Href: undefined,
              Tooltip: 'Selecionar'
            }
          }
        ],
        Type: TypeFilter.none,
        EnumOptions: undefined,
        EnumName: undefined,
        Filter: false,
        OrderBy: false,
        ServerField: 'undefined',
        StyleColuna: 'min-Width: 20vh',
        StyleCell: undefined,
        ClassCell: 'd-flex justify-content-center',
        CellGraphics:  undefined,
        CellImage: undefined
      }

      this.gridOptions.Colunas.push(this.colunaSeletionModal);
    }

    this.gridOptions.Colunas.forEach(element => {
      this.displayedColumns.push(element.Field);
      this.displayedColumnsFilter.push(element.ServerField + 'Field');
    });  

    if(this.gridOptions.Parametros.PaginatorSizeOptions != undefined)
      this.pageSizeOptions = this.gridOptions.Parametros.PaginatorSizeOptions;

    if(this.gridOptions.Parametros.PageSize != undefined)
      this.pageEvent.pageSize = this.gridOptions.Parametros.PageSize;

    this.ConsultarGrid(this.pageEvent);
  }

  ConsultarGrid(event?:PageEvent,sortEvent?: Sort,filter?: Filter){
    if(event != undefined)    
      this.pageEvent = event;

    if(sortEvent != undefined)    
      this.sort = sortEvent;

    if(filter != undefined){
      for (let index = 0; index < this.QueryFilters.length; index++) {
        if(this.QueryFilters[index].Field == filter.Field){
          if(filter.Type == 'data' && this.QueryFilters[index].EOperadorFilter != filter.EOperadorFilter){
            continue;
          }          
          this.QueryFilters.splice(index, 1); 
        }
      }

      if(filter.Value != ""){
        this.QueryFilters.push(filter);
      }
    }   
    
    let request = {
      Take: this.pageEvent.pageSize,
      Page: this.pageEvent.pageIndex,
      OrderFilters: {
        Campo: this.sort.active,
        Operador: this.sort.direction == 'asc' ? 0 : 1
      },
      QueryFilters: this.QueryFilters
    }

    if(this.gridOptions.Parametros.Params != undefined){
      request = Object.assign(request,this.gridOptions.Parametros.Params)
    }
    
    this.response.Post(this.gridOptions.Parametros.Controller,this.gridOptions.Parametros.Metodo,request)
    .subscribe(
      (response: ResponseData) =>{        
        if(response.sucesso){
          this.data = response.data.itens;
          this.pageEvent.length = response.data.totalItens;

          //Atribuição de html a coluna
          this.gridOptions.Colunas.forEach(element =>{
            if(element.ActionButton != undefined){
              response.data.itens.forEach(cell =>
                cell[element.Field] = element.CellTemplate);
            }  
          });

          //Atribuição de action button a coluna
          this.gridOptions.Colunas.forEach(element =>{
            if(element.CellTemplate != undefined){
              response.data.itens.forEach(cell =>
                cell[element.Field] = element.CellTemplate);
            }
            
            this.loading = false;
          });
        }
        else{
          this.toastr.error('<small>' + response.mensagem + '<small>', 'Mensagem');
        }
      }
    );
  }

  @debounce(500)
  FiltrarGrid(filter?: Filter){
    this.ConsultarGrid(undefined,undefined,filter);
  }

  ActionButton(action: Action,data: any){
    if(action.ParametrosAction.Href != undefined)
      return;

    if(action.TypeActionButton == TypeActionButton.Editar){
      this.Editar.emit(data);
    }  
    
    if(action.TypeActionButton == TypeActionButton.Selecionar){
      this.gridService.SelecionarModal(data);
    }  
  }

  EmitirRelatorio(tipo: ETipoArquivo){
    
    let request = {
      OrderFilters: {
        Campo: this.sort.active,
        Operador: this.sort.direction == 'asc' ? 0 : 1
      },
      QueryFilters: this.QueryFilters,
      Tipo: tipo
    }

    if(this.gridOptions.Parametros.Params != undefined){
      request = Object.assign(request,this.gridOptions.Parametros.Params)
    }
    
    this.response.PostRelatorio(this.gridOptions.Parametros.UrlRelatorio,request)
    .subscribe((response: any) => {
      let fileName = response.headers.get('content-disposition')?.split(';')[1].split('=')[1];
      let blob: Blob = response.body as Blob;
      let a = document.createElement('a');
      a.download = fileName;
      a.href = window.URL.createObjectURL(blob);

      a.click()
    });  
  }
}
