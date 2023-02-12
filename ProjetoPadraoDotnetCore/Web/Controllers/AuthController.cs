using Aplication.Authentication;
using Aplication.Interfaces;
using Aplication.Models.Request.Login;
using Aplication.Models.Request.Senha;
using Aplication.Models.Request.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : DefaultController
{
    protected readonly IAuthApp AuthApp;
    protected readonly IJwtTokenAuthentication Token;
    protected readonly IUsuarioApp UsuarioApp;

    
    public AuthController(IAuthApp authApp,IJwtTokenAuthentication token,IUsuarioApp usuarioApp)
    {
        AuthApp = authApp;
        Token = token;
        UsuarioApp = usuarioApp;
    }

    [HttpPost]
    [Route("Login")]
    public JsonResult Login(LoginRequest request)
    {
        try
        {
            var retorno = AuthApp.Login(request);

            if (!retorno.Autenticado)
                return ResponderErro("Usuário ou senha inválido!");

            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Route("GerarToken")]
    public JsonResult GerarToken(TokenRequest request)
    {
        try
        {
            var usuario = UsuarioApp.GetByCpf(request.Cpf);
            
            if(usuario == null)
                return ResponderErro("Não foi encontrado usuário com este CPF!");

            var retorno = Token.GerarToken(request.Cpf);

            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Route("RecuperarSenha")]
    public JsonResult RecuperarSenha(RecuperarSenhaRequest request)
    {
        try
        {
            var usuario = UsuarioApp.GetByCpfEmail(request.CpfRecover,request.EmailRecover);
            
            if(usuario == null)
                return ResponderErro("Não foi encontrado usuário vinculado a este CPF e email!");

            var retorno = AuthApp.RecuperarSenha(usuario);
            
            if(!retorno.IsValid())
                return ResponderErro(retorno.LErrors.FirstOrDefault());

            return ResponderSucesso("Insira o código informado no email",usuario.IdUsuario);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("AlterarSenha")]
    public JsonResult AlterarSenha(UsuarioAlterarSenhaRequest request)
    {
        try
        {
            var retorno = UsuarioApp.AlterarSenha(request);
            
            if(!retorno.IsValid())
                return ResponderErro(retorno.LErrors.FirstOrDefault());

            return ResponderSucesso("Senha Alterada com sucesso");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Route("ValidarCodigo")]
    public JsonResult ValidarCodigo(ValidarCodigoRequest request)
    {
        try
        {
            var usuario = UsuarioApp.GetById(request.IdUsuario);
            var retorno = UsuarioApp.ValidarCodigo(usuario,request);
            
            if(!retorno.IsValid())
                return ResponderErro(retorno.LErrors.FirstOrDefault());

            var login = new LoginRequest()
            {
                EmailLogin = usuario!.Email,
                SenhaLogin = usuario.Senha
            };
                
            var sessao = AuthApp.Login(login,true);

            return ResponderSucesso("Codigo validado com sucesso!",sessao);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}