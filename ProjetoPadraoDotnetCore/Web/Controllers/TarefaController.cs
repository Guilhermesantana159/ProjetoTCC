using Aplication.Interfaces;
using Aplication.Models.Request.Tarefa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : DefaultController
{
    private readonly ITarefaApp _app;
    public TarefaController(ITarefaApp app)
    {
        _app = app;
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("Integrar")]
    public JsonResult Integrar(TarefaAdmRequest request)
    {
        try
        {
            var cadastro = _app.Integrar(request);

            if (cadastro.IsValid())
            {
                if(!request.IdTarefa.HasValue)
                    return ResponderSucesso("Tarefa cadastrada com sucesso!");

                return ResponderSucesso("Tarefa editada com sucesso!");
            }
            
            return ResponderErro(cadastro.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarTarefasPorIdProjeto/{idProjeto}")]
    public JsonResult ConsultarTarefasPorIdProjeto(int idProjeto)
    {
        try
        {
            return ResponderSucesso(_app.ConsultarTarefasPorIdProjeto(idProjeto));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarTarefasRegistro/{idProjeto}/{idUsuario}")]
    public JsonResult ConsultarTarefasRegistro(int idProjeto,int idUsuario)
    {
        try
        {
            return ResponderSucesso(_app.ConsultarTarefasRegistro(idProjeto,idUsuario));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("Deletar/{idTarefa}")]
    public JsonResult Deletar(int idTarefa)
    {
        try
        {
            var delete = _app.DeletarTarefaById(idTarefa);

            if (delete.IsValid())
                return ResponderSucesso("Tarefa deletada com sucesso!");
            
            return ResponderErro(delete.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("IntegrarComentarioTarefa")]
    public JsonResult IntegrarComentarioTarefa(ComentarioTarefaRequest request)
    {
        try
        {
            var retorno = _app.IntegrarComentarioTarefa(request);

            if (retorno.IsValid())
                return ResponderSucesso($"Tarefa {(request.IdComentarioTarefa.HasValue ? "editada" : "cadastrada")} com sucesso!");
            
            return ResponderErro(retorno.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("DeletarComentario/{idComentario}")]
    public JsonResult DeletarComentario(int? idComentario)
    {
        try
        {
            var delete = _app.DeletarComentarioById(idComentario);

            if (delete.IsValid())
                return ResponderSucesso("Comentário deletado com sucesso!");
            
            return ResponderErro(delete.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarDetalhesTarefa/{idTarefa}")]
    public JsonResult ConsultarDetalhesTarefa(int idTarefa)
    {
        try
        {
            return ResponderSucesso( _app.ConsultarDetalhesTarefa(idTarefa));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("IntegrarMovimentacao")]
    public JsonResult IntegrarMovimentacao(MovimentacaoTarefaRequest request)
    {
        try
        {
            var delete = _app.IntegrarMovimentacao(request);

            if (delete.IsValid())
                return ResponderSucesso("Movimentação feita com sucesso!");
            
            return ResponderErro(delete.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarMovimentacao/{idAtividade}")]
    public JsonResult ConsultarMovimentacao(int idAtividade)
    {
        try
        {
            return ResponderSucesso( _app.ConsultarMovimentacao(idAtividade));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}