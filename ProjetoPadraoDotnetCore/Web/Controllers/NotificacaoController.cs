using Aplication.Interfaces;
using Aplication.Models.Request.Notificao;
using Aplication.Models.Response.Notificacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificacaoController : DefaultController
{
    protected readonly INotificacaoApp App;
    public NotificacaoController(INotificacaoApp notificacaoApp)
    {
        App = notificacaoApp;
    }

    [HttpGet]
    [Authorize]
    [Route("GetNotificacoesByUser/{id}")]
    public JsonResult GetNotificacoesByUser(int id)
    {
        try
        {
            var retorno = new NotificacaoResponse()
            {
                Itens = App.GetNotificaçõesByUser(id)
            };
            
            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("NotificacaoLida")]
    public JsonResult NotificacaoLida(NotificacaolidaRequest request)
    {
        try
        {
            App.NotificacaoLida(request);
            return ResponderSucesso("Notificação lida com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Route("NotificarProjetosAtrasados")]
    public JsonResult NotificarProjetosAtrasados()
    {
        try
        {
            App.NotificarProjetosAtrasados();
            return ResponderSucesso("Notificação concluída com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Route("NotificarAtividadesAtrasadas")]
    public JsonResult NotificarAtividadesAtrasadas()
    {
        try
        {
            App.NotificarAtividadesAtrasadas();
            return ResponderSucesso("Notificação concluída com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}