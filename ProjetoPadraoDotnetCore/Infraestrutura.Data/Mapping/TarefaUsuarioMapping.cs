using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TarefaUsuarioMapping : IEntityTypeConfiguration<TarefaUsuario>
{
    public void Configure(EntityTypeBuilder<TarefaUsuario> builder)
    {
        builder.ToTable("TarefaUsuario");

        builder.HasKey(o => new {o.IdUsuario,o.IdTarefa});

        builder
            .HasMany(t => t.Tarefa)
            .WithMany(x => x.LTarefaUsuario);

        builder
            .HasMany(t => t.Usuario)
            .WithMany(x => x.LTarefaUsuario);

    }
}