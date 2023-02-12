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
    [Authorize]
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
}