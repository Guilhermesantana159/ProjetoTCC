using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TemplateMapping : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("Template");

        builder.HasKey(o => o.IdTemplate);
        builder.Property(o => o.Titulo).IsRequired();
        builder.Property(o => o.Descricao);
        builder.Property(o => o.Escala).IsRequired();
        builder.Property(o => o.QuantidadeTotal).IsRequired();
        builder.Property(o => o.DataCadastro).IsRequired();
        builder.Property(o => o.Foto);

        builder.HasOne(p => p.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioCadastro)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.CategoriaTemplate)
            .WithMany()
            .HasForeignKey(t => t.IdTemplateCategoria)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(t => t.LAtividadesTemplate)
            .WithOne(at => at.Template)
            .HasForeignKey(at => at.IdTemplate);
    }
}