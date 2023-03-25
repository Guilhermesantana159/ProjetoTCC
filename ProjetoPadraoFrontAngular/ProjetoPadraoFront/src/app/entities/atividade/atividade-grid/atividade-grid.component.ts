import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DataGridComponent } from 'src/components/data-grid/data-grid.component';
import { GridService } from 'src/components/data-grid/data-grid.service';
import { ETipoArquivo } from 'src/enums/ETipoArquivo';
import { EnumBase } from 'src/enums/EnumBase';
import { TypeFilter } from 'src/enums/TypeFilter';
import { BaseService } from 'src/factorys/base.service';
import { GridOptions } from 'src/objects/Grid/GridOptions';

@Component({
  selector: 'atividade-root',
  templateUrl: './atividade-grid.component.html',
  styleUrls: ['../atividade.component.css']
})

export class AtvidadeComponent{
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
          UrlRelatorio: 'Atividade/GerarRelatorioGridProjetoAtividade',
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
            StyleColuna: 'min-width: 15vh; max-width: 20vh;',
            StyleCell: "display: inline-block;padding: 2pt;",
            ClassCell: undefined,
            CellGraphics: undefined,
            CellImage: undefined,
            ActionButton: [
              {
                TypeActionButton: 5,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-eye"></i>',
                  ClassProperty: 'btn btn-primary btn-sm',
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
                  Tooltip: 'Visualizar'
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

  VisualizarProjeto(data: any){
    this.router.navigate(['main/atividade/projeto/' + data.idProjeto.toString()]);
  }

  GerarRelatorio(tipo: ETipoArquivo){
    this.gridService.EmitirRelatorio(tipo);
  }
  
  RecarregarGrid(){
    this.gridService.RecarregarGrid();
  }
};


