using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ProfissaoMapping : IEntityTypeConfiguration<Profissao>
{
    public void Configure(EntityTypeBuilder<Profissao> builder)
    {
        builder.ToTable("Profissao");
        
        builder.HasKey(o => o.IdProfissao);
        builder.Property(t => t.Descricao).HasColumnName("Descricao").IsRequired();
    }
}