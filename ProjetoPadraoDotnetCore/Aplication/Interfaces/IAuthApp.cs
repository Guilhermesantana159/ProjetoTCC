using Aplication.Models.Request.Login;
using Aplication.Models.Response.Auth;
using Aplication.Utils.Objeto;
using Infraestrutura.Entity;

namespace Aplication.Interfaces;

public interface IAuthApp
{
    public LoginResponse Login(LoginRequest request, bool isRecuperacaoSenha = false);
    public ValidationResult RecuperarSenha(Usuario request);
}