using System.Reflection;
using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.DataBaseContext;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());    
    }

    //Injeção dos dataSets
    public DbSet<ContatoChat> ContatoChat { get; set; } = null!;
    public DbSet<Atividade> Atividade { get; set; } = null!;
    public DbSet<ComentarioTarefa> ComentarioTarefa { get; set; } = null!;
    public DbSet<Menu> Menu { get; set; } = null!;
    public DbSet<Modulo> Modulo { get; set; } = null!;
    public DbSet<Notificacao> Notificacao { get; set; } = null!;
    public DbSet<Projeto> Projeto { get; set; } = null!;
    public DbSet<SkillUsuario>? SkillUsuario { get; set; } = null!;
    public DbSet<SubModulo> SubModulo { get; set; } = null!;
    public DbSet<TagTarefa> TagTarefa { get; set; } = null!;
    public DbSet<Tarefa> Tarefa { get; set; } = null!;
    public DbSet<TarefaUsuario> TarefaUsuario { get; set; } = null!;
    public DbSet<Usuario> Usuario { get; set; } = null!;
    public DbSet<MovimentacaoTarefa> MovimentacaoTarefa { get; set; } = null!;
    public DbSet<Template> Template { get; set; } = null!;
    public DbSet<AtividadeTemplate> AtividadeTemplate { get; set; } = null!;
    public DbSet<TarefaTemplate> TarefaTemplate { get; set; } = null!;
    public DbSet<CategoriaTemplate> CategoriaTemplate { get; set; } = null!;
    public DbSet<TagTarefaTemplate> TagTarefaTemplate { get; set; } = null!;
}