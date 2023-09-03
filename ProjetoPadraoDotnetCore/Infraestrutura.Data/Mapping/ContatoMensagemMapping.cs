using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ContatoMensagemMapping : IEntityTypeConfiguration<ContatoMensagem>
{
    public void Configure(EntityTypeBuilder<ContatoMensagem> builder)
    {
        builder.ToTable("ContatoMensagem");
        
        builder.HasKey(o => o.IdContatoMensagem);
        builder.Property(t => t.Nome).HasColumnName("Nome").IsRequired();
        builder.Property(t => t.Email).HasColumnName("Email").IsRequired();
        builder.Property(t => t.Mensagem).HasColumnName("Mensagem").IsRequired();
        builder.Property(t => t.DataCadastro).HasColumnName("DataCadastro").IsRequired();
    }
}