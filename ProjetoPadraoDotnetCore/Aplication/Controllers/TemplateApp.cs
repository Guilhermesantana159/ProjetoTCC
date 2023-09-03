using System.Linq.Dynamic.Core;
using Aplication.Interfaces;
using Aplication.Models.Grid;
using Aplication.Models.Request.Template;
using Aplication.Models.Response.Base;
using Aplication.Models.Response.Projeto;
using Aplication.Models.Response.Template;
using Aplication.Utils.FilterDynamic;
using Aplication.Utils.Helpers;
using Aplication.Utils.Objeto;
using Aplication.Validators.Template;
using AutoMapper;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;

public class TemplateApp : ITemplateApp
{
    protected readonly IMapper Mapper;
    protected readonly ITemplateService Service;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioService _usuarioService;
    private readonly ITemplateValidator _validator;

    public TemplateApp(IConfiguration configuration,IMapper mapper,ITemplateService service, ITemplateValidator validator, IUsuarioService usuarioService)
    {
        Mapper = mapper;
        _configuration = configuration;
        Service = service;
        _validator = validator;
        _usuarioService = usuarioService;
    }

    public ValidationResult IntegrarTemplate(TemplateRequest request)
    {
        var validacao = _validator.ValidacaoCadastro(request);

        if (validacao.IsValid())
        {
            var entity = Mapper.Map<TemplateRequest, Template>(request);

            CategoriaTemplate categoria;

            if (request.IdTemplateCategoria is null or 0 && !string.IsNullOrEmpty(request.DescricaoCategoriaNova))
            {
               categoria = Service.CadastrarCategoriaComRetorno(new CategoriaTemplate()
               {
                   DataCadastro = DateTime.Now,
                   Descricao = request.DescricaoCategoriaNova,
                   IdUsuarioCadastro = request.IdUsuarioCadastro,
               });

               entity.IdTemplateCategoria = categoria.IdCategoriaTemplate;
            }

            foreach (var atividade in entity.LAtividadesTemplate)
            {
                foreach (var tarefa in atividade.LTarefaTemplate)
                {
                    tarefa.AtividadeTemplate = atividade;
                }
            }


            if (request.IdTemplate.HasValue)
            {
                var oldEntity = Service.ConsultarPorIdWithIncludes(request.IdTemplate.Value);

                if (oldEntity == null)
                    throw new Exception("Template Id não localizado!");

                entity.DataCadastro = oldEntity.DataCadastro;

                foreach (var item in oldEntity.LAtividadesTemplate)
                {
                    Service.ExcluirTarefasTemplate(item.LTarefaTemplate.ToList());
                }

                Service.ExcluirAtividadeTemplate(oldEntity.LAtividadesTemplate.ToList());
                
                Service.EditarTemplate(entity);
            }
            else
                Service.CadastrarTemplate(entity);
            
        }

        return validacao;
    }
    
     public BaseGridResponse ConsultarGridTemplate(TemplateRequestGridRequest request)
    {
        var itens = Service.GetAllTemplateGrid();
        var usuarioConsulta = _usuarioService.GetById(request.IdUsuario);
        
        itens = string.IsNullOrEmpty(request.OrderFilters?.Campo)
            ? itens.OrderByDescending(x => x.IdTemplate)
            : itens.OrderBy($"{request.OrderFilters.Campo} {request.OrderFilters.Operador.ToString()}");

        itens = itens.AplicarFiltrosDinamicos(request.QueryFilters);
        
        return new BaseGridResponse()
        {
            Itens = itens.Skip(request.Page * request.Take).Take(request.Take)
                .Select(x => new TemplateGridResponse()
                {
                    IdTemplate = x.IdTemplate,
                    TituloTemplate = x.Titulo,
                    Duracao = $"{x.QuantidadeTotal} {(x.Escala == EEscala.Semana ? "Semanas":"Dias")}",
                    Categoria = x.CategoriaTemplate.Descricao,
                    Autor = x.UsuarioCadastro.Nome,
                    IsEdit = usuarioConsulta != null && x.IdUsuarioCadastro == usuarioConsulta.IdUsuario,
                    IsView = usuarioConsulta != null && x.IdUsuarioCadastro != usuarioConsulta.IdUsuario,
                    FotoTemplate = string.IsNullOrEmpty(x.Foto) ? _configuration.GetSection("ImageDefaultTemplate:Imagem").Value 
                        : x.Foto
                }).ToList(),
            
            TotalItens = itens.Count()
        };
    }

     public void DeletarTemplate(int id)
     {
         var template = Service.ConsultarPorIdWithIncludes(id);

         if (template == null)
             throw new Exception("Template não localizado!");
         
         foreach (var atividade in template.LAtividadesTemplate)
         {
             Service.ExcluirTarefasTemplate(atividade.LTarefaTemplate.ToList());
         }

         Service.ExcluirAtividadeTemplate(template.LAtividadesTemplate.ToList());
         Service.ExcluirTemplate(id);
     }

     public List<SelectBase> ConsultarCategorias()
     {
         return Service.GetAllCategoria().Select(x => new SelectBase()
         {
             Description = x.Descricao,
             Value = x.IdCategoriaTemplate ?? 0
         }).ToList();
     }

     public ValidationResult IntegrarCategoria(CategoriaRequest request)
     {
         var retorno = new ValidationResult();
         
         if(string.IsNullOrWhiteSpace(request.Descricao))
             retorno.LErrors.Add("Campo descrição obrigatório!");
         if(!request.IdUsuarioCadastro.HasValue)
             retorno.LErrors.Add("Campo IdUsuarioCadastro obrigatório!");

         if (retorno.IsValid()){{}}
            Service.CadastrarCategoria(Mapper.Map<CategoriaRequest,CategoriaTemplate>(request));
         
         return retorno;
     }

     public void DeletarCategoria(int id)
     {
         Service.DeletarCategoria(id);
     }

     public TemplateResponse ConsultarViaId(int id)
     {
         var retorno = Service.ConsultarPorIdWithIncludes(id);

         if (retorno == null)
             throw new Exception("Template não encontrado!");

         return new TemplateResponse()
         {
             IdTemplate = retorno.IdTemplate,
             Descricao = retorno.Descricao,
             EscalaTempo = retorno.Escala.GetHashCode().ToString(),
             Foto = string.IsNullOrEmpty(retorno.Foto)
                 ? _configuration.GetSection("ImageDefaultTemplate:Imagem").Value
                 : retorno.Foto,
             Titulo = retorno.Titulo,
             DataCadastro = retorno.DataCadastro.FormatDateBr(),
             Quantidade = retorno.QuantidadeTotal,
             IdUsuarioCadastro = retorno.IdUsuarioCadastro,
             Categoria = retorno.IdTemplateCategoria.ToString(),
             LAtividade = retorno.LAtividadesTemplate.Select(x => new AtividadeTemplateResponse()
             {
                 TempoPrevisto = x.TempoPrevisto,
                 Titulo = x.Titulo,
                 Posicao = x.Posicao,
                 LTarefaTemplate = x.LTarefaTemplate.Select(y => new TarefaTemplateResponse()
                 {
                     Descricao = y.Descricao,
                     Prioridade = y.Prioridade.GetHashCode().ToString(),
                     DescricaoTarefa = y.DescricaoTarefa,
                     LTagsTarefa = y.TagTarefaTemplate.Select(z => z.Descricao).ToList()
                 }).ToList()
             }).ToList()
         };
     }

     public ProjetoResponse CarregarTemplate(int id)
     {
         var template = Service.ConsultarPorIdWithIncludes(id);

         if (template == null)
             throw new Exception("Template não encontrado!");

         return new ProjetoResponse()
         {
            IdProjeto = null,
            Titulo = template.Titulo,
            DataInicio = DateTime.Now.Date,
            DataFim = DateTime.Now.Date.AddDays(template.Escala == EEscala.Semana
             ? 7 * template.QuantidadeTotal
             : template.QuantidadeTotal),
            Descricao = template.Descricao,
            ListarAtvProjeto = true,
            EmailProjetoAtrasado = true,
            PortalProjetoAtrasado = true,
            EmailTarefaAtrasada = true,
            PortalTarefaAtrasada = true,
            AlteracaoStatusProjetoNotificar = true,
            AlteracaoTarefasProjetoNotificar = true, 
             ListAtividade = template.LAtividadesTemplate.Select(x => new AtvidadeResponse()
             {
                 IdAtividade = null,
                 Atividade = x.Titulo,
                 DataInicial = DateTime.Now.Date
                     .AddDays(template.Escala == EEscala.Semana
                         ? 7 * ((x.Posicao ?? 1) - 1)
                         : template.QuantidadeTotal * ((x.Posicao ?? 1) - 1)).FormatDateBr(),
                 DataFim = DateTime.Now.Date
                     .AddDays(template.Escala == EEscala.Semana
                         ? 7 * ((x.Posicao ?? 1) - 1)
                         : template.QuantidadeTotal * ((x.Posicao ?? 1) - 1))
                     .AddDays(template.Escala == EEscala.Semana ? 7 * (x.TempoPrevisto ?? 1) : x.TempoPrevisto ?? 1)
                     .FormatDateBr(),
                 ListTarefas = x.LTarefaTemplate.Select(y => new TarefaAtividadeResponse
                 {
                     Descricao = y.Descricao,
                     DescricaoTarefa = y.DescricaoTarefa,
                     Prioridade = y.Prioridade.GetHashCode().ToString(),
                     LTagsTarefa = y.TagTarefaTemplate.Select(z => z.Descricao).ToList()
                 }).ToList()
             }).ToList(),
         };
     }
}