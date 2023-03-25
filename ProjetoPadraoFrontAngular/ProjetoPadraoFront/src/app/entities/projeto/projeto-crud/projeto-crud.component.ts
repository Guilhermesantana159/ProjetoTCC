import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from 'src/factorys/base.service';
import { BaseOptions } from 'src/objects/Select/SelectPadrao';
import { RetornoPadrao } from 'src/objects/RetornoPadrao';
import { UsuarioResponse } from '../../../../objects/Usuario/UsuarioResponse';
import { DefaultService } from 'src/factorys/default.service';
import { ConsultaModalParams } from 'src/objects/Consulta-Padrao/ConsultaModalParams';
import { Tarefa } from 'src/objects/Projeto/Tarefas';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { ValidateDataAtual } from 'src/factorys/validators/validators-form';
import { GridAtvTarefas } from 'src/objects/Projeto/GridAtvTarefas';
import { TableBase } from 'src/objects/Table-base/Table-Base';
import { GridTarefaEquipe } from 'src/objects/Projeto/GridTarefaEquipe';
import { TarefaReponsavel } from 'src/objects/Projeto/TarefaResponsavel';
import { ProjetoRequest, AtividadeRequest } from 'src/objects/Projeto/ProjetoRequest';

@Component({
  selector: 'projeto-crud-root',
  templateUrl: './projeto-crud.component.html',
  styleUrls: ['../projeto.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class ProjetoCrudComponent{
  //Variaveis funcionais comportamento da tela tela
  loading = false;
  ProjetoRegisterFormGroup: FormGroup;
  submitRegister = false;
  indexTab: number = 0;
  IsNew = true;
  IsAdmin: string = window.localStorage.getItem('Perfil') ?? "false";
  IdUsuarioLogado: number = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '1');

  //Variaveis Aba Equipe e Funções
  FuncoesRegisterFormGroup: FormGroup;
  submitFuncoes: boolean = false;
  datalTarefaFuncoes: GridTarefaEquipe[] = []
  dataSourcelTarefaFuncoes = new MatTableDataSource(this.datalTarefaFuncoes);
  editTarefaEquipe: boolean = false;
  positionEquipeFuncoes: number = 0;
  columnslTarefaFuncoes: Array<string> = ['actions','responsavel','listTarefas']
  positionlTarefaFuncoes: number = 0;
  editlTarefaFuncoes: boolean = false;

  //Variaveis Aba Atividades Tarefas
  AtividadeRegisterFormGroup: FormGroup;
  submitAtv: boolean = false;
  lTarefa: Tarefa[] = [];
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  data: GridAtvTarefas[] = []
  dataSource = new MatTableDataSource(this.data);
  addOnBlur = true;
  
  columnsAtividade: Array<string> = ['actions','atividade','dataInicial','dataFim']
  columnsToDisplay: Array<TableBase> = [
    {
      columnName: 'Ações',
      columnBody: 'actions'
    },
    {
      columnName: 'Atividade',
      columnBody: 'atividade'
    },
    {
      columnName: 'Data de início',
      columnBody: 'dataInicial'
    },
    {
      columnName: 'Data Fim',
      columnBody: 'dataFim'
    }
];

  columnsToDisplayWithExpand = [...this.columnsAtividade, 'expand'];
  expandedElement!: GridAtvTarefas | null;
  position: number = 0;
  editTarefa: boolean = false;

  //Aba Principal
  optionsTipoProjeto: Array<BaseOptions> = [
    { 
      description: "Outros",
      value: 0
    }];
  paramsConsultaUsuario: ConsultaModalParams;
  index: any;

  constructor(private formBuilder: FormBuilder,private response: BaseService,private defaultService: DefaultService,
    private toastr: ToastrService,private router: Router,private route: ActivatedRoute) {    
    this.loading = true;

    this.paramsConsultaUsuario = {
      Label: 'Responsável pela Tarefa',
      Title: 'Consulta de Usuários',
      Disabled: false,
      Class: 'col-sm-12 col-xs-8 col-md-8 col-lg-8',
      Required: true,
      GridOptions: defaultService.Modal.ConsultaPadraoUsuario,
      SelectedText: '',
      SelectedValue: ''
    };

    this.ProjetoRegisterFormGroup = this.formBuilder.group({
      idProjeto: [undefined],
      titulo: [undefined, [Validators.required]],
      dataFim: [null, [Validators.required,ValidateDataAtual]],
      dataInicio: [undefined, [Validators.required,ValidateDataAtual]],      
      descricao: [undefined],     
      fotoProjeto: [undefined],
      listarAtvProjeto: [false],
      foto: [undefined]
    });

    this.AtividadeRegisterFormGroup = this.formBuilder.group({
      atividadeDescricao: [undefined, [Validators.required]],
      dataInicioAtv: [undefined, [Validators.required]],
      dataFimAtv: [undefined, [Validators.required]],
      lTarefas: [undefined, [Validators.required]],
      position: [undefined]
    });

    this.FuncoesRegisterFormGroup = this.formBuilder.group({
      listTarefas: [undefined, [Validators.required]],
      responsavel: [undefined, [Validators.required]],
      idResponsavel: [undefined, [Validators.required]],
      position: [undefined]
    });

    this.route.params.subscribe(params => {
      //Load Edit
      if(params['id'] != undefined){
         this.response.Get("Usuario","ConsultarViaId/" + params['id']).subscribe(
      (response: UsuarioResponse) =>{        
        if(response.sucesso){
          this.IsNew = false;
          this.ProjetoRegisterFormGroup.setValue(response.data);
          this.ProjetoRegisterFormGroup.get('pais')?.setValue('Brasil');
          this.ProjetoRegisterFormGroup.get('dataNascimento')?.setValue(new Date(response.data.dataNascimento));

          //Senha Imutavel quando edição
          this.ProjetoRegisterFormGroup.controls['senha'].clearValidators();
          this.ProjetoRegisterFormGroup.controls['senha'].updateValueAndValidity();

        }else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }
      });
    }});

    this.loading = false;
  }

  //Operacional tela
  Salvar = (form:FormGroup) =>{
    this.loading = true;
    this.submitRegister = true;

    if(this.ProjetoRegisterFormGroup.invalid){
      this.loading = false;
      this.toastr.error('<small>Preencha os campos corretamente no formulário!</small>', 'Mensagem:');
      return;
    }

    if(this.dataSource.data.length == 0){
      this.loading = false;
      this.toastr.error('<small>Cadastre no mínimo uma atividade para seu projeto!</small>', 'Mensagem:');
      return;
    }

    //Formatação para Save
    debugger
    let projetoRequest:ProjetoRequest = {
      IdProjeto: form.get('idProjeto')?.value,
      Titulo: form.get('titulo')?.value,
      DataInicio: form.get('dataInicio')?.value,
      DataFim: form.get('dataFim')?.value,
      Descricao: form.get('descricao')?.value,
      ListarParaParticipantes: form.get('listarAtvProjeto')?.value,
      Atividade: [],
      IdUsuarioCadastro: this.IdUsuarioLogado,
      Tarefa: [],
      Foto: form.get('foto')?.value
    }

    //Tarefa Usuario
    this.dataSourcelTarefaFuncoes.data.forEach(function(element){
      debugger
      let tarefa:TarefaReponsavel = {
        Tarefa: element.listTarefas,
        ResponsavelId: element.idResponsavel
      };

      projetoRequest.Tarefa.push(tarefa);
    });

    //Atividade
    this.dataSource.data.forEach(function(element){
      //Formatacao data
      let dataInicial:any = element.dataInicial.split("/"); 
      let dataFim:any = element.dataFim.split("/");

      let Atv:AtividadeRequest = {
        Atividade: element.atividade,
        DataInicial: new Date(dataInicial[2], dataInicial[1]-1, dataInicial[0]),
        DataFim:  new Date(dataFim[2], dataFim[1]-1, dataFim[0]),
        ListTarefas: element.listTarefas
      };
      
      projetoRequest.Atividade.push(Atv);
    });


    if(this.IsNew){
      this.response.Post("Projeto","Cadastrar",projetoRequest).subscribe(
        (response: RetornoPadrao) =>{        
          if(response.sucesso){
            this.toastr.success(response.mensagem, 'Mensagem:');
            this.router.navigateByUrl('/main/projeto')
          }else{
            this.toastr.error(response.mensagem, 'Mensagem:');
          }
          this.loading = false;
        }
      );
    }else{
      this.response.Post("Usuario","Editar",form.value).subscribe(
        (response: RetornoPadrao) =>{        
          if(response.sucesso){
            this.toastr.success(response.mensagem, 'Mensagem:');
            this.router.navigateByUrl('/main/usuario')
          }else{
            this.toastr.error(response.mensagem, 'Mensagem:');
          }
          this.loading = false;
        }
      );
    }
  };

  //AbaPricipal
  ChangeDataPrevisao(){
    let dataInicio = this.ProjetoRegisterFormGroup.get('dataInicio')?.value; 
    let dataFim =  this.ProjetoRegisterFormGroup.get('dataFim')?.value; 
    
    this.dataSource.data.forEach(element => {
      let dataInicioTable:any = element.dataInicial.split("/");
      let dataFimTable:any = element.dataFim.split("/");

      if(dataInicio != undefined && new Date(dataInicioTable[2], dataInicioTable[1]-1, dataInicioTable[0]) < dataInicio){
        element.dataInicial = dataInicio.toLocaleDateString();
      }

      if(dataFim != undefined && new Date(dataFimTable[2], dataFimTable[1]-1, dataFimTable[0]) > dataFim){
        element.dataFim = dataFim.toLocaleDateString();
      }
    });

    this.dataSource.filter = "";
  }

  //Aba Atvidades/Tarefas
  AdicionarGridAtividade(form:FormGroup){
    let validDuplicidade = true;
    this.submitAtv = true;

    if(this.AtividadeRegisterFormGroup.invalid){
      this.toastr.error('<small>Preencha os campos corretamente para adicionar uma atividade!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataInicioAtv')?.value > this.ProjetoRegisterFormGroup.get('dataFim')?.value){
      this.toastr.error('<small>A data de início da atividade não pode ser maior que a data final do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataInicioAtv')?.value < this.ProjetoRegisterFormGroup.get('dataInicio')?.value){
      this.toastr.error('<small>>A data de início da atividade não pode ser menor que a data inicial do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataFimAtv')?.value > this.ProjetoRegisterFormGroup.get('dataFim')?.value){
      this.toastr.error('<small>A data fim da atividade não pode ser maior que a data final do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataFimAtv')?.value < this.ProjetoRegisterFormGroup.get('dataInicio')?.value){
      this.toastr.error('<small>A data fim da atividade não pode ser menor que a data de início do projeto!</small>', 'Mensagem:');
      return;
    }

    var objAtv:GridAtvTarefas = {
      position: form.get('position')?.value ?? 0,
      atividade: form.get('atividadeDescricao')?.value,
      dataInicial: form.get('dataInicioAtv')?.value.toLocaleDateString(),
      dataFim: form.get('dataFimAtv')?.value.toLocaleDateString(),
      listTarefas: form.get('lTarefas')?.value,
    };

    //Verificação duplicidade tarefas e atvidade
    objAtv.listTarefas.forEach(element => {
      if(objAtv.listTarefas.map(x => x.descricao).indexOf(element.descricao) != 
        objAtv.listTarefas.map(x => x.descricao).lastIndexOf(element.descricao)){
        this.toastr.error('<small>Não é permitido o cadastro de tarefas iguais em uma mesma atividade!</small>', 'Mensagem:');
        validDuplicidade = false;
      }
    });

    if(!validDuplicidade){
      return;
    }

    if(objAtv.position == 0){
      this.position = this.position + 1;
      objAtv.position = this.position;
      this.dataSource.data.forEach(element => {
        if(element.atividade == objAtv.atividade){
          this.toastr.error('<small>Não é permitido o cadastro de atividades com nomes iguais!</small>', 'Mensagem:');
          validDuplicidade = false;
        }
      });
  
      if(!validDuplicidade){
        return;
      }

      this.dataSource.data.push(objAtv);
    }
    else{
      this.dataSource.data.forEach(element => {
        if(element.atividade == objAtv.atividade && element.position != objAtv.position){
          this.toastr.error('<small>Não é permitido o cadastro de atividades com nomes iguais!</small>', 'Mensagem:');
          validDuplicidade = false;
        }
      });
  
      if(!validDuplicidade){
        return;
      }

      //Editar
      for (let index = 0; index < this.dataSource.data.length; index++) {
        if(this.dataSource.data[index].position == objAtv.position){
          //Deletar Resquícios de tarefas na grid equipe
          for (let z = 0; z < this.dataSourcelTarefaFuncoes.data.length; z++) {
            for (let y = 0; y < this.dataSourcelTarefaFuncoes.data[z].listTarefas.length; y++) {
              if(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].Atividade == this.dataSource.data[index].atividade){
                //Receber novo nome
                this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].Atividade = objAtv.atividade;
                //Se não tiver mais a atividade ela é excluída
                if(objAtv.listTarefas.map(x => x.descricao).indexOf(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].Tarefa) == -1){
                  this.dataSourcelTarefaFuncoes.data[z].listTarefas.splice(y,1)
                  y=-1;
                }
              }
            } 

            if(this.dataSourcelTarefaFuncoes.data[z].listTarefas.length == 0){
              this.dataSourcelTarefaFuncoes.data.splice(z,1)
              z=-1;
            }
          }

          this.dataSourcelTarefaFuncoes.filter = "";
          this.dataSource.data[index] = objAtv;
          this.dataSource.filter = "";
          break;
        } 
      }
    }

    //Operação
    this.submitAtv = false;
    this.dataSource.filter = "";

    //Reset Campos
    this.ResetarCamposAtividades();
    this.ResetarCamposTarefaEquipe();
    this.editTarefa = false;
  }

  DeletarAtividade(Atv: GridAtvTarefas){
    //Deletar Resquícios de tarefas na grid equipe
    for (let z = 0; z < this.dataSourcelTarefaFuncoes.data.length; z++) {
      for (let y = 0; y < this.dataSourcelTarefaFuncoes.data[z].listTarefas.length; y++) {
        if(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].Atividade == Atv.atividade){
          this.dataSourcelTarefaFuncoes.data[z].listTarefas.splice(y,1)
          y=-1;
        }
      } 

      if(this.dataSourcelTarefaFuncoes.data[z].listTarefas.length == 0){
        this.dataSourcelTarefaFuncoes.data.splice(z,1)
        z=-1;
      }
    }
    this.dataSourcelTarefaFuncoes.filter = "";

    //Deletar Atividade na Grid Atividade
    for (let index = 0; index < this.dataSource.data.length; index++) {
      if(this.dataSource.data[index].position == Atv.position){
        this.dataSource.data.splice(index,1);
        this.dataSource.filter = "";
      }
    }    

    //Reset Campos
    this.ResetarCamposAtividades();
    this.ResetarCamposTarefaEquipe();
  }

  EditarAtividade(Atv: GridAtvTarefas){
    this.editTarefa = true;

    //Reset Campos
    this.ResetarCamposAtividades();
    this.ResetarCamposTarefaEquipe();
  
    let dataInicial:any = Atv.dataInicial.split('/');
    let dataFim:any = Atv.dataFim.split('/');
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(Atv.atividade);
    this.AtividadeRegisterFormGroup.get('dataInicioAtv')?.setValue(new Date(dataInicial[2], dataInicial[1]-1, dataInicial[0]));
    this.AtividadeRegisterFormGroup.get('dataFimAtv')?.setValue(new Date(dataFim[2], dataFim[1]-1, dataFim[0]));
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(Atv.listTarefas);
    this.AtividadeRegisterFormGroup.get('position')?.setValue(Atv.position);
    
    Atv.listTarefas.forEach(element => {
      this.lTarefa.push({descricao:element.descricao});
    });
  };

  ResetarCamposAtividades(){
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('dataInicioAtv')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('dataFimAtv')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('position')?.setValue(undefined);
    this.lTarefa = [];
  }
  
  applyFilterAtividades(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  Add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.lTarefa.push({descricao:value});
    }

    event.chipInput!.clear();
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(this.lTarefa);
  }

  Remove(tarefa: Tarefa): void {
    const index = this.lTarefa.indexOf(tarefa);

    if (index >= 0) {
      this.lTarefa.splice(index, 1);
    }

    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(this.lTarefa);
  }

  LimparCampoDataAtividade(){
    this.AtividadeRegisterFormGroup.get('dataFimAtv')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('dataInicioAtv')?.setValue(undefined);
  }

  //Aba Equipes/Funções
  AdicionarTarefaEquipe(form:FormGroup){
    this.submitFuncoes = true;

    if(this.FuncoesRegisterFormGroup.invalid){
      this.toastr.error('<small>Preencha os campos corretamente para atribuir uma tarefa a um integrante do projeto!</small>', 'Mensagem:');
      return;
    }

    if(this.dataSourcelTarefaFuncoes.data.map(x => x.idResponsavel).indexOf(form.get('idResponsavel')?.value) != -1 && !this.editTarefaEquipe){
      this.toastr.error('<small>Não é permitido inserir um usuário repetido! Para adicionar ou retirar tarefas desse usuário, edite o que está presente na grid!</small>', 'Mensagem:');
      return;
    }

    let objAtvFuncao:GridTarefaEquipe = {
      position: 0,
      responsavel: form.get('responsavel')?.value,
      listTarefas: form.get('listTarefas')?.value,
      idResponsavel: form.get('idResponsavel')?.value
    };

    if(form.get('position')?.value == undefined){
      objAtvFuncao.position = this.positionEquipeFuncoes++;
    }
    else{
      this.DeletarTarefaEquipe(objAtvFuncao);
    }

    //Operação
    this.submitFuncoes = false;
    this.dataSourcelTarefaFuncoes.data.push(objAtvFuncao);
    this.dataSourcelTarefaFuncoes.filter = "";

    //Reset Campos
    this.ResetarCamposTarefaEquipe();
    this.editTarefaEquipe = false;
  }

  DeletarTarefaEquipe(objAtvFuncao: GridTarefaEquipe){
    for (let index = 0; index < this.dataSourcelTarefaFuncoes.data.length; index++) {
      if(this.dataSourcelTarefaFuncoes.data[index].position == objAtvFuncao.position){
        this.dataSourcelTarefaFuncoes.data.splice(index,1);
        this.dataSourcelTarefaFuncoes.filter = "";
      }
    }
  }

  ResetarCamposTarefaEquipe(){
    this.FuncoesRegisterFormGroup.get('responsavel')?.setValue(undefined);
    this.FuncoesRegisterFormGroup.get('idResponsavel')?.setValue(undefined);
    this.FuncoesRegisterFormGroup.get('listTarefas')?.setValue(undefined);
    this.FuncoesRegisterFormGroup.get('position')?.setValue(undefined);
  }

  applyFilterTarefaEquipe(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcelTarefaFuncoes.filter = filterValue.trim().toLowerCase();
  }

  EditarTarefaEquipe(Atv: GridTarefaEquipe){
    this.editTarefaEquipe = true;
    this.ResetarCamposTarefaEquipe();
    this.FuncoesRegisterFormGroup.get('responsavel')?.setValue(Atv.responsavel);
    this.FuncoesRegisterFormGroup.get('idResponsavel')?.setValue(Atv.idResponsavel);
    this.FuncoesRegisterFormGroup.get('listTarefas')?.setValue(Atv.listTarefas);
    this.FuncoesRegisterFormGroup.get('position')?.setValue(Atv.position);
  };

  //Operacional
  ChangeFoto = (event:any) => {
    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file); 

    reader.onloadend = () => {
      let base64data: any = reader.result;
      this.ProjetoRegisterFormGroup.get('foto')?.setValue(base64data);
    }
  };

  OpenFileUpload = () => {
    document.getElementById("customFile")?.click();
  };
  
  LimparCampoDataProjeto(){
    this.ProjetoRegisterFormGroup.get('dataInicio')?.setValue(undefined);
    this.ProjetoRegisterFormGroup.get('dataFim')?.setValue(undefined);
  }

  //Interação das abas
  ControleAbas(event: number){
    this.indexTab = event;
  }

  ButtonEventAba(acc: number){
    this.indexTab += acc;
  }
}


