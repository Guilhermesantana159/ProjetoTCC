using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ModuloMapping : IEntityTypeConfiguration<Modulo>
{
    public void Configure(EntityTypeBuilder<Modulo> builder)
    {
        builder.ToTable("Modulo");
        
        builder.HasKey(o => o.IdModulo);
        builder.Property(t => t.Nome).HasColumnName("Nome").IsRequired();
        builder.Property(t => t.Icone).HasColumnName("Icone").IsRequired();
        builder.Property(t => t.DescricaoLabel).HasColumnName("DescricaoLabel").IsRequired();
        builder.Property(t => t.DescricaoModulo).HasColumnName("DescricaoModulo").IsRequired(false);
    }
}