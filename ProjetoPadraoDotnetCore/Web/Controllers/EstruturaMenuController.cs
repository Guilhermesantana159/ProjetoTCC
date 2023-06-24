using Aplication.Interfaces;
using Aplication.Models.Request.ModuloMenu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class EstruturaMenuController : DefaultController
{
    protected readonly IEstruturaMenuApp App;
    
    public EstruturaMenuController(IEstruturaMenuApp estruturaMenuApp)
    {
        App = estruturaMenuApp;
    }

    [HttpPost]
    [Authorize]
    [Route("IntegrarSubModulo")]
    public JsonResult IntegrarSubModulo(SubModuloRequest request)
    {
        try
        {
            var integracao = App.IntegrarSubModulo(request);
            
            if (!integracao.IsValid())
                return ResponderErro(integracao.LErrors.FirstOrDefault());
                
            return ResponderSucesso("SubModulo cadastrado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("IntegrarModulo")]
    public JsonResult IntegrarModulo(ModuloRequest request)
    {
        try
        {
            var integracao = App.IntegrarModulo(request);
            
            if (!integracao.IsValid())
                return ResponderErro(integracao.LErrors.FirstOrDefault());
                
            return ResponderSucesso("Modulo cadastrado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("IntegrarMenu")]
    public JsonResult IntegrarMenu(MenuRequest request)
    {
        try
        {
            var integracao = App.IntegrarMenu(request);
            
            if (!integracao.IsValid())
                return ResponderErro(integracao.LErrors.FirstOrDefault());
                
            return ResponderSucesso("Menu cadastrado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Route("ConsultarEstruturaMenus/{idUsuario}")]
    public JsonResult ConsultarEstruturaMenus(int idUsuario)
    {
        try
        {
            var integracao = App.ConsultarEstruturaMenus(idUsuario);

            return ResponderSucesso(integracao);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarMenus")]
    public JsonResult ConsultarMenus()
    {
        try
        {
            return ResponderSucesso(App.ConsultarMenus());
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarModulo")]
    public JsonResult ConsultarModulo()
    {
        try
        {
            return ResponderSucesso(App.ConsultarModulo());
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarSubModulo")]
    public JsonResult ConsultarSubModulo()
    {
        try
        {
            return ResponderSucesso(App.ConsultarSubModulo());
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarAutoCompleteMenu/{idUsuario}")]
    public JsonResult ConsultarAutoCompleteMenu(int idUsuario)
    {
        try
        {
            var integracao = App.ConsultarAutoCompleteMenu(idUsuario);

            return ResponderSucesso(integracao);        
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}