using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class AtvidadeTemplateMapping : IEntityTypeConfiguration<AtividadeTemplate>
{
    public void Configure(EntityTypeBuilder<AtividadeTemplate> builder)
    {
        builder.ToTable("AtividadeTemplate");

        builder.HasKey(o => o.IdAtvidadeTemplate);
        builder.Property(o => o.IdTemplate).IsRequired();
        builder.Property(o => o.TempoPrevisto).IsRequired();
        builder.Property(o => o.Titulo).IsRequired();
        builder.Property(o => o.Posicao).IsRequired();

        builder.HasOne(p => p.Template)
            .WithMany(p => p.LAtividadesTemplate)
            .HasForeignKey(p => p.IdTemplate)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}