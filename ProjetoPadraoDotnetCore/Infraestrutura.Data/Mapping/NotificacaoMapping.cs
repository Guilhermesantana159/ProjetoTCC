using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.ToTable("Notificacao");

        builder.HasKey(o => o.IdNotificacao);
        builder.Property(t => t.DataCadastro).HasColumnName("DataCadastro").IsRequired();
        builder.Property(t => t.DataVisualização).HasColumnName("DataVisualização").IsRequired(false);
        builder.Property(t => t.Lido).HasColumnName("Lido").IsRequired();
        builder.Property(t => t.Corpo).HasMaxLength(800).HasColumnName("Corpo").IsRequired();
        builder.Property(t => t.Titulo).HasMaxLength(150).HasColumnName("Titulo").IsRequired();
        builder.Property(t => t.ClassficacaoMensagem).HasColumnName("ClassficacaoMensagem").IsRequired();

        builder
            .HasOne(t => t.Usuario)
            .WithMany(t => t.LNotificacaoUsuarios)
            .HasForeignKey(t => t.IdUsuario);
        
    }
}