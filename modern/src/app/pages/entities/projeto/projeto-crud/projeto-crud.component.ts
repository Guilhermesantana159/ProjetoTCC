import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseService } from 'src/factorys/services/base.service';
import { DefaultService } from 'src/factorys/services/default.service';
import { ConsultaModalParams } from 'src/app/objects/Consulta-Padrao/ConsultaModalParams';
import { Tarefa } from 'src/app/objects/Tarefa/Tarefas';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { GridAtvTarefas } from 'src/app/objects/Tarefa/GridAtvTarefas';
import { GridTarefaEquipe, TarefaEquipe } from 'src/app/objects/Projeto/GridTarefaEquipe';
import { TarefaReponsavel } from 'src/app/objects/Tarefa/TarefaResponsavel';
import { ProjetoRequest, AtividadeRequest } from 'src/app/objects/Projeto/ProjetoRequest';
import { RetornoPadrao } from 'src/app/objects/RetornoPadrao';
import { BaseOptions } from 'src/app/objects/Select/SelectPadrao';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ProjetoResponse } from 'src/app/objects/Projeto/ProjetoResponse';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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

export class ProjetoCrudComponent implements OnInit,OnDestroy{
  //Variaveis funcionais comportamento da tela tela
  loading = false;
  ProjetoRegisterFormGroup: FormGroup;
  submitRegister = false;
  indexTab: number = 0;
  IsNew = true;
  IsAdmin: string = window.localStorage.getItem('Perfil') ?? "false";
  IdUsuarioLogado: number = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '1');
  disabledAll = true;

  //Variaveis Aba Equipe e Funções
  FuncoesRegisterFormGroup: FormGroup;
  submitFuncoes: boolean = false;
  datalTarefaFuncoes: GridTarefaEquipe[] = []
  dataSourcelTarefaFuncoes = new MatTableDataSource(this.datalTarefaFuncoes);
  editTarefaEquipe: boolean = false;
  positionlTarefaFuncoes: number = 0;
  editlTarefaFuncoes: boolean = false;
  tarefas!: Observable<any>;

  //Variaveis Aba Atividades Tarefas
  AtividadeRegisterFormGroup: FormGroup;
  submitAtv: boolean = false;
  lTarefa: Tarefa[] = [];
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  data: GridAtvTarefas[] = [];
  @ViewChild("customNav") nav: any;
  dataSource = new MatTableDataSource(this.data);
  addOnBlur = true;
  atividade!: Observable<any>;
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

  constructor(private formBuilder: FormBuilder,private modalService: NgbModal,private response: BaseService,private defaultService: DefaultService,private router: Router,
    private route: ActivatedRoute,private toastr: ToastrService,private changeDetectorRef: ChangeDetectorRef) {    
    this.paramsConsultaUsuario = {
      Label: 'Responsável pela Tarefa',
      Title: 'Consulta de Usuários',
      Disabled: false,
      Class: 'col-sm-12 col-xs-8 col-md-8 col-lg-8',
      Required: true,
      GridOptions: defaultService.Modal.ConsultaPadraoUsuario,
      SelectedText: '',
      SelectedValue: '',
      OnlyButton: false
    };

    this.ProjetoRegisterFormGroup = this.formBuilder.group({
      idProjeto: [undefined],
      titulo: [undefined, [Validators.required]],
      dataFim: [null, [Validators.required]],
      dataInicio: [undefined, [Validators.required]],      
      descricao: [undefined],     
      listarAtvProjeto: [false],
      foto: [undefined],
      idUsuarioCadastro: [undefined],
      dataCadastro: [undefined]
    });

    this.AtividadeRegisterFormGroup = this.formBuilder.group({
      idAtividade: [undefined],
      statusAtividade: [undefined],
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
  }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.atividade = this.dataSource.connect();
    this.tarefas = this.dataSourcelTarefaFuncoes.connect();

    this.route.params.subscribe(params => {
      //Load Edit
      if(params['id'] != undefined){
        this.loading = true;

        this.response.Get("Projeto","ConsultarViaId/" + params['id']).subscribe(
        (response: ProjetoResponse) =>{        
          if(response.sucesso){
            this.IsNew = false;
            this.ProjetoRegisterFormGroup.patchValue(response.data);

            response.data.listAtividade.forEach(element => {
              this.position = this.position + 1;
              element.position = this.position;
              this.dataSource.data.push(element);
              this.dataSource.filter = "";          
            });

            response.data.listTarefa.forEach(element => {
              this.positionlTarefaFuncoes = this.positionlTarefaFuncoes + 1;
              element.position = this.positionlTarefaFuncoes;
              this.dataSourcelTarefaFuncoes.data.push(element);
              this.dataSourcelTarefaFuncoes.filter = "";          
            });

            if(response.data.status == 1 || response.data.status == 2){
              this.disabledAll = true;
              this.toastr.info('<small>O cadastro de projeto está no modo visualização!</small>', 'Mensagem:');
            }
            else{
              this.disabledAll = false;
            }

          }
          else{
            this.toastr.error(response.mensagem, 'Mensagem:');
          }

        this.loading = false;
      });
    }else{
      this.disabledAll = false;
    }});
  }

  ngOnDestroy() {
    if (this.dataSource) { 
      this.dataSource.disconnect(); 
      this.dataSourcelTarefaFuncoes.disconnect(); 
    }
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
        IdAtividade: element.idAtividade,
        StatusAtividade: element.statusAtividade == undefined ? 0 : element.statusAtividade,
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
            this.router.navigateByUrl('/main-dashboard/entities/projeto')
          }else{
            this.toastr.error(response.mensagem, 'Mensagem:');
          }
          this.loading = false;
        }
      );
    }else{
      this.response.Post("Projeto","Editar",projetoRequest).subscribe(
        (response: RetornoPadrao) =>{        
          if(response.sucesso){
            this.toastr.success(response.mensagem, 'Mensagem:');
            this.router.navigateByUrl('/main-dashboard/entities/projeto')
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

    if(this.ProjetoRegisterFormGroup.get('dataInicio')?.value == undefined){
      this.toastr.error('<small>Preencha corretamente a data início do projeto!</small>', 'Mensagem:');
      return;
    }

    if(this.ProjetoRegisterFormGroup.get('dataFim')?.value == undefined){
      this.toastr.error('<small>Preencha corretamente a data fim do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataInicioAtv')?.value > this.ProjetoRegisterFormGroup.get('dataFim')?.value && this.IsNew){
      this.toastr.error('<small>A data de início da atividade não pode ser maior que a data final do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataInicioAtv')?.value < this.ProjetoRegisterFormGroup.get('dataInicio')?.value ){
      this.toastr.error('<small>A data de início da atividade não pode ser menor que a data inicial do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataFimAtv')?.value > this.ProjetoRegisterFormGroup.get('dataFim')?.value && this.IsNew){
      this.toastr.error('<small>A data fim da atividade não pode ser maior que a data final do projeto!</small>', 'Mensagem:');
      return;
    }

    if(form.get('dataFimAtv')?.value < this.ProjetoRegisterFormGroup.get('dataInicio')?.value){
      this.toastr.error('<small>A data fim da atividade não pode ser menor que a data de início do projeto!</small>', 'Mensagem:');
      return;
    }

    var objAtv:GridAtvTarefas = {
      idAtividade: form.get('idAtividade')?.value,
      statusAtividade: form.get('statusAtividade')?.value,
      position: form.get('position')?.value ?? 0,
      escalaTempoAtividade: undefined,
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
              if(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].atividade == this.dataSource.data[index].atividade){
                //Receber novo nome
                this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].atividade = objAtv.atividade;
                //Se não tiver mais a atividade ela é excluída
                if(objAtv.listTarefas.map(x => x.descricao).indexOf(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].tarefa) == -1){
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
        if(this.dataSourcelTarefaFuncoes.data[z].listTarefas[y].atividade == Atv.atividade){
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
    this.AtividadeRegisterFormGroup.get('idAtividade')?.setValue(Atv.idAtividade);
    this.AtividadeRegisterFormGroup.get('statusAtividade')?.setValue(Atv.statusAtividade);
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(Atv.atividade);
    this.AtividadeRegisterFormGroup.get('dataInicioAtv')?.setValue(new Date(dataInicial[2], dataInicial[1]-1, dataInicial[0]));
    this.AtividadeRegisterFormGroup.get('dataFimAtv')?.setValue(new Date(dataFim[2], dataFim[1]-1, dataFim[0]));
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(Atv.listTarefas);
    this.AtividadeRegisterFormGroup.get('position')?.setValue(Atv.position);
    
    Atv.listTarefas.forEach(element => {
      this.lTarefa.push({descricao:element.descricao,idTarefa: element.idTarefa});
    });
  };

  ResetarCamposAtividades(){
    this.AtividadeRegisterFormGroup.get('idAtividade')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('statusAtividade')?.setValue(undefined);
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
      this.lTarefa.push({descricao:value,idTarefa: undefined});
    }

    event.chipInput!.clear();
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(this.lTarefa);
  }

  Remove(tarefa: Tarefa): void {
    if(tarefa.idTarefa != undefined)
    {
      return
    }

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
      position: form.get('position')?.value ?? 0,
      responsavel: form.get('responsavel')?.value,
      listTarefas: form.get('listTarefas')?.value,
      idResponsavel: form.get('idResponsavel')?.value
    };

    if(objAtvFuncao.position == 0){
      objAtvFuncao.position = this.positionlTarefaFuncoes + 1;
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
    console.log(Atv.listTarefas);
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
  
  LimparCampoDataProjetoDataInicio(){
    this.ProjetoRegisterFormGroup.get('dataFim')?.setValue(undefined);
  }

  LimparCampoDataProjetoDataFim(){
    this.ProjetoRegisterFormGroup.get('dataFim')?.setValue(undefined);
  }

  openModal(content: any) {
    this.modalService.open(content, { size: 'md', centered: true });
  }

  DeletarAtividadeExistente(Atv: GridAtvTarefas){
    if(Atv == undefined){
      return
    }

    let lAtvExistentes = [];

    this.dataSource.data.forEach(element => {
      if(element.idAtividade != undefined){
        lAtvExistentes.push(element);
      }
    });

    if(lAtvExistentes.length <= 1){
      this.toastr.error('<small>Não é permitido deletar esta atividade pois é necessário ter pelo menos uma cadastrada no projeto!</small>', 'Mensagem:');
      return
    }

    this.response.Post("Projeto","DeletarAtividade/" + Atv.idAtividade,null).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success(response.mensagem, 'Mensagem:');
          this.DeletarAtividade(Atv);
        }else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }
        this.loading = false;
      }
    );

  }

  //Interação das abas
   ControleAbas(event: number){
    this.indexTab = event;
  }

  ButtonEventAba(acc: number){
    this.indexTab += acc;
    this.nav.select(this.indexTab);
  }

  compareTarefaObjects(object1: TarefaEquipe, object2: TarefaEquipe) {
    return object1 && object2 && (object1.tarefa == object2.tarefa && object1.atividade == object2.atividade);
  }
}


