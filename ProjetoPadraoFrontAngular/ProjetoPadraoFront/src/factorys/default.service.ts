import { Injectable } from '@angular/core';
import { TypeFilter } from 'src/enums/TypeFilter';

@Injectable({
    providedIn: 'root'
})

export class DefaultService{
    public OptionSelect = {
      TipoUsuario: [
        {
          Description: 'Administrador',
          Value: '0'
        },
        {
          Description: 'Comum',
          Value: '1'
        }
      ]
    };

    public Modal = {
        ConsultaPadraoUsuario: {
            Parametros: {
                Controller: 'Usuario',
                Metodo: 'ConsultarGridUsuario',
                PaginatorSizeOptions: [5,10],
                UrlRelatorio: '',
                MultiModal: false,
                Modal: {
                  SelectedText: 'nome',
                  SelectedValue: 'idUsuario'
                },
                PageSize: 5,
                Params: {
                  Teste: 1
                }
              },
              Colunas: [
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
                StyleColuna: 'min-width: 40vh; max-width: 50vh;',
                EnumOptions: undefined,
                StyleCell: 'margin-left:5pt; padding: 2pt; border-radius: 2pt; background: rgb(40,167,69);',
                ClassCell: 'd-inline text-white',
                CellGraphics: undefined,
                CellImage: {
                  PropertyLink: 'imagemUsuario',
                  ClassImage: 'rounded float-start d-inline',
                  StyleImage: 'max-width: 30pt;',
                  Tooltip:  'Foto do usuário',
                  OnlyImage: false
                },    
              }
            ]
        }
    }
};