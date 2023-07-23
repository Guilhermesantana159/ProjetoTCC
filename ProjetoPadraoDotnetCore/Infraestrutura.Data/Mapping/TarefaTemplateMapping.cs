using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TarefaTemplateMapping : IEntityTypeConfiguration<TarefaTemplate>
{
    public void Configure(EntityTypeBuilder<TarefaTemplate> builder)
    {
        builder.ToTable("TarefaTemplate");

        builder.HasKey(o => o.IdTarefaTemplate);
        builder.Property(o => o.Descricao).IsRequired();
        builder.Property(o => o.IdAtividadeTemplate).IsRequired();

        builder.HasOne(p => p.AtividadeTemplate)
            .WithMany(x => x.LTarefaTemplate)
            .HasForeignKey(p => p.IdAtividadeTemplate)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}