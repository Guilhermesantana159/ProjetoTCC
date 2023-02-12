import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataGridComponent } from 'src/components/data-grid/data-grid.component';
import { GridService } from 'src/components/data-grid/data-grid.service';
import { EnumBase } from 'src/enums/EnumBase';
import { TypeFilter } from 'src/enums/TypeFilter';
import { GridOptions } from 'src/objects/Grid/GridOptions';
import { ETipoArquivo } from '../../../../enums/ETipoArquivo';

@Component({
  selector: 'usuario-root',
  templateUrl: './usuario-grid.component.html',
  styleUrls: ['../usuario.component.css']
})

export class UsuarioComponent{
  gridOptions: GridOptions;
  optionsEnumPerfil: Array<EnumBase>;
  
  constructor(private gridService: GridService,public gridComponent: DataGridComponent,
    private router: Router){  
    this.optionsEnumPerfil = [
      {
        Description: 'Administrador',
        Value: '0'
      },
      {
        Description: 'Comum',
        Value: '1'
      }
    ];

    this.gridOptions = {
        Parametros: {
          Controller: 'Usuario',
          Metodo: 'ConsultarGridUsuario',
          PaginatorSizeOptions: [10,15,20],
          PageSize: 10,
          MultiModal: true,
          UrlRelatorio: 'Usuario/GerarRelatorioGridUsuario',
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
                    PropertyDisabled: 'teste'
                  },
                  Hidden: {
                    Hidden: false,
                    PropertyHidden: ''
                  },
                  Target: undefined,
                  Href: undefined,
                  Tooltip: 'Editar'
                }
              }
            ]  
        },
        {
          Field: 'idUsuario',
          DisplayName: 'Cód',
          CellTemplate: undefined,
          ActionButton: undefined,
          Type: TypeFilter.Number,
          EnumName: undefined,
          Filter: true,
          OrderBy: true,
          ServerField: 'IdUsuario',
          StyleColuna: undefined,
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,   
          CellGraphics: undefined,
          CellImage: undefined,
        },
        {
          Field: 'nome',
          DisplayName: 'Nome',
          CellTemplate: undefined,
          ActionButton: undefined,
          Type: TypeFilter.String,
          EnumName: undefined,
          OrderBy: true,
          Filter: true,
          ServerField: 'Nome',
          StyleColuna: 'min-width: 45vh; max-width: 50vh;',
          EnumOptions: undefined,
          StyleCell: 'margin-left:5pt; padding: 3pt; border-radius: 2pt;',
          ClassCell: 'd-inline',
          CellGraphics: undefined,
          CellImage: {
            PropertyLink: 'imagemUsuario',
            ClassImage: 'rounded float-start d-inline',
            StyleImage: 'max-width: 30pt;',
            Tooltip:  'Foto do usuário',
            OnlyImage: false
          },    
        },
        {
          Field: 'email',
          DisplayName: 'Email',
          CellTemplate: undefined,
          ActionButton: undefined,
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'Email',
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
          Field: 'cpf',
          DisplayName: 'CPF',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'CPF',
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
          Field: 'dataNascimento',
          DisplayName: 'Data de nascimento',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Data,
          EnumName: undefined,
          ServerField: 'dataNascimento',
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
          Field: 'perfil',
          DisplayName: 'Perfil',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.Enum,
          EnumName: undefined,
          ServerField: 'perfil',
          Filter: true,
          OrderBy: true,
          StyleColuna: undefined,
          EnumOptions: this.optionsEnumPerfil,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: undefined,
          CellImage: undefined,  
        },
        {
          Field: 'dedicacao',
          DisplayName: 'Dedicaçãp do usuário',
          CellTemplate: undefined,
          ActionButton: undefined, 
          Type: TypeFilter.String,
          EnumName: undefined,
          ServerField: 'dedicacao',
          Filter: false,
          OrderBy: false,
          StyleColuna: 'min-width: 40vh',
          EnumOptions: undefined,
          StyleCell: undefined,
          ClassCell: undefined,
          CellGraphics: {  
            PropertyLink: 'dedicacao',
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
    this.router.navigate(['main/usuario/' + data.idUsuario.toString() + '/editar']);
  }

  GerarRelatorio(tipo: ETipoArquivo){
    this.gridService.EmitirRelatorio(tipo);
  }
};


