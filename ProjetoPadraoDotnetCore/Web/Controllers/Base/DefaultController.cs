using Aplication.Models.Response.Base;
using Infraestrutura.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Base;

public class DefaultController : ControllerBase
{
    protected JsonResult ResponderSucesso(string mensagem,object objeto)
    {
        var retorno = new Retorno()
        {
            Mensagem = mensagem,
            Data = objeto,
            Status = StatusRetorno.Sucesso,
            Sucesso = true
        };
        
        return new JsonResult(retorno);
    }
    protected JsonResult ResponderSucesso(string mensagem)
    {
        var retorno = new Retorno()
        {
            Mensagem = mensagem,
            Status = StatusRetorno.Sucesso,
            Sucesso = true
        };
        
        return new JsonResult(retorno);
    }
    
    protected JsonResult ResponderSucesso(object objeto)
    {
        var retorno = new Retorno()
        {
            Status = StatusRetorno.Sucesso,
            Sucesso = true,
            Data = objeto
        };
        
        return new JsonResult(retorno);
    }
    
    protected JsonResult ResponderErro(string mensagem,object objeto)
    {
        var retorno = new Retorno()
        {
            Mensagem = mensagem,
            Data = objeto,
            Status = StatusRetorno.Erro,
            Sucesso = false
        };
        
        return new JsonResult(retorno);
    }
    
    protected JsonResult ResponderErro(string? mensagem)
    {
        var retorno = new Retorno()
        {
            Mensagem = mensagem,
            Status = StatusRetorno.Erro,
            Sucesso = false
        };
        
        return new JsonResult(retorno);
    }
    
    protected JsonResult ResponderErro(object objeto)
    {
        var retorno = new Retorno()
        {
            Status = StatusRetorno.Erro,
            Sucesso = false,
            Data = objeto
        };
        
        return new JsonResult(retorno);
    }
}