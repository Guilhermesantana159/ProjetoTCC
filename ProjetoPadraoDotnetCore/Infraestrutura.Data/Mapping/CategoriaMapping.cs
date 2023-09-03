using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class CategoriaMapping : IEntityTypeConfiguration<CategoriaTemplate>
{
    public void Configure(EntityTypeBuilder<CategoriaTemplate> builder)
    {
        builder.ToTable("CategoriaTemplate");

        builder.HasKey(o => o.IdCategoriaTemplate);
        builder.Property(o => o.Descricao);
        builder.Property(o => o.DataCadastro).IsRequired();


        builder.HasOne(p => p.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioCadastro)
            .OnDelete(DeleteBehavior.Restrict);
    }
}