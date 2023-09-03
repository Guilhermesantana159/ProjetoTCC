using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class FeedbackMapping : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedback");

        builder.HasKey(o => o.IdFeedback);
        builder.Property(o => o.IdUsuarioCadastro).IsRequired();
        builder.Property(o => o.Rating).IsRequired();
        builder.Property(o => o.Comentario);
        builder.Property(o => o.DataCadastro).IsRequired();

        builder
            .HasOne(t => t.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioCadastro)
            .IsRequired();
        
    }
}