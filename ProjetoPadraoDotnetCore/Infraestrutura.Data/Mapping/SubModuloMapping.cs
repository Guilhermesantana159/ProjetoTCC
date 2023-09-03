using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class SubModuloMapping : IEntityTypeConfiguration<SubModulo>
{
    public void Configure(EntityTypeBuilder<SubModulo> builder)
    {
        builder.ToTable("SubModulo");
        
        builder.HasKey(o => o.IdSubModulo);
        builder.Property(t => t.Nome).HasColumnName("Nome").IsRequired();
        builder.Property(t => t.Icone).HasColumnName("Icone").IsRequired();
        
        builder
            .HasOne(p => p.Modulo)
            .WithMany(b => b.LSubModulo)
            .HasForeignKey(p => p.IdModulo)
            .IsRequired();
    }
}