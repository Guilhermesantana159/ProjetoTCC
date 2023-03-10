import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsuarioCrudComponent } from 'src/app/entities/usuario/usuario-crud/usuario-crud.component';
import { UsuarioComponent } from 'src/app/entities/usuario/usuario-grid/usuario-grid.component';
import { LoginComponent } from '../../app/entities/login/login.component';
import { MainComponent } from '../../app/entities/main/main.component';
import { RecoverPasswordComponent } from '../../app/entities/recoverPassword/recoverPassword.component';

const routes: Routes = [
  {path: 'main',component: MainComponent, 
    children: [ 
      { 
        path: 'usuario', 
        component: UsuarioComponent,
      },
      { 
        path: 'usuario/:id/editar', 
        component: UsuarioCrudComponent 
      },
      { 
        path: 'usuario/registro/novo', 
        component: UsuarioCrudComponent 
      }
    ]
  },
  {path: 'login',component: LoginComponent},
  {path: 'recuperar-senha',component: RecoverPasswordComponent},
  {path: '**',component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
