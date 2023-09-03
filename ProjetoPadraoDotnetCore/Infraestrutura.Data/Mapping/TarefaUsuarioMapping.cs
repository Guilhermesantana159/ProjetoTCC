using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TarefaUsuarioMapping : IEntityTypeConfiguration<TarefaUsuario>
{
    public void Configure(EntityTypeBuilder<TarefaUsuario> builder)
    {
        builder.ToTable("TarefaUsuario");

        builder.HasKey(bc => new { bc.IdTarefa, bc.IdUsuario });
        
        builder
            .HasOne(bc => bc.Tarefa)
            .WithMany(c => c.TarefaUsuario)
            .HasForeignKey(bc => bc.IdTarefa);
        
        builder
            .HasOne(bc => bc.Usuario)
            .WithMany(c => c.TarefaUsuario)
            .HasForeignKey(bc => bc.IdUsuario)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}