using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class MovimentacaoTarefaMapping : IEntityTypeConfiguration<MovimentacaoTarefa>
{
    public void Configure(EntityTypeBuilder<MovimentacaoTarefa> builder)
    {
        builder.ToTable("MovimentacaoTarefa");

        builder.HasKey(o => o.IdMovimentacaoTarefa);
        builder.Property(o => o.IdTarefa).IsRequired();
        builder.Property(o => o.DataCadastro).IsRequired();
        builder.Property(o => o.To).IsRequired();
        builder.Property(o => o.From).IsRequired();
        builder.Property(o => o.TempoUtilizadoUltimaColuna).IsRequired();

        builder
            .HasOne(x => x.Tarefa)
            .WithMany(x => x.MovimentacaoTarefa)
            .HasForeignKey(x => x.IdTarefa)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(x => x.Usuario)
            .WithMany(x => x.MovimentacaoTarefa)
            .HasForeignKey(x => x.IdUsuarioMovimentacao)
            .OnDelete(DeleteBehavior.ClientCascade);

    }
}