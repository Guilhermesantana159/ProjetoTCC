import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataGridComponent } from 'src/components/data-grid/data-grid.component';
import { GridService } from 'src/components/data-grid/data-grid.service';
import { EnumBase } from 'src/enums/EnumBase';
import { TypeFilter } from 'src/enums/TypeFilter';
import { GridOptions } from 'src/objects/Grid/GridOptions';
import { ETipoArquivo } from '../../../../enums/ETipoArquivo';
import { RetornoPadrao } from 'src/objects/RetornoPadrao';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from 'src/factorys/base.service';

@Component({
  selector: 'projeto-root',
  templateUrl: './projeto-grid.component.html',
  styleUrls: ['../projeto.component.css']
})

export class ProjetoComponent{
  gridOptions: GridOptions;
  statusEnumProjeto: Array<EnumBase>;

  constructor(private gridService: GridService,public gridComponent: DataGridComponent,
    private router: Router,private response: BaseService,private toastr: ToastrService){  

    this.statusEnumProjeto = [
      {
        Description: 'Aberto',
        Value: '0'
      },
      {
        Description: 'Cancelado',
        Value: '1'
      },
      {
        Description: 'Concluido',
        Value: '2'
      },
      {
        Description: 'Atrasado',
        Value: '3'
      }
    ];

    this.gridOptions = {
        Parametros: {
          Controller: 'Projeto',
          Metodo: 'ConsultarGridProjeto',
          PaginatorSizeOptions: [10,15,20],
          PageSize: 10,
          MultiModal: false,
          UrlRelatorio: 'Projeto/GerarRelatorioGridProjeto',
          Modal: undefined,
          Params: undefined
        },
        Colunas: [{
            Field: 'Action',
            DisplayName: 'Ações',
            CellTemplate: undefined,
            Type: TypeFilter.none,
            Filter: false,
            OrderBy: false,
            EnumName: undefined,
            ServerField: '',
            EnumOptions: undefined,  
            StyleColuna: 'min-width: 40vh; max-width: 45vh;',
            StyleCell: "display: inline-block;padding: 2pt;",
            ClassCell: undefined,
            CellGraphics: undefined,
            CellImage: undefined,
            ActionButton: [
              {
                TypeActionButton: 0,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-pencil-square"></i>',
                  ClassProperty: 'btn btn-info btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: 'actionDisabled'
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: ''
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Editar'
                }
              },
              {
                TypeActionButton: 2,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-check2"></i>',
                  ClassProperty: 'btn btn-success btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: 'actionDisabled'
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: ''
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Concluir'
                }
              },
              {
                TypeActionButton: 4,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-trash"></i>',
                  ClassProperty: 'btn btn-danger btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: ''
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: ''
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Deletar'
                }
              },
              {
                TypeActionButton: 3,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-x-octagon"></i>',
                  ClassProperty: 'btn btn-danger btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: 'actionDisabled'
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: ''
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Cancelar'
                }
              }
            ]  
        },
        {
          Field: 'idProjeto',
          DisplayName: 'Cód',
          CellTemplate: undefined,
          ActionButton: undefined,
          Type: TypeFilter.Number,
          EnumName: undefined,
          Filter: true,
          OrderBy: true,
          ServerField: 'IdProjeto',
          StyleColuna: undefined,
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,   
          CellGraphics: undefined,
          CellImage: undefined,
        },
        {
          Field: 'titulo',
          DisplayName: 'Titulo',
          CellTemplate: undefined,
          ActionButton: undefined,
          Type: TypeFilter.String,
          EnumName: undefined,
          OrderBy: true,
          Filter: true,
          ServerField: 'Titulo',
          StyleColuna: 'min-width: 45vh; max-width: 50vh;',
          EnumOptions: undefined,
          StyleCell: 'margin-left:5pt; padding: 3pt; border-radius: 2pt;',
          ClassCell: 'd-inline',
          CellGraphics: undefined,
          CellImage: {
            PropertyLink: 'fotoProjeto',
            ClassImage: 'rounded float-start d-inline',
            StyleImage: 'max-width: 30pt;',
            Tooltip:  'Representação do projeto',
            OnlyImage: false
          },    
        },
        {
          Field: 'dataInicio',
          DisplayName: 'Data de início',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Data,
          EnumName: undefined,
          ServerField: 'DataInicio',
          Filter: true,
          OrderBy: true,
          StyleColuna: undefined,
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
          CellImage: undefined,    
        },
        {
          Field: 'dataFim',
          DisplayName: 'Data de conclusão',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Data,
          EnumName: undefined,
          ServerField: 'DataFim',
          Filter: true,
          OrderBy: true,
          StyleColuna: undefined,
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
          CellImage: undefined,    
        },
        {
          Field: 'status',
          DisplayName: 'Status Projeto',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Enum,
          EnumName: undefined,
          ServerField: 'Status',
          Filter: true,
          OrderBy: true,
          StyleColuna: undefined,
          EnumOptions: this.statusEnumProjeto,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
          CellImage: undefined,  
        },
        {
          Field: 'porcentagem',
          DisplayName: 'Andamento do projeto',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'porcentagem',
          Filter: false,
          OrderBy: false,
          StyleColuna: 'min-width: 40vh',
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: {  
            PropertyLink: 'porcentagem',
            Tooltip: '%',
            OnlyGraphics: true,
            ClassGraphics: '',
            StyleGraphics: undefined
          },
          CellImage: undefined,  
        }
      ]
    }
  }

  RecarregarGrid(){
    this.gridService.RecarregarGrid();
  }

  Editar(data: any){
    this.router.navigate(['main/projeto/' + data.idProjeto.toString() + '/editar']);
  }

  Concluir(data: any){
    this.response.Post("Projeto","MudarStatusProjeto",{Status: 2,IdProjeto: data.idProjeto}).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success(response.mensagem, 'Mensagem:');
        }else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }

        this.gridService.RecarregarGrid();

      });
  }

  Encerrar(data: any){
    this.response.Post("Projeto","MudarStatusProjeto",{Status: 1,IdProjeto: data.idProjeto}).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success(response.mensagem, 'Mensagem:');

        }else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }

        this.gridService.RecarregarGrid();

      });
  }

  Deletar(data: any){
    this.response.Post("Projeto","Deletar",{IdProjeto: data.idProjeto}).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success(response.mensagem, 'Mensagem:');
        }else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }

        this.gridService.RecarregarGrid();

      });
  }

  GerarRelatorio(tipo: ETipoArquivo){
    this.gridService.EmitirRelatorio(tipo);
  }
};


