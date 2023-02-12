import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { BaseService } from 'src/factorys/base.service';
import { ToastrService } from 'ngx-toastr';
import { EstruturaMenu, Modulo } from 'src/objects/Menus/EstruturaMenu';
import { FormControl } from '@angular/forms';
import { Observable, startWith, map } from 'rxjs';
import { DataNotificacao, NotificacaoResponse } from '../../../objects/Notificacao/NotificacaoResponse';
import { ELido } from '../../../enums/ELido';
import { NotificacaoRequest } from '../../../objects/Notificacao/NotificacaoRequestLida';

@Component({
  selector: 'main-root',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']

})

export class MainComponent implements OnInit{
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
    this.loading = true;
    this.usuarioNome = window.localStorage.getItem('NomeUsuario');
    this.response.Get("EstruturaMenu","ConsultarEstruturaMenus").subscribe(
      (response: EstruturaMenu) =>{        
        if(response.sucesso){
          for(var i=0;i<response.data.lModulos.length;i++){
            this.estruturaMenu.push(response.data.lModulos[i]);
          }
          this.loading = false;
        }else{
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
      }
    ); 

    this.idUsuarioLogado = Number.parseInt(window.localStorage.getItem('IdUsuario') ?? '0');

    this.response.Get("Notificacao","GetNotificacoesByUser/" + this.idUsuarioLogado).subscribe(
      (response: NotificacaoResponse) =>{        
        if(response.sucesso){

          response.data.itens.forEach(element => {
            
            if(element.lido == ELido.Nao){
              this.notificaoSemLer = true;
              this.quantidadeSemLer++;
            }

            this.listNotificacao.push(element);
          });
        }else{
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
      }
    ); 

  }

  toggleClass = () =>{
    document.getElementById('sidebar')?.classList.toggle('active');
    document.getElementById('content')?.classList.toggle('active');;
  };

  Deslogar = () =>{
    window.localStorage.clear();
    this.router.navigateByUrl('/');
  };

  NotificacaoLida = (notificacao: DataNotificacao) =>{
    if(notificacao.lido == ELido.Nao){
      
      this.listNotificacao.forEach(element => {
        if(element == notificacao){
          element.lido = ELido.Sim;
        }
      });

      let request: NotificacaoRequest = {
        IdNotificaoLida: notificacao.idNotificacao
      }; 

      this.response.Post("Notificacao","NotificacaoLida", request).subscribe(); 
    }
  };

  EditarPerfil = () => {
    let id = window.localStorage.getItem('IdUsuario');
    
    this.router.navigateByUrl('/main/usuario/' + id + '/editar');
  };

  streets: string[] = ['Champs-Élysées', 'Lombard Street', 'Abbey Road', 'Fifth Avenue'];
  filteredStreets!: Observable<string[]>;

  ngOnInit() {
    this.filteredStreets = this.control.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }

  private _filter(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.streets.filter(street => this._normalizeValue(street).includes(filterValue));
  }

  private _normalizeValue(value: string): string {
    return value.toLowerCase().replace(/\s/g, '');
  }

}



