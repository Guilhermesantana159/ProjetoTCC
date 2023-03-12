using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class AtividadeTarefaMapping : IEntityTypeConfiguration<AtividadeTarefa>
{
    public void Configure(EntityTypeBuilder<AtividadeTarefa> builder)
    {
        builder.ToTable("AtividadeTarefa");

        builder.HasKey(o => new {o.IdAtividade,o.IdTarefa});

        builder
            .HasMany(t => t.Atividade)
            .WithMany(x => x.LAtividadeTarefa);

        builder
            .HasMany(t => t.Tarefa)
            .WithMany(x => x.LAtividadeTarefa);

    }
}