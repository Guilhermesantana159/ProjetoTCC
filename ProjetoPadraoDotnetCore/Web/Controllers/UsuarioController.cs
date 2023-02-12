using Microsoft.AspNetCore.Mvc;
using Aplication.Interfaces;
using Aplication.Models.Request.Usuario;
using Aplication.Models.Response.Usuario;
using Infraestrutura.Entity;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Usuario;
using Microsoft.AspNetCore.Authorization;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : DefaultController
{
    protected readonly IUsuarioApp App;
    protected readonly IUsuarioGridBuildReport ReportGrid;
    public UsuarioController(IUsuarioApp usuarioApp, IUsuarioGridBuildReport reportGrid)
    {
        App = usuarioApp;
        ReportGrid = reportGrid;
    }

    [HttpPost]
    [Authorize]
    [Route("Cadastrar")]
    public JsonResult Cadastrar(UsuarioRequest request)
    {
        try
        {
            var cadastro = App.Cadastrar(request);
            
            if(cadastro.IsValid())
                return ResponderSucesso("Usuário cadastrado com sucesso!");
            
            return ResponderErro(cadastro.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Route("CadastroInicial")]
    public JsonResult CadastroInicial(UsuarioRegistroInicialRequest request)
    {
        try
        {
            var cadastro = App.CadastroInicial(request);

            if (!cadastro.Validacao.IsValid())
                return ResponderErro(cadastro.Validacao.LErrors.FirstOrDefault());
                
            return ResponderSucesso(cadastro.DataUsuario);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("ConsultarTodos")]
    public JsonResult ConsultarTodos()
    {
        try
        {
            var objeto = new UsuarioResponse()
            {
                itens = App.GetAll()
            };
            
            return ResponderSucesso(objeto);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("ConsultarViaId/{id}")]
    public JsonResult ConsultarViaId(int id)
    {
        try
        {
            return ResponderSucesso(App.GetById(id));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("CadastrarListaUsuario")]
    public JsonResult CadastrarListaUsuario(List<Usuario> lUsuario)
    {
        try
        {
            App.CadastrarListaUsuario(lUsuario);
            return ResponderSucesso("usuários cadastrado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("Editar")]
    public JsonResult Editar(UsuarioRequest request)
    {
        try
        {
            var edicao = App.Editar(request);
            
            if(edicao.IsValid())
                return ResponderSucesso("Usuário editado com sucesso!");
            
            return ResponderErro(edicao.LErrors.FirstOrDefault());
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("EditarListaUsuario")]
    public JsonResult EditarListaUsuario(List<Usuario> lUsuario)
    {
        try
        {
            App.EditarListaUsuario(lUsuario);
            return ResponderSucesso("Usuário ");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("DeleteById")]
    public JsonResult DeleteById(int id)
    {
        try
        {
            App.DeleteById(id);
            return ResponderSucesso("Usuário deletado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("ConsultarGridUsuario")]
    public JsonResult ConsultarGridUsuario(UsuarioGridRequest request)
    {
        try
        {
            var retorno = App.ConsultarGridUsuario(request);
            
            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("GerarRelatorioGridUsuario")]
    public FileStreamResult GerarRelatorioGridUsuario(UsuarioRelatorioRequest request)
    {
       var retorno = App.ConsultarRelatorioUsuario(request);
        var result = ReportGrid.GerarRelatorioGridUsuario(request.Tipo,retorno);
        
        if(request.Tipo == ETipoArquivo.Excel)
            return File(result,"application/excel" , "RelatorioUsuarios.xls");
        if(request.Tipo == ETipoArquivo.Word)
            return File(result,"application/word" , "RelatorioUsuarios.docx");

        return File(result,"application/pdf" , "RelatorioUsuarios.pdf");

    }
}