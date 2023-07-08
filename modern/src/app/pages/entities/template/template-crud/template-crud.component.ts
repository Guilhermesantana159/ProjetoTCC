import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseService } from 'src/factorys/services/base.service';
import { DefaultService } from 'src/factorys/services/default.service';
import { Tarefa } from 'src/app/objects/Tarefa/Tarefas';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { GridAtvTarefas } from 'src/app/objects/Tarefa/GridAtvTarefas';
import { TarefaEquipe } from 'src/app/objects/Projeto/GridTarefaEquipe';
import { ProjetoRequest, AtividadeRequest } from 'src/app/objects/Projeto/ProjetoRequest';
import { RetornoPadrao } from 'src/app/objects/RetornoPadrao';
import { BaseOptions, SelectPadrao } from 'src/app/objects/Select/SelectPadrao';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ProjetoResponse } from 'src/app/objects/Projeto/ProjetoResponse';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {CdkDragDrop,moveItemInArray,transferArrayItem,CdkDrag,} from '@angular/cdk/drag-drop';

@Component({
  selector: 'template-crud-root',
  templateUrl: './template-crud.component.html',
  styleUrls: ['../template.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class TemplateCrudComponent implements OnInit,OnDestroy{
  //Variaveis funcionais comportamento da tela tela
  loading = false;
  TemplateRegisterFormGroup: FormGroup;
  submitRegister = false;
  indexTab: number = 0;
  IsNew = true;
  IsAdmin: string = window.localStorage.getItem('Perfil') ?? "false";
  IdUsuarioLogado: number = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '1');
  disabledAll = false;

  //Variaveis Aba Atividades Tarefas
  AtividadeRegisterFormGroup: FormGroup;
  submitAtv: boolean = false;
  lTarefa: any[] = [];
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  data: GridAtvTarefas[] = [];
  @ViewChild("customNav") nav: any;
  dataSource = new MatTableDataSource(this.data);
  addOnBlur = true;
  atividade!: Observable<any>;
  position: number = 0;
  editTarefa: boolean = false;
  tarefaFormGroup!: FormGroup;
  ltags:Array<string> = []

  //Aba Principal
  optionsCategoria: Array<BaseOptions> = [
    { 
      description: "Outros",
      value: 0
    }];
  
  index: any;

  //CronoGrama
  lCrongrama = [];

  constructor(private formBuilder: FormBuilder,private modalService: NgbModal,private response: BaseService,private defaultService: DefaultService,private router: Router,
    private route: ActivatedRoute,private toastr: ToastrService,private changeDetectorRef: ChangeDetectorRef) {    
    
    this.TemplateRegisterFormGroup = this.formBuilder.group({
      idProjeto: [undefined],
      titulo: [undefined, [Validators.required]],
      escalaTempo: ["0", [Validators.required]],
      quantidade: [undefined, [Validators.required]],
      categoria: ["0", [Validators.required]],      
      newCategoria: [undefined, [Validators.required]],      
      descricao: [undefined, [Validators.required]],     
      foto: [undefined],
      idUsuarioCadastro: [undefined],
      dataCadastro: [undefined]
    });

    this.AtividadeRegisterFormGroup = this.formBuilder.group({
      idAtividade: [undefined],
      statusAtividade: [undefined],
      atividadeDescricao: [undefined, [Validators.required]],
      escalaTempoAtividade: [undefined, [Validators.required]],
      lTarefas: [undefined, [Validators.required]],
      position: [undefined]
    });

    this.tarefaFormGroup = this.formBuilder.group({
      descricao: [undefined, [Validators.required]],
      prioridade: ["0"],
      descricaoTarefa: [undefined],
      atividade: [undefined, [Validators.required]],
      lTagsTarefa: [[]]
    });
  }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.atividade = this.dataSource.connect();

    this.response.Get("Template","ConsultarCategorias/").subscribe(
      (response: SelectPadrao) =>{        
        if(response.sucesso){
          response.data.forEach(element => {
           this.optionsCategoria.push(element);    
          });
        }
        else{
          this.toastr.error(response.mensagem, 'Mensagem:');
        }
    });

    this.route.params.subscribe(params => {
      //Load Edit
      if(params['id'] != undefined){
        this.loading = true;

        this.response.Get("Projeto","ConsultarViaId/" + params['id']).subscribe(
        (response: ProjetoResponse) =>{        
          if(response.sucesso){
            this.IsNew = false;
            this.TemplateRegisterFormGroup.patchValue(response.data);

            response.data.listAtividade.forEach(element => {
              this.position = this.position + 1;
              element.position = this.position;
              this.dataSource.data.push(element);
              this.dataSource.filter = "";          
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
    }
  }

  //Operacional tela
  Salvar = (form:FormGroup) =>{
    this.loading = true;
    this.submitRegister = true;

    if(this.TemplateRegisterFormGroup.invalid){
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

  VerificarValor(){
    let value = parseInt(this.TemplateRegisterFormGroup.get('quantidade')?.value);
    let escala = this.TemplateRegisterFormGroup.get('escalaTempo')?.value == "0" ? 'dias' : 'semanas' 

    for (let index = 0; index < this.dataSource.data.length; index++) {
      if(parseInt(this.dataSource.data[index].escalaTempoAtividade ?? '0') > value){
        this.dataSource.data[index].escalaTempoAtividade = value.toString();
        this.dataSource.filter = "";
      }
    }    


    if(value > 999){
      this.TemplateRegisterFormGroup.get('quantidade')?.setValue(999);
      this.toastr.warning('<small>Valor máximo de '+ escala +' é 999!</small>', 'Mensagem:');
      return
    }

    let valueAtividade = parseInt(this.AtividadeRegisterFormGroup.get('escalaTempoAtividade')?.value);
    if(valueAtividade > value){
      this.AtividadeRegisterFormGroup.get('escalaTempoAtividade')?.setValue(value);
      this.toastr.warning('<small>Valor inválido: Este valor ultrapassa a quantidade máxima de '+ escala +' cadastrada para o template!</small>', 'Mensagem:');
      return
    }
  }

  //Aba Atvidades/Tarefas
  AdicionarGridAtividade(form:FormGroup){
    let validDuplicidade = true;
    this.submitAtv = true;

    if(this.AtividadeRegisterFormGroup.invalid){
      this.toastr.error('<small>Preencha os campos corretamente para adicionar uma atividade!</small>', 'Mensagem:');
      return;
    }

    if(this.TemplateRegisterFormGroup.get('quantidade')?.value == undefined || this.TemplateRegisterFormGroup.get('quantidade')?.value == "0"){
      this.toastr.error('<small>Preencha o período estimado do template para adicionar uma atividade!</small>', 'Mensagem:');
      return;
    }

    var objAtv:GridAtvTarefas = {
      idAtividade: form.get('idAtividade')?.value,
      statusAtividade: undefined,
      position: form.get('position')?.value ?? 0,
      escalaTempoAtividade:  form.get('escalaTempoAtividade')?.value,
      atividade: form.get('atividadeDescricao')?.value,
      dataInicial: '',
      dataFim: '',
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
    this.editTarefa = false;
  }

  DeletarAtividade(Atv: GridAtvTarefas){
    //Deletar Atividade na Grid Atividade
    for (let index = 0; index < this.dataSource.data.length; index++) {
      if(this.dataSource.data[index].position == Atv.position){
        this.dataSource.data.splice(index,1);
        this.dataSource.filter = "";
      }
    }    

    //Reset Campos
    this.ResetarCamposAtividades();
  }

  EditarAtividade(Atv: GridAtvTarefas){
    this.editTarefa = true;

    //Reset Campos
    this.ResetarCamposAtividades();
  
    this.AtividadeRegisterFormGroup.get('idAtividade')?.setValue(Atv.idAtividade);
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(Atv.atividade);
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(Atv.listTarefas);
    this.AtividadeRegisterFormGroup.get('position')?.setValue(Atv.position);
    this.AtividadeRegisterFormGroup.get('escalaTempoAtividade')?.setValue(Atv.escalaTempoAtividade);
    
    Atv.listTarefas.forEach(element => {
      this.lTarefa.push({descricao:element.descricao,idTarefa: element.idTarefa});
    });
  };

  ResetarCamposAtividades(){
    this.AtividadeRegisterFormGroup.get('idAtividade')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('atividadeDescricao')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('lTarefas')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('escalaTempoAtividade')?.setValue(undefined);
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

  AddTags(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.ltags.push(value);
    }

    event.chipInput!.clear();
    this.tarefaFormGroup.get('lTagsTarefa')?.setValue(this.ltags);
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

  RemoveTag(tag: string): void {
    const index = this.ltags.indexOf(tag);

    if (index >= 0) {
      this.ltags.splice(index, 1);
    }

    this.tarefaFormGroup.get('lTagsTarefa')?.setValue(this.ltags);
  }

  LimparCampoDataAtividade(){
    this.AtividadeRegisterFormGroup.get('dataFimAtv')?.setValue(undefined);
    this.AtividadeRegisterFormGroup.get('dataInicioAtv')?.setValue(undefined);
  }

  
  ComplementarTarefa(form:FormGroup){
   for (let index = 0; index < this.dataSource.data.length; index++) {
      if(this.dataSource.data[index].atividade == form.get('atividade')?.value){
        this.dataSource.data[index].listTarefas.forEach(element => {
            if(element.descricao == form.get('descricao')?.value){
              element.prioridade = form.get('prioridade')?.value;
              element.descricaoTarefa = form.get('descricaoTarefa')?.value;
              element.lTagsTarefa = form.get('lTagsTarefa')?.value;
            }
        });
      }
    }    
    this.modalService.dismissAll();
    this.ResetCamposTarefa();
  }

  //Operacional
  ChangeFoto = (event:any) => {
    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file); 

    reader.onloadend = () => {
      let base64data: any = reader.result;
      this.TemplateRegisterFormGroup.get('foto')?.setValue(base64data);
    }
  };

  OpenFileUpload = () => {
    document.getElementById("customFile")?.click();
  };
  

  openModal(content: any) {
    this.modalService.open(content, { size: 'md', centered: true });
  }

  openModalcomplementar(content: any,tarefa: any,atividade:string) {
    this.ResetCamposTarefa();

    //Edição
    this.tarefaFormGroup.get('descricao')?.setValue(tarefa.descricao);
    this.tarefaFormGroup.get('prioridade')?.setValue(tarefa.prioridade);
    this.tarefaFormGroup.get('descricaoTarefa')?.setValue(tarefa.descricaoTarefa);
    this.tarefaFormGroup.get('atividade')?.setValue(atividade);
    this.tarefaFormGroup.get('lTagsTarefa')?.setValue(tarefa.lTagsTarefa);

    if(tarefa.lTagsTarefa != undefined){
      this.ltags = tarefa.lTagsTarefa;
    }

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

  ResetCamposTarefa(){
    this.tarefaFormGroup.get('descricao')?.setValue(undefined);
    this.tarefaFormGroup.get('prioridade')?.setValue(undefined);
    this.tarefaFormGroup.get('descricaoTarefa')?.setValue(undefined);
    this.tarefaFormGroup.get('atividade')?.setValue(undefined);
    this.tarefaFormGroup.get('lTagsTarefa')?.setValue([]);
    this.ltags = [];
  }

  //Cronograma
  items = ['Carrots', 'Tomatoes', 'Onions', 'Apples', 'Avocados'];

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  evenPredicate(item: CdkDrag<number>) {
    return item.data % 2 === 0;
  }

  noReturnPredicate() {
    return false;
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


