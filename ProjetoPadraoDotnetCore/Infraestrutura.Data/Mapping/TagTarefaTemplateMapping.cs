using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class TagTarefaTemplateMapping : IEntityTypeConfiguration<TagTarefaTemplate>
{
    public void Configure(EntityTypeBuilder<TagTarefaTemplate> builder)
    {
        builder.ToTable("TagTarefaTemplate");

        builder.HasKey(o => o.IdTagTarefaTemplate);
        builder.Property(o => o.Descricao).IsRequired();
        builder.Property(o => o.IdTarefaTemplate);
        
        
        builder
            .HasOne(x => x.TarefaTemplate)
            .WithMany(x => x.TagTarefaTemplate)
            .HasForeignKey(x => x.IdTarefaTemplate)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}