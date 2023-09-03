using Aplication.Authentication;
using Aplication.Controllers;
using Aplication.Interfaces;
using Aplication.Utils.Email;
using Aplication.Utils.HashCripytograph;
using Aplication.Utils.ValidatorDocument;
using Aplication.Validators.EstruturaMenu;
using Aplication.Validators.Projeto;
using Aplication.Validators.Tarefa;
using Aplication.Validators.Template;
using Aplication.Validators.Usuario;
using Aplication.Validators.Utils;
using Domain.Interfaces;
using Domain.Services;
using Infraestrutura.DataBaseContext;
using Infraestrutura.Reports.Projeto;
using Infraestrutura.Reports.Service;
using Infraestrutura.Reports.Usuario;
using Infraestrutura.Repository.External;
using Infraestrutura.Repository.Interface.Atividade;
using Infraestrutura.Repository.Interface.AtvidadeTemplate;
using Infraestrutura.Repository.Interface.Base;
using Infraestrutura.Repository.Interface.CategoriaTemplate;
using Infraestrutura.Repository.Interface.ComentarioTarefa;
using Infraestrutura.Repository.Interface.ContatoChat;
using Infraestrutura.Repository.Interface.ContatoMensagem;
using Infraestrutura.Repository.Interface.Feedback;
using Infraestrutura.Repository.Interface.MensagemChat;
using Infraestrutura.Repository.Interface.Menu;
using Infraestrutura.Repository.Interface.Modulo;
using Infraestrutura.Repository.Interface.MovimentacaoTarefa;
using Infraestrutura.Repository.Interface.SubModulo;
using Infraestrutura.Repository.Interface.Notificacao;
using Infraestrutura.Repository.Interface.Projeto;
using Infraestrutura.Repository.Interface.SkillUsuario;
using Infraestrutura.Repository.Interface.TagTarefa;
using Infraestrutura.Repository.Interface.TagTarefaTemplate;
using Infraestrutura.Repository.Interface.Tarefa;
using Infraestrutura.Repository.Interface.TarefaTemplate;
using Infraestrutura.Repository.Interface.Template;
using Infraestrutura.Repository.Interface.Usuario;
using Infraestrutura.Repository.ReadRepository;
using Infraestrutura.Repository.WriteRepository;
using Microsoft.EntityFrameworkCore;

namespace CrossCutting.IOC
{
    public static class DependencyInjectory
    {
        public static void Injectory(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region Utils
            services.AddTransient<IHashCriptograph, HashCripytograph>();
            services.AddTransient<IValidatorDocument, ValidatorDocument>();
            services.AddTransient<IJwtTokenAuthentication, JwtAuthentication>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddSingleton<IConfiguration>(builder.Configuration);
            #endregion

            #region Validators
            services.AddTransient<IProjetoValidator, ProjetoValidator>();
            services.AddTransient<IUsuarioValidator, UsuarioValidator>();
            services.AddTransient<ITarefaValidator, TarefaValidator>();
            services.AddTransient<IEstruturaMenuValidator,EstruturaMenuValidator>();
            services.AddTransient<IUtilsValidator,UtilsValidatior>();
            services.AddTransient<ITemplateValidator,TemplateValidator>();
            #endregion

            #region Aplicação
            services.AddScoped<IEstruturaMenuApp, EstruturaMenuApp>();
            services.AddScoped<IUsuarioApp, UsuarioApp>();
            services.AddScoped<INotificacaoApp,NotificacaoApp>();
            services.AddScoped<IAuthApp, AuthApp>();
            services.AddScoped<IProjetoApp, ProjetoApp>();
            services.AddScoped<ITarefaApp, TarefaApp>();
            services.AddScoped<IUtilsApp, UtilsApp>();
            services.AddScoped<IChatApp, ChatApp>();
            services.AddScoped<ITemplateApp, TemplateApp>();
            #endregion

            #region Domínio
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<INotificacaoService, NotificacaoService>();
            services.AddScoped<IEstruturaMenuService, EstruturaMenuService>();
            services.AddScoped<IUtilsService, UtilService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<IAtividadeService, AtividadeService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ITemplateService, TemplateService>();
            #endregion

            #region Repositorio
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseReadRepository<>));
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));
            services.AddScoped<IUsuarioReadRepository, UsuarioReadRepository>();
            services.AddScoped<IUsuarioWriteRepository, UsuarioWriteRepository>();
            
            services.AddScoped<ISubModuloReadRepository, SubModuloReadRepository>();
            services.AddScoped<ISubModuloWriteRepository, SubModuloWriteRepository>();
            
            services.AddScoped<IModuloReadRepository, ModuloReadRepository>();
            services.AddScoped<IModuloWriteRepository, ModuloWriteRepository>();
            
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            
            services.AddScoped<IExternalRepository, ExternalRepository>();
            
            services.AddScoped<ISkillUsuarioReadRepository, SkillUsuarioReadRepository>();
            services.AddScoped<ISkillUsuarioWriteRepository, SkillUsuarioWriteRepository>();
            
            services.AddScoped<INotificacaoWriteRepository,NotificacaoWriteRepository>();
            services.AddScoped<INotificacaoReadRepository,NotificacaoReadRepository>();
            
            services.AddScoped<IProjetoWriteRepository,ProjetoWriteRepository>();
            services.AddScoped<IProjetoReadRepository,ProjetoReadRepository>();
            
            services.AddScoped<IAtividadeWriteRepository,AtividadeWriteRepository>();
            services.AddScoped<IAtividadeReadRepository,AtividadeReadRepository>();
            
            services.AddScoped<ITarefaWriteRepository,TarefaWriteRepository>();
            services.AddScoped<ITarefaReadRepository,TarefaReadRepository>();
            
            services.AddScoped<ITagTarefaReadRepository, TagTarefaReadRepository>();
            services.AddScoped<ITagTarefaWriteRepository, TagTarefaWriteRepository>();
            
            services.AddScoped<IComentarioTarefaReadRepository, ComentarioTarefaReadRepository>();
            services.AddScoped<IComentarioTarefaWriteRepository, ComentarioTarefaWriteRepository>();
            
            services.AddScoped<IMovimentacaoTarefaReadRepository, MovimentacaoTarefaReadRepository>();
            services.AddScoped<IMovimentacaoTarefaWriteRepository, MovimentacaoTarefaWriteRepository>();
            
            services.AddScoped<IContatoChatReadRepository, ContatoChatReadRepository>();
            services.AddScoped<IContatoChatWriteRepository, ContatoChatWriteRepository>();
            
            services.AddScoped<ITemplateReadRepository, TemplateReadRepository>();
            services.AddScoped<ITemplateWriteRepository, TemplateWriteRepository>();
            
            services.AddScoped<ITarefaTemplateReadRepository, TarefaTemplateReadRepository>();
            services.AddScoped<ITarefaTemplateWriteRepository, TarefaTemplateWriteRepository>();  
            
            services.AddScoped<ICategoriaTemplateReadRepository, CategoriaTemplateReadRepository>();
            services.AddScoped<ICategoriaTemplateWriteRepository, CategoriaTemplateWriteRepository>();  
            
            services.AddScoped<IAtividadeTemplateReadRepository, AtividadeTemplateReadRepository>();
            services.AddScoped<IAtividadeTemplateWriteRepository, AtividadeTemplateWriteRepository>();
            
            services.AddScoped<ITagTarefaTemplateReadRepository, TagTarefaTemplateReadRepository>();
            services.AddScoped<ITagTarefaTemplateWriteRepository, TagTarefaTemplateWriteRepository>();
            
            services.AddScoped<IMensagemChatReadRepository, MensagemChatReadRepository>();
            services.AddScoped<IMensagemChatWriteRepository, MensagemChatWriteRepository>();
            
            services.AddScoped<IFeedbackReadRepository, FeedbackReadRepository>();
            services.AddScoped<IFeedbackWriteRepository, FeedbackWriteRepository>();
            
            services.AddScoped<IContatoMensagemReadRepository, ContatoMensagemReadRepository>();
            services.AddScoped<IContatoMensagemWriteRepository, ContatoMensagemWriteRepository>();

            #endregion

            #region Reports
            services.AddScoped<IProjetoGridBuildReport, ProjetoGridBuildReport>();
            services.AddScoped<IUsuarioGridBuildReport, UsuarioGridBuildReport>();
            services.AddScoped<IReportsService, ReportsService>();
            #endregion

            //Context
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            
        }
    }
}
