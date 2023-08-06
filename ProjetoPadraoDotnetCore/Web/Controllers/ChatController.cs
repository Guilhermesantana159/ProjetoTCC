using Aplication.Interfaces;
using Aplication.Models.Request.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : DefaultController
{
    protected readonly IChatApp ChatApp;

    public ChatController(IChatApp chatApp)
    {
        ChatApp = chatApp;
    }

    [HttpPost]
    [Authorize]
    [Route("CadastrarContato")]
    public JsonResult CadastrarContato(ContatoRequest request)
    {
        try
        {
            return ResponderSucesso("Contato adicionado com sucesso!",ChatApp.Cadastrar(request));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("AlterarStatusContato")]
    public JsonResult AlterarStatusContato(AlterarStatusContatoRequest request)
    {
        try
        {
            ChatApp.AlterarStatusContato(request);
            return ResponderSucesso($"Contato {request.NewStatus} com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("DeletarContato/{id}")]
    public JsonResult DeletarContato(int id)
    {
        try
        {
            ChatApp.DeletarContato(id);
            return ResponderSucesso("Contato deletado com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarContatoPorIdPessoa/{id}")]
    public JsonResult ConsultarContatoPorIdPessoa(int id)
    {
        try
        {
            return ResponderSucesso(ChatApp.ConsultarContatoPorIdPessoa(id));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("SalvarMensagem")]
    public JsonResult SalvarMensagem(MensagemChatRequest request)
    {
        try
        {
            return ResponderSucesso(ChatApp.SalvarMensagem(request));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarMensagens/{idUsuarioMandante}/{idUsuarioRecebe}")]
    public JsonResult ConsultarMensagens(int idUsuarioMandante,int idUsuarioRecebe)
    {
        try
        {
            return ResponderSucesso(ChatApp.ConsultarMensagens(idUsuarioMandante,idUsuarioRecebe));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("DeletarMensagem/{idMensagem}")]
    public JsonResult DeletarMensagem(int idMensagem)
    {
        try
        {
            ChatApp.DeletarMensagem(idMensagem);
            return ResponderSucesso("Mensagem deletada com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarMensagensDireta/{id}")]
    public JsonResult ConsultarMensagensDireta(int id)
    {
        try
        {
            return ResponderSucesso(ChatApp.ConsultarMensagensDireta(id));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("ExcluirConversa")]
    public JsonResult ExcluirConversa(ExcluirConversaRequest request)
    {
        try
        {
            ChatApp.ExcluirConversa(request);
            return ResponderSucesso("Mensagens excluída com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}