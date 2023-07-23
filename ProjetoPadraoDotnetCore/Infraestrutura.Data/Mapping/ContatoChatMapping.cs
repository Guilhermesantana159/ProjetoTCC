using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class ContatoChatMapping : IEntityTypeConfiguration<ContatoChat>
{
    public void Configure(EntityTypeBuilder<ContatoChat> builder)
    {
        builder.ToTable("ContatoChat");

        builder.HasKey(o => o.IdContatoChat);
        builder.Property(o => o.IdUsuarioCadastro).IsRequired();
        builder.Property(o => o.IdUsuarioContato).IsRequired();
        builder.Property(o => o.StatusContato).IsRequired();
        builder.Property(o => o.DataCadastro).IsRequired();

        builder.HasOne(p => p.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioCadastro)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.UsuarioContato)
            .WithMany()
            .HasForeignKey(p => p.IdUsuarioContato)
            .OnDelete(DeleteBehavior.Restrict);
    }
}