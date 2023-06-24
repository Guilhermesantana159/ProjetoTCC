using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ComentarioTarefaMapping : IEntityTypeConfiguration<ComentarioTarefa>
{
    public void Configure(EntityTypeBuilder<ComentarioTarefa> builder)
    {
        builder.ToTable("ComentarioTarefa");

        builder.HasKey(o => o.IdComentarioTarefa);
        builder.Property(o => o.IdTarefa).IsRequired();
        builder.Property(o => o.IdUsuario).IsRequired();
        builder.Property(o => o.Descricao).IsRequired();
        builder.Property(o => o.Data).IsRequired();

        builder
            .HasOne(x => x.Tarefa)
            .WithMany(x => x.ComentarioTarefa)
            .HasForeignKey(x => x.IdTarefa)
            .OnDelete(DeleteBehavior.ClientCascade);


        builder
            .HasOne(x => x.Usuario)
            .WithMany(x => x.ComentarioTarefa)
            .HasForeignKey(x => x.IdUsuario)
            .OnDelete(DeleteBehavior.ClientCascade);

    }
}