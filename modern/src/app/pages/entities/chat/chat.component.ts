import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, Validators, UntypedFormGroup, FormGroup } from '@angular/forms';
import { GroupUser, ChatUser, ChatMessage } from './chat.model';
import {   chatData, chatMessagesData } from './data';
import { NgbModal, NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';

// Date Format
import {DatePipe} from '@angular/common';

// Light Box
import { Lightbox } from 'ngx-lightbox';
import { BaseService } from 'src/factorys/services/base.service';
import { ToastrService } from 'ngx-toastr';
import { ChatContatoResponse } from 'src/app/objects/Chat/ChatContatoResponse';
import { RetornoPadrao } from 'src/app/objects/RetornoPadrao';
import { ConsultaModalParams } from 'src/app/objects/Consulta-Padrao/ConsultaModalParams';
import { DefaultService } from 'src/factorys/services/default.service';
import { ChatContatos } from 'src/app/objects/Chat/ChatContatos';
import { ContatoResponse, DataContatoResponse } from 'src/app/objects/Chat/ContatoResponseResponse';
import { EStatusContato } from 'src/app/enums/EStatusContato';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})

/**
 * Chat Component
 */
export class ChatComponent implements OnInit {
  chatData!: ChatUser[];
  chatMessagesData!: ChatMessage[];
  formData!: UntypedFormGroup;
  usermessage!: string;
  isFlag: boolean = false;
  submitted = false;
  isStatus: string = 'online';
  isProfile: string = 'assets/images/users/avatar-2.jpg';
  username: any = 'Lisa Parker';
  @ViewChild('scrollRef') scrollRef:any;
  isreplyMessage = false;
  emoji = '';

  images: { src: string; thumb: string; caption: string }[] = [];

  //Operação
  loadingSidebar: boolean = false;
  loadingInfoContato: boolean = false;
  idUsuario:number = parseInt(window.localStorage.getItem('IdUsuario') ?? '0');

  //Contatos
  paramsConsultaUsuario: ConsultaModalParams;
  ContatoFormGroup: FormGroup;
  lContatos: ChatContatos[] = [];

  //Grupo
  isNewGrupo:boolean = true;
  GrupoFormGroup: FormGroup;
  submitRegisterGrupo: Boolean = false;
  lContatosGrupo: DataContatoResponse[] = [];

  constructor(public formBuilder: UntypedFormBuilder, private lightbox: Lightbox, 
    private offcanvasService: NgbOffcanvas, private datePipe: DatePipe,private response: 
    BaseService,private toastr: ToastrService,private defaultService: DefaultService,private modalService: NgbModal) { 

    this.paramsConsultaUsuario = {
      Label: '',
      Title: 'Adicionar contato',
      Disabled: false,
      Class: 'col-sm-12 col-xs-8 col-md-8 col-lg-8',
      Required: false,
      GridOptions: defaultService.Modal.ConsultaPadraoUsuario,
      SelectedText: '',
      SelectedValue: '',
      OnlyButton: true
    };

    //Adicionar contato
    this.ContatoFormGroup = this.formBuilder.group({
      responsavel: [undefined, [Validators.required]],
      idResponsavel: [undefined, [Validators.required]]
    });

    this.ContatoFormGroup.get('idResponsavel')?.valueChanges.subscribe(value => {
      this.adicionarContato(value);
    });

    this.GrupoFormGroup = this.formBuilder.group({
      idGrupo: [undefined],
      titulo: [undefined, [Validators.required]],
      contatos: [undefined, [Validators.required]]
    });
  
    
    for (let i = 1; i <= 24; i++) {
      const src = '../../../../assets/images/small/img-' + i + '.jpg';
      const caption = 'Image ' + i + ' caption here';
      const thumb = '../../../../assets/images/small/img-' + i + '-thumb.jpg';
      const item = {
        src: src,
        caption: caption,
        thumb: thumb
      };
      this.images.push(item);
    }

  }

  ngOnInit(): void {
    this.loadingSidebar = true;

    //Iniciar tela
    this.consultarContatos()

    this.formData = this.formBuilder.group({
      message: ['', [Validators.required]],
    });

    //Iniciar mensagens
    this.onListScroll();
  }

  consultarContatos(){
    this.response.Get("Chat","ConsultarContatoPorIdPessoa/" + this.idUsuario).subscribe(
      (response: ChatContatoResponse) =>{        
        if(response.sucesso){
          let groupedContacts = response.data.lContatos.reduce((groups: { [title: string]: DataContatoResponse[] }, contato) => {
            let firstLetter = contato.nome?.charAt(0).toUpperCase();
            if (firstLetter) {
              groups[firstLetter] = groups[firstLetter] || [];
              groups[firstLetter].push({
                nome: contato.nome,
                foto: contato.foto,
                statusContato: contato.statusContato,
                idUsuarioContato: contato.idUsuarioContato,
                idContatoChat: contato.idContatoChat
              });
            }
            return groups;
          }, {});

          for (const title in groupedContacts) {
            if (groupedContacts.hasOwnProperty(title)) {
              this.lContatos.push({
                title: title,
                contacts: groupedContacts[title],
              });
            }
          }

          response.data.lContatos.forEach(element => {
            this.lContatosGrupo.push(element)
          });
        }
        else
        {
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }

        this.loadingSidebar = false;
        this._fetchData();
      }
    );
  };

  adicionarContato(idUsuarioContato:number){
    let validContato = true;
    this.lContatos.forEach(element => {
      if(this.ContatoFormGroup.get('responsavel')?.value.substring(0,1).toUpperCase() == element.title){
        let index = element.contacts.findIndex(objeto => objeto.idUsuarioContato === idUsuarioContato);

        if (index !== -1) {
          validContato = false;
          return
        }
      }
    });

    if(!validContato){
      this.toastr.warning('<small>Usuário já adicionado como contato!</small>', 'Mensagem:');
      return
    }

    if(idUsuarioContato == this.idUsuario){
      return;""
    }

    this.loadingSidebar = true;
    this.response.Post("Chat","CadastrarContato/",{
      IdUsuarioCadastro: this.idUsuario,
      IdUsuarioContato: idUsuarioContato
    }).subscribe(
      (response: ContatoResponse) =>{        
        if(response.sucesso){
          debugger
          if(this.lContatos.length == 0){
            this.lContatos.push({
              title: response.data.nome.substring(0,1).toUpperCase(),
              contacts: [response.data]
            });
          }else{
            let newTitle = true;
            this.lContatos.forEach(element => {
              if(element.title.toUpperCase() == (response.data.nome.substring(0,1)).toUpperCase()){
                element.contacts.push(response.data);
                newTitle = false;
              }
            });

            if(newTitle){
              this.lContatos.push({
                title: response.data.nome.substring(0,1).toUpperCase(),
                contacts: [response.data]
              });
            }
          }
          
        }
        else
        {
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }

        this.loadingSidebar = false;
      }
    );
  }

  excluirContato(contato:DataContatoResponse,list: ChatContatos){
    let index = list.contacts.findIndex(objeto => objeto.idContatoChat === contato.idContatoChat);

    if (index !== -1) {
      list.contacts.splice(index, 1);

      if(list.contacts.length == 0){
        index = this.lContatos.findIndex(objeto => objeto.title === list.title);

        if (index !== -1) {
          this.lContatos.splice(index, 1);
        }
      }
    }

    this.response.Post("Chat","DeletarContato/" + contato.idContatoChat,{}).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
        else
        {
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
      }
    );
  }

  infoUsuario(content: TemplateRef<any>) {    
    this.offcanvasService.open(content, { position: 'end' });
  }

  ngAfterViewInit() {
    this.scrollRef.SimpleBar.getScrollElement().scrollTop = 300;
    this.onListScroll();
  }

  alterarStatusContato(contato:DataContatoResponse,status: EStatusContato){
    if(contato.statusContato == EStatusContato.Disponivel){
      contato.statusContato = status;
    }else if(contato.statusContato == status){
      contato.statusContato = EStatusContato.Disponivel;
      status = EStatusContato.Disponivel
    }

    this.response.Post("Chat","AlterarStatusContato/",{
      IdContatoChat: contato.idContatoChat,
      NewStatus: status
    }).subscribe(
      (response: RetornoPadrao) =>{        
        if(response.sucesso){
          this.toastr.success('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
        else
        {
          if(status != EStatusContato.Disponivel){
            this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
          }
        }
      }
    );
  };

  openModal(content: any) {
    this.modalService.open(content, { size: 'md', centered: true });
  }



  // Chat Data Fetch
  private _fetchData() {
    this.chatData = chatData;
    this.chatMessagesData = chatMessagesData;
  }

  onListScroll() {
    if (this.scrollRef !== undefined) {
      setTimeout(() => {
        this.scrollRef.SimpleBar.getScrollElement().scrollTop = this.scrollRef.SimpleBar.getScrollElement().scrollHeight;
      }, 500);
    }
  }

  /**
   * Returns form
   */
   get form() {
    return this.formData.controls;
  }

  /**
   * Save the message in chat
   */
  messageSave() {
    const message = this.formData.get('message')!.value;  
    if (this.isreplyMessage == true) {
     var itemReplyList:any = document.getElementById("users-chat")?.querySelector(".chat-conversation-list");
     var dateTime = this.datePipe.transform(new Date(),"h:mm a");
     var chatReplyUser = (document.querySelector(".replyCard .replymessage-block .flex-grow-1 .conversation-name") as HTMLAreaElement).innerHTML;
     var chatReplyMessage = (document.querySelector(".replyCard .replymessage-block .flex-grow-1 .mb-0")as HTMLAreaElement).innerText;
 
     this.chatMessagesData.push({
       align: 'right',
       name: 'Marcus',
       replayName: chatReplyUser,
       replaymsg: chatReplyMessage,
       message,
       time: dateTime,
     });
     this.onListScroll();
 
   // Set Form Data Reset
   this.formData = this.formBuilder.group({
     message: null,
   });
   this.isreplyMessage = false;

    }
    else{
     if (this.formData.valid && message) {
       // Message Push in Chat
       this.chatMessagesData.push({
         align: 'right',
         name: 'Marcus',
         message,
         time: this.datePipe.transform(new Date(),"h:mm a"),
       });
       this.onListScroll();
       // Set Form Data Reset
       this.formData = this.formBuilder.group({
         message: null,
       });
     }
   }
   document.querySelector('.replyCard')?.classList.remove('show');
   this.emoji = '';
   
   this.submitted = true;
  }

  /***
  * OnClick User Chat show
  */
  chatUsername(name: string, profile: any, status: string) {
    this.isFlag = true;
    this.username = name;
    const currentDate = new Date();
    this.isStatus = status;
    this.isProfile = profile ? profile : 'assets/images/users/user-dummy-img.jpg';
    this.chatMessagesData.map((chat) => { if (chat.profile) { chat.profile = this.isProfile } });
    const userChatShow = document.querySelector('.user-chat');
    if(userChatShow != null){
      userChatShow.classList.add('user-chat-show');
    }
  }

   /**
   * SidebarHide modal
   * @param content modal content
   */
  SidebarHide() {
    const recentActivity = document.querySelector('.user-chat');
      if(recentActivity != null){
        recentActivity.classList.remove('user-chat-show');
      }
  }

  open(index: number): void {
    // open lightbox
    this.lightbox.open(this.images, index, { });
  }

   // Contact Search
   ContactSearch(){
    var input:any, filter:any, ul:any, li:any, a:any | undefined, i:any, txtValue:any;
    input = document.getElementById("searchContact") as HTMLAreaElement;
    filter = input.value.toUpperCase();
    ul = document.querySelectorAll(".chat-user-list");
    ul.forEach((item:any)=>{
      li = item.getElementsByTagName("li");
      for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("p")[0];
        txtValue = a?.innerText;
        if (txtValue?.toUpperCase().indexOf(filter) > -1) {
          li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
      }
    })    
  }

  // Message Search
  MessageSearch(){
    var input:any, filter:any, ul:any, li:any, a:any | undefined, i:any, txtValue:any;
    input = document.getElementById("searchMessage") as HTMLAreaElement;
    filter = input.value.toUpperCase();
    ul = document.getElementById("users-conversation");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
      a = li[i].getElementsByTagName("p")[0];
      txtValue = a?.innerText;
      if (txtValue?.toUpperCase().indexOf(filter) > -1) {
        li[i].style.display = "";
    } else {
        li[i].style.display = "none";
    }
    }
  }



  // Replay Message
  replyMessage(event:any,align:any){
    this.isreplyMessage = true;
    document.querySelector('.replyCard')?.classList.add('show');
    var copyText = event.target.closest('.chat-list').querySelector('.ctext-content').innerHTML;
    (document.querySelector(".replyCard .replymessage-block .flex-grow-1 .mb-0") as HTMLAreaElement).innerHTML = copyText;
    var msgOwnerName:any = event.target.closest(".chat-list").classList.contains("right") == true ? 'You' : document.querySelector('.username')?.innerHTML;
    (document.querySelector(".replyCard .replymessage-block .flex-grow-1 .conversation-name") as HTMLAreaElement).innerHTML = msgOwnerName;
  }

  // Copy Message
  copyMessage(event:any){
    navigator.clipboard.writeText(event.target.closest('.chat-list').querySelector('.ctext-content').innerHTML);
    (document.getElementById("copyClipBoard") as HTMLElement).style.display = "block";
    setTimeout(() => {
    (document.getElementById("copyClipBoard") as HTMLElement).style.display = "none";
    }, 1000);
  }

  // Delete Message
  deleteMessage(event:any){
    event.target.closest('.chat-list').remove();
  }

  // Delete All Message
  deleteAllMessage(event:any){
    var allMsgDelete:any = document.getElementById('users-conversation')?.querySelectorAll('.chat-list');
    allMsgDelete.forEach((item:any)=>{
      item.remove();
    })
  }

   // Emoji Picker
   showEmojiPicker = false;
   sets:any = [
     'native',
     'google',
     'twitter',
     'facebook',
     'emojione',
     'apple',
     'messenger'
   ]
   set:any = 'twitter';
   toggleEmojiPicker() {
     this.showEmojiPicker = !this.showEmojiPicker;
   }
 
   addEmoji(event:any) {
     const { emoji } = this;
     const text = `${emoji}${event.emoji.native}`;
     this.emoji = text;
     this.showEmojiPicker = false;
   }
 
   onFocus() {
     this.showEmojiPicker = false;
   }
   onBlur() {
   }

   closeReplay(){
    document.querySelector('.replyCard')?.classList.remove('show');
  }

  /**
   * Delete Chat Contact Data 
   */
   delete(event:any) {
    event.target.closest('li')?.remove();
  }

}
