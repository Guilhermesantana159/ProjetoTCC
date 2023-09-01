using Aplication.Interfaces;
using Aplication.Models.Request.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UtilsController : DefaultController
{
    protected readonly IUtilsApp UtilsApp;

    public UtilsController(IUtilsApp utilApp)
    {
        UtilsApp = utilApp;
    }

    [HttpGet]
    [Authorize]
    [Route("ConsultarEnderecoCep/{cep}")]
    public JsonResult ConsultarEnderecoCep(string cep)
    {
        try
        {
            var retorno = UtilsApp.ConsultarEnderecoCep(cep);

            if (!retorno.IsValid())
                return ResponderErro("Cep inválido!");

            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("CadastrarFeedback")]
    public JsonResult CadastrarFeedback(FeedbackRequest request)
    {
        try
        {
            var retorno = UtilsApp.CadastrarFeedback(request);

            return ResponderSucesso("Obrigado pela sua avaliação!",retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Route("CadastrarContatoMensagem")]
    public JsonResult CadastrarContatoMensagem(ContatoMensagemRequest request)
    {
        try
        {
            UtilsApp.ContatoMensagem(request);
            return ResponderSucesso("Agradecemos seu contato, qualquer coisa entraremos em contato com o email informado!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarContatoMensagem")]
    public JsonResult ConsultarContatoMensagem()
    {
        try
        {
            return ResponderSucesso(new
            {
                Data = UtilsApp.ConsultarContatoMensagem()
            });
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}