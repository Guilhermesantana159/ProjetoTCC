using Aplication.Interfaces;
using Aplication.Models.Request.Template;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TemplateController : DefaultController
{
    protected readonly ITemplateApp TemplateApp;

    public TemplateController(ITemplateApp templateApp)
    {
        TemplateApp = templateApp;
    }

    [HttpPost]
    [Authorize]
    [Route("IntegrarTemplate")]
    public JsonResult IntegrarTemplate(TemplateRequest request)
    {
        try
        {
            var retorno = TemplateApp.IntegrarTemplate(request);

            if (retorno.IsValid())
                return ResponderSucesso("Template adicionado com sucesso!");
            
            return ResponderErro(retorno.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("ConsultarGridTemplate")]
    public JsonResult ConsultarGridTemplate(TemplateRequestGridRequest request)
    {
        try
        {
            var retorno = TemplateApp.ConsultarGridTemplate(request);
            
            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("DeletarTemplate/{id}")]
    public JsonResult DeletarTemplate(int id)
    {
        try
        {
            TemplateApp.DeletarTemplate(id);
            return ResponderSucesso("Template deletado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarCategorias")]
    public JsonResult ConsultarCategorias()
    {
        try
        {
            return ResponderSucesso(TemplateApp.ConsultarCategorias());
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("IntegrarCategoria")]
    public JsonResult IntegrarCategoria(CategoriaRequest categoria)
    {
        try
        {
            var retorno = TemplateApp.IntegrarCategoria(categoria);

            if (!retorno.IsValid())
                return ResponderErro(retorno.LErrors.FirstOrDefault());

            return ResponderSucesso("Categoria cadastrado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    [HttpPost]
    [Authorize]
    [Route("DeletarCategoria/{id}")]
    public JsonResult DeletarCategoria(int id)
    {
        try
        {
            TemplateApp.DeletarCategoria(id);
            return ResponderSucesso("Categoria deletada com sucesso!");
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
            return ResponderSucesso(TemplateApp.ConsultarViaId(id));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}