using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ProjetoAtividadeMapping : IEntityTypeConfiguration<ProjetoAtividade>
{
    public void Configure(EntityTypeBuilder<ProjetoAtividade> builder)
    {
        builder.ToTable("ProjetoAtividade");

        builder.HasKey(o => new {o.IdAtividade,o.IdProjeto});

        builder
            .HasMany(t => t.Atividade)
            .WithMany(x => x.LProjetoAtividade);

        builder
            .HasMany(t => t.Projeto)
            .WithMany(x => x.LProjetoAtividade);

    }
}