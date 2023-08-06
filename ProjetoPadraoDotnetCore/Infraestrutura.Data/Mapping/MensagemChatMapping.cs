using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class MensagemChatMapping : IEntityTypeConfiguration<MensagemChat>
{
    public void Configure(EntityTypeBuilder<MensagemChat> builder)
    {
        builder.ToTable("MensagemChat");

        builder.HasKey(o => o.IdMensagemChat);
        builder.Property(o => o.IdUsuarioMandante).IsRequired();
        builder.Property(o => o.IdUsuarioRecebe).IsRequired();
        builder.Property(o => o.IdContatoRecebe).IsRequired();
        builder.Property(o => o.Message).IsRequired();
        builder.Property(o => o.DataCadastro).IsRequired();
        builder.Property(o => o.ReplayName);
        builder.Property(o => o.IdUsuarioExclusao).IsRequired(false);
        builder.Property(o => o.ReplayMessage);
        builder.Property(o => o.StatusMessage).IsRequired();
        builder.Property(o => o.IdContatoRecebe);

        builder
            .HasOne(t => t.UsuarioMandante)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioMandante)
            .IsRequired(false);
        
        builder
            .HasOne(t => t.UsuarioRecebe)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioRecebe)
            .IsRequired(false);
        
        builder
            .HasOne(t => t.ContatoRecebeChat)
            .WithMany()
            .HasForeignKey(t => t.IdContatoRecebe)
            .IsRequired(false);

        builder
            .HasOne(t => t.UsuarioExclusao)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioExclusao)
            .IsRequired(false);
    }
}