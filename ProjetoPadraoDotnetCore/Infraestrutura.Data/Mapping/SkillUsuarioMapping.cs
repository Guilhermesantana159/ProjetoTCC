using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class SkillUsuarioMapping : IEntityTypeConfiguration<SkillUsuario>
{
    public void Configure(EntityTypeBuilder<SkillUsuario> builder)
    {
        builder.ToTable("SkillUsuario");
        
        builder.HasKey(o => o.IdSkill);
        builder.Property(t => t.Descricao).HasColumnName("Descricao").IsRequired();
        
        builder
            .HasOne(p => p.Usuario)
            .WithMany(b => b.LSkillUsuarios)
            .HasForeignKey(p => p.IdUsuario)
            .IsRequired();
    }
}