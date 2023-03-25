import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConsultaModal } from 'src/objects/Consulta-Padrao/consulta-modal';
import { ConsultaModalParams } from 'src/objects/Consulta-Padrao/ConsultaModalParams';
import { GridService } from '../data-grid/data-grid.service';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'consulta-padrao',
  templateUrl: './consulta-padrao.component.html'
})

export class ConsultaModalComponent implements OnInit{
  @Input() ParamsConsulta!: ConsultaModalParams;
  @Input() selectedText!: string;
  @Input() disabled!: boolean;
  @Input() selectedValue!: string;
  @Output() valueChange = new EventEmitter();
  form!: FormGroup;

  //Multi modal
  itens: Array<string> = [];
  itensMultiModal: Array<ConsultaModal> = [];

  //Modal
  ConsultaModal!: ConsultaModal;
  gridOptions: any;

  constructor(private gridService: GridService,public rootFormGroup: FormGroupDirective){
  }

  ngOnInit(): void {
    this.form = this.rootFormGroup.control;
    this.ConsultaModal = {
      SelectedText: this.form.get(this.selectedText)?.value,
      SelectedValue: undefined
    }
    this.gridOptions = this.ParamsConsulta.GridOptions;

    this.gridService.selecionar.subscribe((data: any) => {
      if(!this.gridOptions.Parametros.MultiModal){
        if(this.gridOptions.Parametros.Modal?.SelectedText != undefined &&
          this.gridOptions.Parametros.Modal?.SelectedText != undefined){

          //Monitoramento de changes
          this.ConsultaModal.SelectedText = data[this.gridOptions.Parametros.Modal?.SelectedText];
          this.ConsultaModal.SelectedValue = data[this.gridOptions.Parametros.Modal?.SelectedValue];
          this.valueChange.emit(this.ConsultaModal);

          //Atribuição no formulário
          this.form.get(this.selectedText)?.setValue(this.ConsultaModal.SelectedText);
          this.form.get(this.selectedValue)?.setValue(this.ConsultaModal.SelectedValue);
        }
      }
      else{
        if(this.gridOptions.Parametros.Modal?.SelectedText != undefined &&
          this.gridOptions.Parametros.Modal?.SelectedText != undefined){

          //Monitoramento de changes
          this.ConsultaModal.SelectedText = data[this.gridOptions.Parametros.Modal?.SelectedText];
          this.ConsultaModal.SelectedValue = data[this.gridOptions.Parametros.Modal?.SelectedValue];
          this.valueChange.emit(this.itensMultiModal);

          //Alimentacao
          this.itensMultiModal.push(this.ConsultaModal);
          this.itens.push(this.ConsultaModal.SelectedText ?? '');  

          //Atribuição no formulário
          this.form.get(this.selectedText)?.setValue(this.ConsultaModal.SelectedText);
          this.form.get(this.selectedValue)?.setValue(this.ConsultaModal.SelectedValue);
        }
      }
    });  
  }

  ResetConsultaPadrao(){
    this.ConsultaModal ={
      SelectedText: '',
      SelectedValue: undefined
    };

    this.form.get(this.selectedText)?.setValue(undefined);
    this.form.get(this.selectedValue)?.setValue(undefined);

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
}
