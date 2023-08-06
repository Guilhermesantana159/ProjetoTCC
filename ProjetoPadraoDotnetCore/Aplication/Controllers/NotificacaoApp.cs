using System.Text;
using Aplication.Interfaces;
using Aplication.Models.Request.Notificao;
using Aplication.Utils.Email;
using Aplication.Utils.Helpers;
using Domain.DTO.Tarefa;
using Domain.Interfaces;
using Infraestrutura.Entity;
using Infraestrutura.Enum;

namespace Aplication.Controllers;

public class NotificacaoApp : INotificacaoApp
{
    protected readonly INotificacaoService Service;
    protected readonly IProjetoService ProjetoService;
    protected readonly ITarefaService TarefaService;
    protected readonly IEmailHelper EmailHelper;

    public NotificacaoApp(INotificacaoService service, IProjetoService projetoService, IEmailHelper emailHelper,
        ITarefaService tarefaService)
    {
        Service = service;
        ProjetoService = projetoService;
        EmailHelper = emailHelper;
        TarefaService = tarefaService;
    }

    public List<Notificacao> GetNotificaçõesByUser(int id)
    {
        var listNotificacao = Service.GetAllQuery().Where(x => x.IdUsuario == id).OrderByDescending(x => x.DataCadastro);

        var listNotificacaoAntigas = listNotificacao.Where(x =>
            x.DataVisualização != null && x.Lido == ESimNao.Sim && x.DataVisualização.Value.AddDays(10) > DateTime.Now);

        if (listNotificacaoAntigas.Any())
            Service.DeleteList(listNotificacaoAntigas.ToList());

        return listNotificacao.ToList();
    }

    public void NotificacaoLida(NotificacaolidaRequest request)
    {
        var entityNotificacao = Service.GetById(request.IdNotificaoLida);

        if (entityNotificacao != null)
        {
            //Atribuição de lida
            entityNotificacao.DataVisualização = DateTime.Now;
            entityNotificacao.Lido = ESimNao.Sim;
            Service.Editar(entityNotificacao);
        }
    }

    public void NotificarProjetosAtrasados()
    {
        var lProjetosAtrasados = ProjetoService
            .GetAllWithIncludeQuery()
            .Where(x => x.DataFim < DateTime.Now && x.Atividades.Any(y =>
                y.DataFim < DateTime.Now && y.StatusAtividade != EStatusAtividade.Completo))
            .ToList();

        //Notificar Email

        foreach (var projeto in lProjetosAtrasados)
        {
            if (projeto.Usuario != null)
            {
                if (projeto.EmailProjetoAtrasado)
                {
                    var usuario = new List<string>();
                    usuario.Add(projeto.Usuario.Email);

                    //Corpo email
                    var corpo = new StreamReader(Environment.CurrentDirectory + "/Content/" + "ProjetoAtrasado.html")
                        .ReadToEnd();

                    //Caampos
                    corpo = corpo.Replace("#NomeProjeto#", projeto.Titulo);
                    corpo = corpo.Replace("#Cod#", $"#{projeto.IdProjeto.ToString()}");
                    corpo = corpo.Replace("#DataFim#", projeto.DataFim.FormatDateBr());

                    corpo = corpo.Replace("{0}",
                        string.Join("",
                            projeto.Atividades.Select(x => x.Titulo).ToList().ConvertAll(s => $"<li>{s}</li>")));

                    if (string.IsNullOrEmpty(corpo))
                        throw new Exception("Arquivo html projeto atrasado não encontrado!");

                    var email = EmailHelper.EnviarEmail(usuario, "Projeto Padrão - Projeto atrasado", corpo);
                    
                    //Colocar Log
                }

                if (projeto.PortalProjetoAtrasado)
                {
                    //Notificar no Sistema
                    var pushMensagem = new Notificacao()
                    {
                        IdUsuario = projeto.Usuario.IdUsuario,
                        DataCadastro = DateTime.Now,
                        Lido = ESimNao.Nao,
                        ClassficacaoMensagem = EMensagemNotificacao.ProjetoAtrasado,
                        Corpo =
                            $"Olá, o projeto {projeto.Titulo} Cód(#{projeto.IdProjeto}) possui atividades atrasadas. Entre na tela ADMINISTRAÇÃO DE ATIVIDADES para acompanhar.",
                        Titulo = $"Projeto {projeto.Titulo} está com status atrasado!",
                        DataVisualização = null,
                    };

                    Service.Cadastrar(pushMensagem);
                }
            }
        }
    }

    public void NotificarAtividadesAtrasadas()
    {
        var lAtividadeAtrasadas = TarefaService.GetTarefaUsuarioWithInclude()
            .Where(x => x.Tarefa.AtividadeFk.DataFim < DateTime.Now && x.Tarefa.Status != EStatusTarefa.Completo)
            .ToList()
            .GroupBy(x => x.Usuario);

        foreach (var projeto in lAtividadeAtrasadas)
        {
            var listProjeto = new List<string>();
            var corpo = new StreamReader(Environment.CurrentDirectory + "/Content/" + "TarefaAtrasada.html").ReadToEnd();
            var lProjeto = new List<ProjetoTarefa>();

            foreach (var atividade in projeto)
            {
                if (atividade.Tarefa.AtividadeFk.ProjetoFk.Titulo != null && atividade.Tarefa.AtividadeFk.ProjetoFk.EmailTarefaAtrasada)
                {
                    var projetoTitulo = atividade.Tarefa.AtividadeFk.ProjetoFk.Titulo;
                    
                    if(atividade.Tarefa.AtividadeFk.ProjetoFk.PortalTarefaAtrasada)
                        listProjeto.Add(projetoTitulo);

                    if (lProjeto.Any(x => x.Projeto == projetoTitulo))
                    {
                        if (atividade.Tarefa.Descricao != null && atividade.Tarefa.AtividadeFk.ProjetoFk.EmailTarefaAtrasada)
                            lProjeto.FirstOrDefault(x => x.Projeto == projetoTitulo)?.Tarefa?.Add(atividade.Tarefa.Descricao);
                    }
                    else
                    {
                        if (atividade.Tarefa.AtividadeFk.ProjetoFk.EmailTarefaAtrasada)
                        {
                            lProjeto.Add(new ProjetoTarefa()
                            {
                                Projeto = projetoTitulo,
                                Tarefa = new List<string>()
                            });
                        }
                    }
                }
            }

            //Notificar Email
            if (lProjeto.Any())
            {
                var stringBuilder = new StringBuilder();

                foreach (var item in lProjeto)
                {
                    stringBuilder.AppendLine($"<strong>{item.Projeto}</strong>");
                    stringBuilder.AppendLine("<ul>");

                    if (item.Tarefa != null)
                    {
                        foreach (var itens in item.Tarefa)
                        {
                            stringBuilder.AppendLine($"<li>{itens}</li>");
                        }
                    }
                
                    stringBuilder.AppendLine("</ul>");
                }

                var corpoMain = stringBuilder.ToString();
                
                var usuario = new List<string>();
                usuario.Add(projeto.Key.Email);

                corpo = corpo.Replace("{0}",  corpoMain);
            
                if (string.IsNullOrEmpty(corpo))
                    throw new Exception("Arquivo html projeto atrasado não encontrado!");

                var email = EmailHelper.EnviarEmail(usuario,"Projeto Padrão - Tarefas atrasadas",corpo);
            
                //Colocar Log

            }

            //Notificar no Sistema
            if (listProjeto.Any())
            {
                var pushMensagem = new Notificacao()
                {
                    IdUsuario = projeto.Key.IdUsuario, 
                    DataCadastro = DateTime.Now,
                    Lido = ESimNao.Nao,
                    ClassficacaoMensagem = EMensagemNotificacao.AtividadeAtrasada,
                    Corpo = $"Olá, você possui atividades atrasadas nos seguintes projetos: {string.Join("", listProjeto.Distinct().ToList().ConvertAll(s => $"{s}"))}.",
                    Titulo = $"Você possui tarefas atrasadas!",
                    DataVisualização = null,
                };
        
                Service.Cadastrar(pushMensagem);
            }
        }
    }
}

