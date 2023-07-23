using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TagTarefaMapping : IEntityTypeConfiguration<TagTarefa>
{
    public void Configure(EntityTypeBuilder<TagTarefa> builder)
    {
        builder.ToTable("TagTarefa");

        builder.HasKey(o => o.IdTagTarefa);
        builder.Property(o => o.Descricao).IsRequired();
        builder.Property(o => o.IdTarefa);

        builder
            .HasOne(x => x.Tarefa)
            .WithMany(x => x.TagTarefa)
            .HasForeignKey(x => x.IdTarefa)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}