using Aplication.Authentication;
using Aplication.Controllers;
using Aplication.Interfaces;
using Aplication.Utils.Email;
using Aplication.Utils.HashCripytograph;
using Aplication.Utils.ValidatorDocument;
using Aplication.Validators.EstruturaMenu;
using Aplication.Validators.Projeto;
using Aplication.Validators.Usuario;
using Aplication.Validators.Utils;
using Domain.Interfaces;
using Domain.Services;
using Infraestrutura.DataBaseContext;
using Infraestrutura.Reports.Service;
using Infraestrutura.Reports.Usuario;
using Infraestrutura.Repository.External;
using Infraestrutura.Repository.Interface.Atividade;
using Infraestrutura.Repository.Interface.Atividade_Tarefa;
using Infraestrutura.Repository.Interface.Base;
using Infraestrutura.Repository.Interface.Menu;
using Infraestrutura.Repository.Interface.Modulo;
using Infraestrutura.Repository.Interface.Notificacao;
using Infraestrutura.Repository.Interface.Projeto;
using Infraestrutura.Repository.Interface.Projeto_Atividade;
using Infraestrutura.Repository.Interface.SkillUsuario;
using Infraestrutura.Repository.Interface.Tarefa;
using Infraestrutura.Repository.Interface.Tarefa_Usuario;
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
            services.AddTransient<IEstruturaMenuValidator,EstruturaMenuValidator>();
            services.AddTransient<IUtilsValidator,UtilsValidatior>();
            #endregion

            #region Aplicação
            services.AddScoped<IEstruturaMenuApp, EstruturaMenuApp>();
            services.AddScoped<IUsuarioApp, UsuarioApp>();
            services.AddScoped<INotificacaoApp,NotificacaoApp>();
            services.AddScoped<IAuthApp, AuthApp>();
            services.AddScoped<IProjetoApp, ProjetoApp>();
            services.AddScoped<IUtilsApp, UtilsApp>();
            #endregion

            #region Domínio
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<INotificacaoService, NotificacaoService>();
            services.AddScoped<IEstruturaMenuService, EstruturaMenuService>();
            services.AddScoped<IUtilsService, UtilService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            #endregion

            #region Repositorio
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseReadRepository<>));
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));
            services.AddScoped<IUsuarioReadRepository, UsuarioReadRepository>();
            services.AddScoped<IUsuarioWriteRepository, UsuarioWriteRepository>();
            services.AddScoped<IModuloReadRepository, ModuloReadRepository>();
            services.AddScoped<IModuloWriteRepository, ModuloWriteRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
            services.AddScoped<IExternalRepository, ExternalRepository>();
            services.AddScoped<ISkillUsuarioReadRepository, SkillUsuarioReadRepository>();
            services.AddScoped<ISkillUsuarioWriteRepository, SkillUsuarioWriteRepository>();
            services.AddScoped<IUsuarioReadRepository, UsuarioReadRepository>();
            services.AddScoped<INotificacaoWriteRepository,NotificacaoWriteRepository>();
            services.AddScoped<INotificacaoReadRepository,NotificacaoReadRepository>();
            services.AddScoped<IProjetoWriteRepository,ProjetoWriteRepository>();
            services.AddScoped<IProjetoReadRepository,ProjetoReadRepository>();
            services.AddScoped<IAtividadeWriteRepository,AtividadeWriteRepository>();
            services.AddScoped<IAtividadeReadRepository,AtividadeReadRepository>();
            services.AddScoped<ITarefaWriteRepository,TarefaWriteRepository>();
            services.AddScoped<ITarefaReadRepository,TarefaReadRepository>();
            services.AddScoped<IProjetoAtividadeWriteRepository,ProjetoAtividadeWriteRepository>();
            services.AddScoped<IProjetoAtividadeReadRepository,ProjetoAtividadeReadRepository>();
            services.AddScoped<IAtividadeTarefaWriteRepository,AtividadeTarefaWriteRepository>();
            services.AddScoped<IAtividadeTarefaReadRepository,AtividadeTarefaReadRepository>();
            services.AddScoped<ITarefaUsuarioWriteRepository,TarefaUsuarioWriteRepository>();
            services.AddScoped<ITarefaUsuarioReadRepository,TarefaUsuarioReadRepository>();
            #endregion

            #region Reports
            services.AddScoped<IUsuarioGridBuildReport, UsuarioGridBuildReport>();
            services.AddScoped<IReportsService, ReportsService>();
            #endregion
            
            
            //Context
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
        }
    }
}
