using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class InitBanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulo",
                columns: table => new
                {
                    IdModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoModulo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.IdModulo);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerfilAdministrador = table.Column<bool>(type: "bit", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: true),
                    NomeMae = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomePai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: true),
                    CodigoRecuperarSenha = table.Column<int>(type: "int", nullable: true),
                    TentativasRecuperarSenha = table.Column<int>(type: "int", nullable: true),
                    DataRecuperacaoSenha = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_IdUsuarioCadastro",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    IdMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdModulo = table.Column<int>(type: "int", nullable: false),
                    DescricaoMenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlyAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.IdMenu);
                    table.ForeignKey(
                        name: "FK_Menu_Modulo_IdModulo",
                        column: x => x.IdModulo,
                        principalTable: "Modulo",
                        principalColumn: "IdModulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    IdNotificacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVisualização = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Corpo = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClassficacaoMensagem = table.Column<int>(type: "int", nullable: false),
                    Lido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.IdNotificacao);
                    table.ForeignKey(
                        name: "FK_Notificacao_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    IdProjeto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListarParaParticipantes = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.IdProjeto);
                    table.ForeignKey(
                        name: "FK_Projeto_Usuario_IdUsuarioCadastro",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillUsuario",
                columns: table => new
                {
                    IdSkill = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUsuario", x => x.IdSkill);
                    table.ForeignKey(
                        name: "FK_SkillUsuario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProjeto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.IdAtividade);
                    table.ForeignKey(
                        name: "FK_Atividade_Projeto_IdProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtividade = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.IdTarefa);
                    table.ForeignKey(
                        name: "FK_Tarefa_Atividade_IdAtividade",
                        column: x => x.IdAtividade,
                        principalTable: "Atividade",
                        principalColumn: "IdAtividade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarefaUsuario",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaUsuario", x => new { x.IdTarefa, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_TarefaUsuario_Tarefa_IdTarefa",
                        column: x => x.IdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaUsuario_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_IdProjeto",
                table: "Atividade",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_IdModulo",
                table: "Menu",
                column: "IdModulo");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_IdUsuario",
                table: "Notificacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdUsuarioCadastro",
                table: "Projeto",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_SkillUsuario_IdUsuario",
                table: "SkillUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_IdAtividade",
                table: "Tarefa",
                column: "IdAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaUsuario_IdUsuario",
                table: "TarefaUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdUsuarioCadastro",
                table: "Usuario",
                column: "IdUsuarioCadastro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropTable(
                name: "SkillUsuario");

            migrationBuilder.DropTable(
                name: "TarefaUsuario");

            migrationBuilder.DropTable(
                name: "Modulo");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
