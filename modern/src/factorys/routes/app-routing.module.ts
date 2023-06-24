import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { MainComponent } from 'src/app/pages/entities/main/main.component';
import { LoginComponent } from 'src/app/pages/entities/login/login.component';
import { RegisterComponent } from 'src/app/pages/entities/register/register.component';
import { RecoverPasswordComponent } from 'src/app/pages/entities/recoverPassword/recoverPassword.component';

const routes: Routes = [
  { path: '', component: LoginComponent},
  { path: 'recuperar-senha', component: RecoverPasswordComponent},
  { path: 'nova-conta', component: RegisterComponent},
  { path: 'main-dashboard', component: MainComponent, loadChildren: () => import('../../app/pages/pages.module').then(m => m.PagesModule), canActivate: [AuthGuard] },
  { path: 'landing', loadChildren: () => import('../../app/landing/landing.module').then(m => m.LandingModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
