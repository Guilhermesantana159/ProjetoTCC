import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from 'src/factorys/base.service';
import { Modulo, EstruturaMenu } from 'src/objects/Menus/EstruturaMenu';
import { DataNotificacao } from 'src/objects/Notificacao/NotificacaoResponse';

@Component({
  selector: 'sidebar-main',
  templateUrl: './sidebar-main.component.html',
  styleUrls: ['sidebar-main.component.scss']
})

export class SidebarMainComponent{
  usuarioNome: string | null;
  estruturaMenu: Array<Modulo> = [];
  loading: boolean;
  fullscreen: boolean = false;
  control = new FormControl('');
  fotoUser: string = window.localStorage.getItem('Foto') ?? "";
  listNotificacao: Array<DataNotificacao> = [];
  idUsuarioLogado: number = 0;
  notificaoSemLer: boolean = false;
  quantidadeSemLer: number = 0;

  constructor(private toastr: ToastrService,private response: BaseService,private router: Router){
    this.idUsuarioLogado = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '0');
    this.loading = true;
    this.usuarioNome = window.localStorage.getItem('NomeUsuario');
    this.response.Get("EstruturaMenu","ConsultarEstruturaMenus/" + this.idUsuarioLogado.toString()).subscribe(
      (response: EstruturaMenu) =>{        
        if(response.sucesso){
          if(response.data.lModulos != null){
            for(var i=0;i<response.data.lModulos.length;i++){
              this.estruturaMenu.push(response.data.lModulos[i]);
            }
          }
          
          this.loading = false;
        }else{
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
      }
    ); 

  }
}
