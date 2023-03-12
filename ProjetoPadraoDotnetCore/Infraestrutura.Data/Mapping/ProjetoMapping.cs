using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ProjetoMapping : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("Projeto");

        builder.HasKey(o => o.IdProjeto);
        builder.Property(o => o.Titulo).IsRequired();
        builder.Property(o => o.Descricao);
        builder.Property(o => o.DataFim).IsRequired();
        builder.Property(o => o.DataInicio).IsRequired();
        builder.Property(o => o.ListarParaParticipantes).IsRequired();
        builder.Property(o => o.IdUsuarioCadastro).IsRequired();
        builder.Property(o => o.DataCadastro).IsRequired();
        
        builder
            .HasOne(t => t.Usuario)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioCadastro)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder
            .HasMany(t => t.LProjetoAtividade)
            .WithMany(x => x.Projeto);
    }
}