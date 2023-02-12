import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BaseService } from 'src/factorys/base.service';
import { Usuario } from 'src/objects/Usuario/Usuario';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ValidateSenha } from '../../../factorys/validators/validators-form';

@Component({
  selector: 'login-root',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginFormGroup: FormGroup;
  UserRegisterFormGroup: FormGroup;
  loading: boolean;
  submitLogin: boolean;
  submitRegister: boolean;

  constructor(private formBuilder: FormBuilder,private response: BaseService,private toastr: ToastrService,
    private router: Router) {
    this.loading = false;
    this.submitLogin = false;
    this.submitRegister = false;

    this.loginFormGroup = this.formBuilder.group({
        emailLogin: ['', Validators.required],
        senhaLogin: ['', Validators.required]
    });

    this.UserRegisterFormGroup = this.formBuilder.group({
      email: ['', [Validators.required,Validators.email]],
      nome: ['', Validators.required],
      CPF: ['', [Validators.required,Validators.minLength(11)]],
      genero: ['0'],
      senha: ['', [Validators.required,ValidateSenha]]
    });
  }

  RegisterUsuario = (form:FormGroup) =>{
    if(this.UserRegisterFormGroup.invalid){
      this.submitRegister = true;
      return;
    }

    //Formatação tipo da variável enum back-end
    form.get('genero')?.setValue(parseInt(form.get('genero')?.value));

    this.loading = true;
    this.response.Post("Usuario","CadastroInicial",form.value).subscribe(
      (response: Usuario) =>{        
        if(response.sucesso){
          window.localStorage.setItem('NomeUsuario',response.data.nome);
          window.localStorage.setItem('IdUsuario',response.data.idUsuario);
          window.localStorage.setItem('Token',response.data.sessionKey.acess_token);
          window.localStorage.setItem('Foto', response.data.foto);
          window.localStorage.setItem('Perfil', response.data.perfil.toString());
          this.toastr.success('<small> Seja bem vindo <br>' + response.data.nome + '</small>', 'Mensagem:');   
          this.router.navigate(['/', 'main'])        
        }else
        {
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem');
        }
        this.loading = false;
      }
    );

    //Re atribuição caso error
    form.get('genero')?.setValue((form.get('genero')?.value.toString()));
  }
    
  Login = (form:FormGroup) =>{
    if(this.loginFormGroup.invalid){
      this.submitLogin = true;
      return;
    }

    this.loading = true;
    this.response.Post("Auth","Login",form.value).subscribe(
      (response: Usuario) =>{        
        if(response.sucesso){
          window.localStorage.setItem('NomeUsuario',response.data.nome);
          window.localStorage.setItem('IdUsuario',response.data.idUsuario);
          window.localStorage.setItem('Token',response.data.sessionKey.acess_token);
          window.localStorage.setItem('Foto', response.data.foto);
          window.localStorage.setItem('Perfil', response.data.perfil.toString());
          this.toastr.success('<small>' + 'Seja bem vindo de volta: <br>' + response.data.nome + '</small>', 'Mensagem:');   
          this.router.navigate(['/', 'main'])
        }
        else
        {
          this.toastr.error('<small>' + response.mensagem + '</small>', 'Mensagem:');
        }
        this.loading = false;
      }
    );
  }


  //Campo Senha
  ActiveEyePasswordLogin: boolean = false;
  ActiveEyePasswordRegister: boolean = false;

  EyePasswordRegister = (): void => {
    this.ActiveEyePasswordRegister = !this.ActiveEyePasswordRegister;
  };

  EyePasswordLogin = (): void => {
    this.ActiveEyePasswordLogin = !this.ActiveEyePasswordLogin;
  };
}
