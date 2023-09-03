using Aplication.Authentication;
using Aplication.Interfaces;
using Aplication.Models.Request.Login;
using Aplication.Models.Response.Auth;
using Aplication.Utils.Email;
using Aplication.Utils.HashCripytograph;
using Aplication.Utils.Objeto;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;

public class AuthApp : IAuthApp
{
    protected readonly IEmailHelper EmailHelper;
    protected readonly IUsuarioService UsuarioService;
    protected readonly IHashCriptograph Crypto;
    protected readonly IJwtTokenAuthentication Jwt;
    private readonly IConfiguration _configuration;
    public AuthApp(IUsuarioService usuarioService,IHashCriptograph crypto,IJwtTokenAuthentication jwt, IConfiguration configuration, IEmailHelper emailHelper)
    {
        UsuarioService = usuarioService;
        Crypto = crypto;
        Jwt = jwt;
        _configuration = configuration;
        EmailHelper = emailHelper;
    }

    public LoginResponse Login(LoginRequest request,bool isRecuperacaoSenha = false)
    {
        var retorno = new LoginResponse();

        Usuario? usuario;
        
        if (isRecuperacaoSenha)
        { 
            usuario = UsuarioService.GetAllList()
                .FirstOrDefault(x => x.Email == request.EmailLogin && x.Senha ==
                    request.SenhaLogin);
        }
        else
        {
            usuario = UsuarioService.GetAllList()
                .FirstOrDefault(x => x.Email == request.EmailLogin && x.Senha ==
                    new HashCripytograph().Hash(request.SenhaLogin));
        }
     

        if (usuario == null)
            retorno.Autenticado = false;
        else
        {
            retorno.Autenticado = true;
            retorno.Nome = usuario.Nome;
            retorno.SessionKey = Jwt.GerarToken(usuario.Cpf);
            retorno.IdUsuario = usuario.IdUsuario;
            retorno.Foto = usuario.Foto == null
                ? usuario.Genero == EGenero.Masculino
                    ? _configuration.GetSection("ImageDefaultUser:Masculino").Value
                    : _configuration.GetSection("ImageDefaultUser:Feminino").Value
                : usuario.Foto;
            retorno.Perfil = usuario.PerfilAdministrador;
        }

        return retorno;
    }
    
    public ValidationResult RecuperarSenha(Usuario request)
    {
        var retorno = new ValidationResult();
        var radom = new Random().Next(111111,999999);

        if (request.DataRecuperacaoSenha != null && request.TentativasRecuperarSenha > 3 && request.DataRecuperacaoSenha.Value.AddHours(3) > DateTime.Now)
            throw new Exception("Número máximo de tentivas alcançado, Tente novamente mais tarde!");

        //Dados recuperação de senha
        request.DataRecuperacaoSenha = DateTime.Now;
        request.CodigoRecuperarSenha = radom;
        request.TentativasRecuperarSenha = 0;
            
        UsuarioService.Editar(request);

        var usuario = new List<string>();
        usuario.Add(request.Email);
        
        //Corpo email
        var corpo = new StreamReader(Environment.CurrentDirectory + "/Content/" + "RecupereSuaSenha.html").ReadToEnd();
        
        corpo = corpo.Replace("#codigo#", radom.ToString());
        
        if (string.IsNullOrEmpty(corpo))
            throw new Exception("Arquivo html recupere sua senha não encontrado!");
        
        var email = EmailHelper.EnviarEmail(usuario,"TaskMaster - Recuperação de senha",corpo);
        
        if(!email)
            retorno.LErrors.Add("Não foi possível enviar o código ao email informado!");

        return retorno;
    }
}