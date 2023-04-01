using Aplication.Interfaces;
using Aplication.Models.Request.Projeto;
using Infraestrutura.Enum;
using Infraestrutura.Reports.Projeto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjetoController : DefaultController
{
    private readonly IProjetoApp _app;
    private readonly IProjetoGridBuildReport _reportGrid;
    public ProjetoController(IProjetoApp app, IProjetoGridBuildReport reportGrid)
    {
        _app = app;
        _reportGrid = reportGrid;
    }
    
    [HttpPost]
    [Authorize]
    [Route("Cadastrar")]
    public JsonResult Cadastrar(ProjetoRequest request)
    {
        try
        {
            var cadastro = _app.Cadastrar(request);
            
            if(cadastro.IsValid())
                return ResponderSucesso("Projeto cadastrado com sucesso!");
            
            return ResponderErro(cadastro.LErrors.FirstOrDefault());

        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("ConsultarGridProjeto")]
    public JsonResult ConsultarGridProjeto(ProjetoGridRequest request)
    {
        try
        {
            var retorno = _app.ConsultarGridProjeto(request);
            
            return ResponderSucesso(retorno);
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("MudarStatusProjeto")]
    public JsonResult MudarStatusProjeto(ProjetoStatusRequest request)
    {
        try
        {
            var mudanca = _app.MudarStatusProjeto(request);
            
            if(!mudanca.IsValid())
                return ResponderErro(mudanca.LErrors.FirstOrDefault());

            return ResponderSucesso($"Status modificado para {request.Status.ToString()}!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("Deletar")]
    public JsonResult Deletar(ProjetoDeleteRequest request)
    {
        try
        {
            var mudanca = _app.Deletar(request);
            
            if(!mudanca.IsValid())
                return ResponderErro(mudanca.LErrors.FirstOrDefault());

            return ResponderSucesso("Projeto excluído com sucesso!");
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("GerarRelatorioGridProjeto")]
    public FileStreamResult GerarRelatorioGridProjeto(ProjetoRelatorioGridRequest request)
    {
        var retorno = _app.ConsultarRelatorioGridProjeto(request);
        var result = _reportGrid.GerarRelatorioGridProjeto(request.Tipo,retorno);
        
        if(request.Tipo == ETipoArquivo.Excel)
            return File(result,"application/excel" , "RelatorioProjeto.xls");
        if(request.Tipo == ETipoArquivo.Word)
            return File(result,"application/word" , "RelatorioProjeto.docx");

        return File(result,"application/pdf" , "RelatorioProjeto.pdf");

    }
    
    [HttpGet]
    [Authorize]
    [Route("ConsultarViaId/{id}")]
    public JsonResult ConsultarViaId(int id)
    {
        try
        {
            return ResponderSucesso(_app.GetById(id));
        }
        catch (Exception e)
        {
            return ResponderErro(e.Message);
        }
    }
}