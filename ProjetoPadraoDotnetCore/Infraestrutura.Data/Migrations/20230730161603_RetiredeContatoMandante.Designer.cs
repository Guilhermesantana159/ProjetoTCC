﻿// <auto-generated />
using System;
using Infraestrutura.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestrutura.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230730161603_RetiredeContatoMandante")]
    partial class RetiredeContatoMandante
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infraestrutura.Entity.Atividade", b =>
                {
                    b.Property<int>("IdAtividade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAtividade"), 1L, 1);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProjeto")
                        .HasColumnType("int");

                    b.Property<int>("StatusAtividade")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAtividade");

                    b.HasIndex("IdProjeto");

                    b.ToTable("Atividade", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.AtividadeTemplate", b =>
                {
                    b.Property<int?>("IdAtvidadeTemplate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdAtvidadeTemplate"), 1L, 1);

                    b.Property<int?>("IdTemplate")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Posicao")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TempoPrevisto")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAtvidadeTemplate");

                    b.HasIndex("IdTemplate");

                    b.ToTable("AtividadeTemplate", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.CategoriaTemplate", b =>
                {
                    b.Property<int?>("IdCategoriaTemplate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdCategoriaTemplate"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdUsuarioCadastro")
                        .HasColumnType("int");

                    b.HasKey("IdCategoriaTemplate");

                    b.HasIndex("IdUsuarioCadastro");

                    b.ToTable("CategoriaTemplate", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.ComentarioTarefa", b =>
                {
                    b.Property<int?>("IdComentarioTarefa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdComentarioTarefa"), 1L, 1);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTarefa")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdComentarioTarefa");

                    b.HasIndex("IdTarefa");

                    b.HasIndex("IdUsuario");

                    b.ToTable("ComentarioTarefa", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.ContatoChat", b =>
                {
                    b.Property<int?>("IdContatoChat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdContatoChat"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuarioCadastro")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioContato")
                        .HasColumnType("int");

                    b.Property<int>("StatusContato")
                        .HasColumnType("int");

                    b.HasKey("IdContatoChat");

                    b.HasIndex("IdUsuarioCadastro");

                    b.HasIndex("IdUsuarioContato");

                    b.ToTable("ContatoChat", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.MensagemChat", b =>
                {
                    b.Property<int>("IdMensagemChat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMensagemChat"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdContatoRecebe")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuarioExclusao")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioMandante")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioRecebe")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplayMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusMessage")
                        .HasColumnType("int");

                    b.HasKey("IdMensagemChat");

                    b.HasIndex("IdContatoRecebe");

                    b.HasIndex("IdUsuarioExclusao");

                    b.HasIndex("IdUsuarioMandante");

                    b.HasIndex("IdUsuarioRecebe");

                    b.ToTable("MensagemChat", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Menu", b =>
                {
                    b.Property<int>("IdMenu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMenu"), 1L, 1);

                    b.Property<int>("IdSubModulo")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Link");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<bool>("OnlyAdmin")
                        .HasColumnType("bit")
                        .HasColumnName("OnlyAdmin");

                    b.HasKey("IdMenu");

                    b.HasIndex("IdSubModulo");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Modulo", b =>
                {
                    b.Property<int>("IdModulo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdModulo"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("IdModulo");

                    b.ToTable("Modulo", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.MovimentacaoTarefa", b =>
                {
                    b.Property<int?>("IdMovimentacaoTarefa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdMovimentacaoTarefa"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("From")
                        .HasColumnType("int");

                    b.Property<int>("IdTarefa")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioMovimentacao")
                        .HasColumnType("int");

                    b.Property<long>("TempoUtilizadoUltimaColuna")
                        .HasColumnType("bigint");

                    b.Property<int>("To")
                        .HasColumnType("int");

                    b.HasKey("IdMovimentacaoTarefa");

                    b.HasIndex("IdTarefa");

                    b.HasIndex("IdUsuarioMovimentacao");

                    b.ToTable("MovimentacaoTarefa", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Notificacao", b =>
                {
                    b.Property<int>("IdNotificacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNotificacao"), 1L, 1);

                    b.Property<int>("ClassficacaoMensagem")
                        .HasColumnType("int")
                        .HasColumnName("ClassficacaoMensagem");

                    b.Property<string>("Corpo")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)")
                        .HasColumnName("Corpo");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataCadastro");

                    b.Property<DateTime?>("DataVisualização")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataVisualização");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Lido")
                        .HasColumnType("int")
                        .HasColumnName("Lido");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Titulo");

                    b.HasKey("IdNotificacao");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Notificacao", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Projeto", b =>
                {
                    b.Property<int>("IdProjeto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProjeto"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuarioCadastro")
                        .HasColumnType("int");

                    b.Property<bool>("ListarParaParticipantes")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProjeto");

                    b.HasIndex("IdUsuarioCadastro");

                    b.ToTable("Projeto", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.SkillUsuario", b =>
                {
                    b.Property<int>("IdSkill")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSkill"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdSkill");

                    b.HasIndex("IdUsuario");

                    b.ToTable("SkillUsuario", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.SubModulo", b =>
                {
                    b.Property<int>("IdSubModulo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSubModulo"), 1L, 1);

                    b.Property<string>("Icone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Icone");

                    b.Property<int>("IdModulo")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("IdSubModulo");

                    b.HasIndex("IdModulo");

                    b.ToTable("SubModulo", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.TagTarefa", b =>
                {
                    b.Property<int>("IdTagTarefa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTagTarefa"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdTarefa")
                        .HasColumnType("int");

                    b.HasKey("IdTagTarefa");

                    b.HasIndex("IdTarefa");

                    b.ToTable("TagTarefa", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.TagTarefaTemplate", b =>
                {
                    b.Property<int>("IdTagTarefaTemplate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTagTarefaTemplate"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdTarefaTemplate")
                        .HasColumnType("int");

                    b.HasKey("IdTagTarefaTemplate");

                    b.HasIndex("IdTarefaTemplate");

                    b.ToTable("TagTarefaTemplate", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Tarefa", b =>
                {
                    b.Property<int>("IdTarefa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTarefa"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoTarefa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("IdTarefa");

                    b.HasIndex("IdAtividade");

                    b.ToTable("Tarefa", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.TarefaTemplate", b =>
                {
                    b.Property<int>("IdTarefaTemplate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTarefaTemplate"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoTarefa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAtividadeTemplate")
                        .HasColumnType("int");

                    b.Property<int?>("Prioridade")
                        .HasColumnType("int");

                    b.HasKey("IdTarefaTemplate");

                    b.HasIndex("IdAtividadeTemplate");

                    b.ToTable("TarefaTemplate", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.TarefaUsuario", b =>
                {
                    b.Property<int?>("IdTarefa")
                        .HasColumnType("int");

                    b.Property<int?>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdTarefa", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.ToTable("TarefaUsuario", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Template", b =>
                {
                    b.Property<int>("IdTemplate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTemplate"), 1L, 1);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Escala")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdTemplateCategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuarioCadastro")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeTotal")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTemplate");

                    b.HasIndex("IdTemplateCategoria");

                    b.HasIndex("IdUsuarioCadastro");

                    b.ToTable("Template", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cidade");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cep");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cidade1");

                    b.Property<int?>("CodigoRecuperarSenha")
                        .HasColumnType("int")
                        .HasColumnName("CodigoRecuperarSenha");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("CPF");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataNascimento");

                    b.Property<DateTime?>("DataRecuperacaoSenha")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataRecuperacaoSenha");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Estado");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genero")
                        .HasColumnType("int")
                        .HasColumnName("Genero");

                    b.Property<int?>("IdUsuarioCadastro")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeMae")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NomeMae");

                    b.Property<string>("NomePai")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NomePai");

                    b.Property<int?>("Numero")
                        .HasColumnType("int")
                        .HasColumnName("Numero");

                    b.Property<string>("Observacao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Observacao");

                    b.Property<string>("Pais")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Pais");

                    b.Property<bool>("PerfilAdministrador")
                        .HasColumnType("bit")
                        .HasColumnName("PerfilAdministrador");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Rua");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Senha");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Telefone");

                    b.Property<int?>("TentativasRecuperarSenha")
                        .HasColumnType("int")
                        .HasColumnName("TentativasRecuperarSenha");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdUsuarioCadastro");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Infraestrutura.Entity.Atividade", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Projeto", "ProjetoFk")
                        .WithMany("Atividades")
                        .HasForeignKey("IdProjeto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjetoFk");
                });

            modelBuilder.Entity("Infraestrutura.Entity.AtividadeTemplate", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Template", "Template")
                        .WithMany("LAtividadesTemplate")
                        .HasForeignKey("IdTemplate")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Infraestrutura.Entity.CategoriaTemplate", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioCadastro")
                        .WithMany()
                        .HasForeignKey("IdUsuarioCadastro")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UsuarioCadastro");
                });

            modelBuilder.Entity("Infraestrutura.Entity.ComentarioTarefa", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Tarefa", "Tarefa")
                        .WithMany("ComentarioTarefa")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany("ComentarioTarefa")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.ContatoChat", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioCadastro")
                        .WithMany()
                        .HasForeignKey("IdUsuarioCadastro")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioContato")
                        .WithMany()
                        .HasForeignKey("IdUsuarioContato")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UsuarioCadastro");

                    b.Navigation("UsuarioContato");
                });

            modelBuilder.Entity("Infraestrutura.Entity.MensagemChat", b =>
                {
                    b.HasOne("Infraestrutura.Entity.ContatoChat", "ContatoRecebeChat")
                        .WithMany()
                        .HasForeignKey("IdContatoRecebe");

                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioExclusao")
                        .WithMany()
                        .HasForeignKey("IdUsuarioExclusao");

                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioMandante")
                        .WithMany()
                        .HasForeignKey("IdUsuarioMandante");

                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioRecebe")
                        .WithMany()
                        .HasForeignKey("IdUsuarioRecebe");

                    b.Navigation("ContatoRecebeChat");

                    b.Navigation("UsuarioExclusao");

                    b.Navigation("UsuarioMandante");

                    b.Navigation("UsuarioRecebe");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Menu", b =>
                {
                    b.HasOne("Infraestrutura.Entity.SubModulo", "SubModulo")
                        .WithMany("LMenus")
                        .HasForeignKey("IdSubModulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubModulo");
                });

            modelBuilder.Entity("Infraestrutura.Entity.MovimentacaoTarefa", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Tarefa", "Tarefa")
                        .WithMany("MovimentacaoTarefa")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany("MovimentacaoTarefa")
                        .HasForeignKey("IdUsuarioMovimentacao")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Notificacao", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany("LNotificacaoUsuarios")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Projeto", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuarioCadastro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.SkillUsuario", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany("LSkillUsuarios")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.SubModulo", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Modulo", "Modulo")
                        .WithMany("LSubModulo")
                        .HasForeignKey("IdModulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");
                });

            modelBuilder.Entity("Infraestrutura.Entity.TagTarefa", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Tarefa", "Tarefa")
                        .WithMany("TagTarefa")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("Infraestrutura.Entity.TagTarefaTemplate", b =>
                {
                    b.HasOne("Infraestrutura.Entity.TarefaTemplate", "TarefaTemplate")
                        .WithMany("TagTarefaTemplate")
                        .HasForeignKey("IdTarefaTemplate")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("TarefaTemplate");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Tarefa", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Atividade", "AtividadeFk")
                        .WithMany("Tarefas")
                        .HasForeignKey("IdAtividade")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtividadeFk");
                });

            modelBuilder.Entity("Infraestrutura.Entity.TarefaTemplate", b =>
                {
                    b.HasOne("Infraestrutura.Entity.AtividadeTemplate", "AtividadeTemplate")
                        .WithMany("LTarefaTemplate")
                        .HasForeignKey("IdAtividadeTemplate")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AtividadeTemplate");
                });

            modelBuilder.Entity("Infraestrutura.Entity.TarefaUsuario", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Tarefa", "Tarefa")
                        .WithMany("TarefaUsuario")
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infraestrutura.Entity.Usuario", "Usuario")
                        .WithMany("TarefaUsuario")
                        .HasForeignKey("IdUsuario")
                        .IsRequired();

                    b.Navigation("Tarefa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Template", b =>
                {
                    b.HasOne("Infraestrutura.Entity.CategoriaTemplate", "CategoriaTemplate")
                        .WithMany()
                        .HasForeignKey("IdTemplateCategoria")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioCadastro")
                        .WithMany()
                        .HasForeignKey("IdUsuarioCadastro")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoriaTemplate");

                    b.Navigation("UsuarioCadastro");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Usuario", b =>
                {
                    b.HasOne("Infraestrutura.Entity.Usuario", "UsuarioFk")
                        .WithMany()
                        .HasForeignKey("IdUsuarioCadastro");

                    b.Navigation("UsuarioFk");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Atividade", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("Infraestrutura.Entity.AtividadeTemplate", b =>
                {
                    b.Navigation("LTarefaTemplate");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Modulo", b =>
                {
                    b.Navigation("LSubModulo");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Projeto", b =>
                {
                    b.Navigation("Atividades");
                });

            modelBuilder.Entity("Infraestrutura.Entity.SubModulo", b =>
                {
                    b.Navigation("LMenus");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Tarefa", b =>
                {
                    b.Navigation("ComentarioTarefa");

                    b.Navigation("MovimentacaoTarefa");

                    b.Navigation("TagTarefa");

                    b.Navigation("TarefaUsuario");
                });

            modelBuilder.Entity("Infraestrutura.Entity.TarefaTemplate", b =>
                {
                    b.Navigation("TagTarefaTemplate");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Template", b =>
                {
                    b.Navigation("LAtividadesTemplate");
                });

            modelBuilder.Entity("Infraestrutura.Entity.Usuario", b =>
                {
                    b.Navigation("ComentarioTarefa");

                    b.Navigation("LNotificacaoUsuarios");

                    b.Navigation("LSkillUsuarios");

                    b.Navigation("MovimentacaoTarefa");

                    b.Navigation("TarefaUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
