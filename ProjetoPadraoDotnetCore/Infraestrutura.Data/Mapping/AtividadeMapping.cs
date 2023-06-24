using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class AtividadeMapping : IEntityTypeConfiguration<Atividade>
{
    public void Configure(EntityTypeBuilder<Atividade> builder)
    {
        builder.ToTable("Atividade");

        builder.HasKey(o => o.IdAtividade);
        builder.Property(t => t.Titulo).IsRequired();
        builder.Property(t => t.DataInicial).IsRequired();
        builder.Property(t => t.DataFim).IsRequired();
        builder.Property(t => t.StatusAtividade).IsRequired();

        builder
            .HasOne(x => x.ProjetoFk)
            .WithMany(x => x.Atividades)
            .HasForeignKey(x => x.IdProjeto);

    }
}