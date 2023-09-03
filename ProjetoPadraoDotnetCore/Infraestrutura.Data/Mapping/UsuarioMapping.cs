using Infraestrutura.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestrutura.Mapping;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(o => o.IdUsuario);
        builder.Property(t => t.Nome).IsRequired();
        builder.Property(t => t.Cpf).HasMaxLength(14).HasColumnName("CPF").IsRequired();
        builder.Property(t => t.Email).HasMaxLength(100).HasColumnName("Email").IsRequired();
        builder.Property(t => t.Senha).HasColumnName("Senha").IsRequired();
        builder.Property(t => t.Telefone).HasColumnName("Telefone");
        builder.Property(t => t.DataNascimento).HasColumnName("DataNascimento");
        builder.Property(t => t.PerfilAdministrador).HasColumnName("PerfilAdministrador");
        builder.Property(t => t.Cep).HasColumnName("Cep");
        builder.Property(t => t.Estado).HasColumnName("Estado");
        builder.Property(t => t.Bairro).HasColumnName("Cidade");
        builder.Property(t => t.Pais).HasColumnName("Pais");
        builder.Property(t => t.Rua).HasColumnName("Rua");
        builder.Property(t => t.Numero).HasColumnName("Numero");
        builder.Property(t => t.NomeMae).HasColumnName("NomeMae");
        builder.Property(t => t.NomePai).HasColumnName("NomePai");
        builder.Property(t => t.Observacao).HasColumnName("Observacao");
        builder.Property(t => t.Genero).HasColumnName("Genero");
        builder.Property(t => t.DataRecuperacaoSenha).HasColumnName("DataRecuperacaoSenha");
        builder.Property(t => t.TentativasRecuperarSenha).HasColumnName("TentativasRecuperarSenha");
        builder.Property(t => t.CodigoRecuperarSenha).HasColumnName("CodigoRecuperarSenha");

        builder
            .HasOne(t => t.UsuarioFk)
            .WithMany()
            .HasForeignKey(t => t.IdUsuarioCadastro)
            .IsRequired(false);
    }
}