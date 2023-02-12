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
    public DbSet<Profissao> Profissao { get; set; } = null!;
    public DbSet<Usuario> Usuario { get; set; } = null!;
    public DbSet<SkillUsuario> SkillUsuario { get; set; } = null!;
    public DbSet<Modulo> Modulo { get; set; } = null!;
    public DbSet<Menu> Menu { get; set; } = null!;
}