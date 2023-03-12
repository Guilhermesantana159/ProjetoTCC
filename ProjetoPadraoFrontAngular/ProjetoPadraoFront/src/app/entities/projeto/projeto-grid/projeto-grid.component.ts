import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataGridComponent } from 'src/components/data-grid/data-grid.component';
import { GridService } from 'src/components/data-grid/data-grid.service';
import { EnumBase } from 'src/enums/EnumBase';
import { TypeFilter } from 'src/enums/TypeFilter';
import { GridOptions } from 'src/objects/Grid/GridOptions';
import { ETipoArquivo } from '../../../../enums/ETipoArquivo';

@Component({
  selector: 'projeto-root',
  templateUrl: './projeto-grid.component.html',
  styleUrls: ['../projeto.component.css']
})

export class ProjetoComponent{
  gridOptions: GridOptions;
  statusEnumProjeto: Array<EnumBase>;

  constructor(private gridService: GridService,public gridComponent: DataGridComponent,
    private router: Router){  

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
        Value: '1'
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
            StyleColuna: undefined,  
            StyleCell: undefined,
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
                    PropertyDisabled: ''
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: 'disabledButtonAction'
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Editar'
                }
              },
              {
                TypeActionButton: 0,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-check2"></i>',
                  ClassProperty: 'btn btn-success btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: ''
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: 'disabledButtonAction'
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Concluir'
                }
              },
              {
                TypeActionButton: 0,
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
                TypeActionButton: 0,
                TypeButton: 1,
                ParametrosAction: {
                  Conteudo: '<i class="bi bi-x-octagon"></i>',
                  ClassProperty: 'btn btn-danger btn-sm',
                  Disabled: {
                    Disabled: undefined,
                    PropertyDisabled: ''
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: 'disabledButtonAction'
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Encerrar'
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
            PropertyLink: 'imagemProjeto',
            ClassImage: 'rounded float-start d-inline',
            StyleImage: 'max-width: 30pt;',
            Tooltip:  'Representação do projeto',
            OnlyImage: false
          },    
        },
        {
          Field: 'dataFimProjeto',
          DisplayName: 'Data de conclusão',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Data,
          EnumName: undefined,
          ServerField: 'dataFimProjeto',
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
          Field: 'statusProjeto',
          DisplayName: 'Perfil',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Enum,
          EnumName: undefined,
          ServerField: 'perfil',
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
          Field: 'andamentoProjeto',
          DisplayName: 'Andamento projeto',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'andamentoProjeto',
          Filter: false,
          OrderBy: false,
          StyleColuna: 'min-width: 40vh',
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: {  
            PropertyLink: 'andamentoProjeto',
            Tooltip: '%',
            OnlyGraphics: true,
            ClassGraphics: '',
            StyleGraphics: undefined
          },
          CellImage: undefined,  
        },
        {
          Field: 'funcaoProjeto',
          DisplayName: 'Função',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'funcaoProjeto',
          Filter: true,
          OrderBy: true,
          StyleColuna: 'min-width: 45vh; max-width: 50vh;',
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
          CellImage: undefined,           
        },
        {
          Field: 'admProjeto',
          DisplayName: 'Admnistrador',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'admProjeto',
          Filter: true,
          OrderBy: true,
          StyleColuna: 'min-width: 45vh; max-width: 50vh;',
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
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

  GerarRelatorio(tipo: ETipoArquivo){
    this.gridService.EmitirRelatorio(tipo);
  }
};


