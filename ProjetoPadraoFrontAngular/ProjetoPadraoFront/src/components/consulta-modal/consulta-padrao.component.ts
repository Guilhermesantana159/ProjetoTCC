import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConsultaModal } from 'src/objects/Consulta-Padrao/consulta-modal';
import { ConsultaModalParams } from 'src/objects/Consulta-Padrao/ConsultaModalParams';
import { GridService } from '../data-grid/data-grid.service';

@Component({
  selector: 'consulta-padrao',
  templateUrl: './consulta-padrao.component.html'
})

export class ConsultaModalComponent implements OnInit{
  @Input() ParamsConsulta!: ConsultaModalParams;
  @Output() valueChange = new EventEmitter();

  //Multi modal
  itens: Array<string> = [];
  itensMultiModal: Array<ConsultaModal> = [];

  //Modal
  ConsultaModal: ConsultaModal = {
    SelectedText: '',
    SelectedValue: undefined
  }
  gridOptions: any;

  constructor(private gridService: GridService){
  }

  ngOnInit(): void {
    this.gridOptions = this.ParamsConsulta.GridOptions;

    this.gridService.selecionar.subscribe((data: any) => {
      if(!this.gridOptions.Parametros.MultiModal){
        if(this.gridOptions.Parametros.Modal?.SelectedText != undefined &&
          this.gridOptions.Parametros.Modal?.SelectedText != undefined){
          this.ConsultaModal.SelectedText = data[this.gridOptions.Parametros.Modal?.SelectedText];
          this.ConsultaModal.SelectedValue = data[this.gridOptions.Parametros.Modal?.SelectedValue];
  
          this.valueChange.emit(this.ConsultaModal);
        }
      }
      else{
        if(this.gridOptions.Parametros.Modal?.SelectedText != undefined &&
          this.gridOptions.Parametros.Modal?.SelectedText != undefined){
          this.ConsultaModal.SelectedText = data[this.gridOptions.Parametros.Modal?.SelectedText];
          this.ConsultaModal.SelectedValue = data[this.gridOptions.Parametros.Modal?.SelectedValue];
  
          this.itensMultiModal.push(this.ConsultaModal);
          this.itens.push(this.ConsultaModal.SelectedText ?? '');

          this.valueChange.emit(this.itensMultiModal);
        }
      }
    });  
  }

  ResetConsultaPadrao(){
    this.ConsultaModal ={
      SelectedText: '',
      SelectedValue: undefined
    };

    this.itensMultiModal = [];
    this.itens = [];
  }

  Remove(item: string){
    this.itens.splice(this.itens.indexOf(item),1);

    for (let index = 0; index < this.itensMultiModal.length; index++) {
      if(this.itensMultiModal[index].SelectedText == item){
        this.itensMultiModal.splice(index,1);
        break;
      }
    }
  }

  LoadEdit(ConsultaModal: ConsultaModalParams){
    this.ConsultaModal.SelectedText = ConsultaModal.SelectedText;
    this.ConsultaModal.SelectedValue = ConsultaModal.SelectedValue;
  };
}
