using System.Linq.Dynamic.Core;
using Aplication.Authentication;
using Aplication.Interfaces;
using Aplication.Models.Grid;
using Aplication.Models.Request.Senha;
using Aplication.Models.Request.Usuario;
using Aplication.Models.Response.Auth;
using Aplication.Models.Response.Usuario;
using Aplication.Utils.FilterDynamic;
using Aplication.Utils.HashCripytograph;
using Aplication.Utils.Helpers;
using Aplication.Utils.Objeto;
using Aplication.Validators.Usuario;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Usuario.Objeto;

namespace Aplication.Controllers;
public class UsuarioApp : IUsuarioApp
{
    protected readonly IUsuarioService Service;
    protected readonly INotificacaoService NotificaService;
    protected readonly IMapper Mapper;
    protected readonly IUsuarioValidator Validation;
    protected readonly IJwtTokenAuthentication Jwt;
    private readonly IConfiguration _configuration;
    public UsuarioApp(IUsuarioService service,IMapper mapper,IUsuarioValidator validation, IJwtTokenAuthentication jwt, IConfiguration configuration, INotificacaoService notificaService)
    {
        Service = service;
        Mapper = mapper;
        Validation = validation;
        Jwt = jwt;
        _configuration = configuration;
        NotificaService = notificaService;
    }

    public List<Usuario> GetAll()
    {
        return Service.GetAllList();
    }

    public Usuario? GetByCpf(string cpf)
    {
        return Service.GetByCpf(cpf);
    }
    
    public Usuario? GetByCpfEmail(string cpf,string email)
    {
        return Service.GetAllQuery().FirstOrDefault(x => x.Email == email && x.Cpf == cpf);
    }

    public UsuarioCrudResponse GetById(int id)
    {
        return Mapper.Map<Usuario, UsuarioCrudResponse>(Service.GetByIdWithInclude(id));
    }

    public ValidationResult Cadastrar(UsuarioRequest request)
    {
        var validation = Validation.ValidacaoCadastro(request);
        var lUsuario = Service.GetAllList();

        if (lUsuario.Any(x => x.Email == request.Email))
            validation.LErrors.Add("Email já vinculado a outro usuário");

        if(validation.IsValid())
        {
            var usuario = Mapper.Map<UsuarioRequest,Usuario>(request);
            
            //Hash da senha
            usuario.Senha = new HashCripytograph().Hash(request.Senha);
            var cadastro = Service.CadastrarComRetorno(usuario);
            
            //Enviar Notificação de bem-vindo
            var pushMensagem = new Notificacao()
            {
                IdUsuario = cadastro.IdUsuario,
                DataCadastro = DateTime.Now,
                Lido = ESimNao.Nao,
                ClassficacaoMensagem = EMensagemNotificacao.MensagemBemVindo,
                Corpo = $"Seja bem vindo {usuario.Nome} este é um futuro software de gestão de projeto aproveite as funcionalidades!",
                Titulo = "Seja bem vindo",
                DataVisualização = null,
            };
            
            NotificaService.Cadastrar(pushMensagem);
        }

        return validation;
    }

    public UsuarioCadastroInicialResponse CadastroInicial(UsuarioRegistroInicialRequest request)
    {
        var validation = Validation.ValidacaoCadastroInicial(request);

        var response = new UsuarioCadastroInicialResponse()
        {
            Validacao = validation,
        };
        
        var lUsuario = Service.GetAllQuery();

        if (lUsuario.Any(x => x.Email == request.Email))
            validation.LErrors.Add("Email já vinculado a outro usuário");
        
        if(validation.IsValid())
        {
            var usuario = Mapper.Map<UsuarioRegistroInicialRequest,Usuario>(request);
            var responseCadastro = Service.CadastrarComRetorno(usuario);
            
            //Enviar Notificação de bem-vindo
            var pushMensagem = new Notificacao()
            {
                IdUsuario = responseCadastro.IdUsuario,
                DataCadastro = DateTime.Now,
                Lido = ESimNao.Nao,
                ClassficacaoMensagem = EMensagemNotificacao.MensagemBemVindo,
                Corpo = $"Seja bem vindo, este é um futuro software de gestão de projeto aproveite as funcionalidades!",
                Titulo = "Seja bem vindo",
                DataVisualização = null,
            };
            
            NotificaService.Cadastrar(pushMensagem);

            response.DataUsuario = new LoginResponse()
            {
                IdUsuario = responseCadastro.IdUsuario,
                SessionKey = Jwt.GerarToken(responseCadastro.Cpf),
                Nome = usuario.Nome,
                Autenticado = true,
                Foto = usuario.Foto == null 
                    ? usuario.Genero == EGenero.Masculino 
                        ? _configuration.GetSection("ImageDefaultUser:Masculino").Value 
                        : _configuration.GetSection("ImageDefaultUser:Feminino").Value     
                    : usuario.Foto,
                Perfil = usuario.PerfilAdministrador
            };
        }

        return response;
    }

    public void CadastrarListaUsuario(List<Usuario> lUsuario)
    {
        Service.CadastrarListaUsuario(lUsuario);
    }
    
    public ValidationResult Editar(UsuarioRequest request)
    {
        var validation = Validation.ValidacaoCadastro(request);
        var lUsuario = Service.GetAllQuery();
        var usuarioOld = Service.GetById(request.IdUsuario ?? 0);

        if (lUsuario.Any(x => x.Email == request.Email && x.IdUsuario != request.IdUsuario))
            validation.LErrors.Add("Email já vinculado a outro usuário");

        if(validation.IsValid())
        {
            var usuario = Mapper.Map<UsuarioRequest,Usuario>(request);

            if (string.IsNullOrEmpty(request.Senha) && usuarioOld != null)
                usuario.Senha = usuarioOld.Senha;

            Service.Editar(usuario);
        }

        return validation;
    }
    
    public void EditarListaUsuario(List<Usuario> lUsuario)
    {
        Service.EditarListaUsuario(lUsuario);
    }
    
    public void DeleteById(int id)
    {
        Service.DeleteById(id);
    }
    
    public List<UsuarioGridReportObj> ConsultarRelatorioUsuario(UsuarioRelatorioRequest request)
    {
        var itens = Service.GetAllQuery();
        
        itens = string.IsNullOrEmpty(request.OrderFilters?.Campo)
            ? itens.OrderByDescending(x => x.IdUsuario)
            : itens.OrderBy($"{request.OrderFilters.Campo} {request.OrderFilters.Operador.ToString()}");

        itens = itens.AplicarFiltrosDinamicos(request.QueryFilters);

        return itens.ProjectTo<UsuarioGridReportObj>(Mapper.ConfigurationProvider).ToList();
    }
    
    public BaseGridResponse ConsultarGridUsuario(BaseGridRequest request)
    {
        var itens = Service.GetAllQuery();
        
        itens = string.IsNullOrEmpty(request.OrderFilters?.Campo)
            ? itens.OrderByDescending(x => x.IdUsuario)
            : itens.OrderBy($"{request.OrderFilters.Campo} {request.OrderFilters.Operador.ToString()}");

        itens = itens.AplicarFiltrosDinamicos(request.QueryFilters);
        
        return new BaseGridResponse()
        {
            Itens = itens.Skip(request.Page * request.Take).Take(request.Take)
                .Select(x => new UsuarioGridResponse()
                {
                    IdUsuario = x.IdUsuario,
                    Nome = x.Nome,
                    Cpf = x.Cpf.ToFormatCpf(),
                    DataNascimento = x.DataNascimento == null ? null : x.DataNascimento!.Value.FormatDateBr(),
                    Email = x.Email,
                    Senha = x.Senha,
                    Telefone = x.Telefone,
                    Perfil = x.PerfilAdministrador ? "Administrador" : "Comum",
                    ImagemUsuario = x.Foto == null ? x.Genero == EGenero.Masculino 
                            ? _configuration.GetSection("ImageDefaultUser:Masculino").Value 
                            : _configuration.GetSection("ImageDefaultUser:Feminino").Value     
                        : x.Foto
                }).ToList(),
            
            TotalItens = itens.Count()
        };
    }

    public ValidationResult AlterarSenha(UsuarioAlterarSenhaRequest request)
    {
        var retorno = new ValidationResult();

        var usuario = Service.GetById(request.IdUsuario ?? 0);

        if (usuario == null)
            retorno.LErrors.Add("Usuário não encontrado na base!");

        if (retorno.IsValid() && usuario != null)
        {
            usuario.Senha = new HashCripytograph().Hash(request.Senha);
            usuario.TentativasRecuperarSenha = 0;
            Service.Editar(usuario);
        }

        return retorno;
    }
    public ValidationResult ValidarCodigo(Usuario? usuario,ValidarCodigoRequest request)
    {
        var retorno = new ValidationResult();
        
        if (usuario == null)
            retorno.LErrors.Add("Usuário não encontrado na base!");
        
        if(usuario != null && usuario.TentativasRecuperarSenha >= 3)
            retorno.LErrors.Add("Número máximo de tentativas alcançado!");

        if (retorno.IsValid() && usuario != null)
        {
            if (request.Codigo != usuario.CodigoRecuperarSenha.ToString())
            {
                usuario.TentativasRecuperarSenha += 1;
                retorno.LErrors.Add($"Código inválido! Tentativas restantes: {3 - usuario.TentativasRecuperarSenha}");

                //Atualizar numero tentativas
                Service.Editar(usuario);
            }
        }

        return retorno;
    }

    public Usuario? GetById(int? id)
    {
        return Service.GetById(id ?? 0);
    }
    
    public IQueryable<Usuario>? GetUsuarioTarefa()
    {
        return Service.GetUsuarioTarefa();
    }
}

