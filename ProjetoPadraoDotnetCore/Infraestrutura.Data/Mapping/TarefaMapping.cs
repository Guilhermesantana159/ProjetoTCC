using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("Tarefa");

        builder.HasKey(o => o.IdTarefa);
        builder.Property(o => o.Descricao).IsRequired();
        builder.Property(o => o.DescricaoTarefa);
        builder.Property(o => o.Prioridade).IsRequired();
        builder.Property(o => o.Status).IsRequired();

        builder
            .HasOne(x => x.AtividadeFk)
            .WithMany(x => x.Tarefas)
            .HasForeignKey(x => x.IdAtividade);
        
    }
}