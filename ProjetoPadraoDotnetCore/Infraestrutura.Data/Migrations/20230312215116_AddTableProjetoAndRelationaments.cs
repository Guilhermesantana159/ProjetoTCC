using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddTableProjetoAndRelationaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividade",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.IdAtividade);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeTarefa",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(type: "int", nullable: false),
                    IdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeTarefa", x => new { x.IdAtividade, x.IdTarefa });
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
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ProjetoAtividade",
                columns: table => new
                {
                    IdProjeto = table.Column<int>(type: "int", nullable: false),
                    IdAtividade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoAtividade", x => new { x.IdAtividade, x.IdProjeto });
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.IdTarefa);
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
                    table.PrimaryKey("PK_TarefaUsuario", x => new { x.IdUsuario, x.IdTarefa });
                });

            migrationBuilder.CreateTable(
                name: "AtividadeAtividadeTarefa",
                columns: table => new
                {
                    AtividadeIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LAtividadeTarefaIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LAtividadeTarefaIdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeAtividadeTarefa", x => new { x.AtividadeIdAtividade, x.LAtividadeTarefaIdAtividade, x.LAtividadeTarefaIdTarefa });
                    table.ForeignKey(
                        name: "FK_AtividadeAtividadeTarefa_Atividade_AtividadeIdAtividade",
                        column: x => x.AtividadeIdAtividade,
                        principalTable: "Atividade",
                        principalColumn: "IdAtividade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtividadeAtividadeTarefa_AtividadeTarefa_LAtividadeTarefaIdAtividade_LAtividadeTarefaIdTarefa",
                        columns: x => new { x.LAtividadeTarefaIdAtividade, x.LAtividadeTarefaIdTarefa },
                        principalTable: "AtividadeTarefa",
                        principalColumns: new[] { "IdAtividade", "IdTarefa" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeProjetoAtividade",
                columns: table => new
                {
                    AtividadeIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LProjetoAtividadeIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LProjetoAtividadeIdProjeto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeProjetoAtividade", x => new { x.AtividadeIdAtividade, x.LProjetoAtividadeIdAtividade, x.LProjetoAtividadeIdProjeto });
                    table.ForeignKey(
                        name: "FK_AtividadeProjetoAtividade_Atividade_AtividadeIdAtividade",
                        column: x => x.AtividadeIdAtividade,
                        principalTable: "Atividade",
                        principalColumn: "IdAtividade",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtividadeProjetoAtividade_ProjetoAtividade_LProjetoAtividadeIdAtividade_LProjetoAtividadeIdProjeto",
                        columns: x => new { x.LProjetoAtividadeIdAtividade, x.LProjetoAtividadeIdProjeto },
                        principalTable: "ProjetoAtividade",
                        principalColumns: new[] { "IdAtividade", "IdProjeto" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjetoProjetoAtividade",
                columns: table => new
                {
                    ProjetoIdProjeto = table.Column<int>(type: "int", nullable: false),
                    LProjetoAtividadeIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LProjetoAtividadeIdProjeto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoProjetoAtividade", x => new { x.ProjetoIdProjeto, x.LProjetoAtividadeIdAtividade, x.LProjetoAtividadeIdProjeto });
                    table.ForeignKey(
                        name: "FK_ProjetoProjetoAtividade_Projeto_ProjetoIdProjeto",
                        column: x => x.ProjetoIdProjeto,
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoProjetoAtividade_ProjetoAtividade_LProjetoAtividadeIdAtividade_LProjetoAtividadeIdProjeto",
                        columns: x => new { x.LProjetoAtividadeIdAtividade, x.LProjetoAtividadeIdProjeto },
                        principalTable: "ProjetoAtividade",
                        principalColumns: new[] { "IdAtividade", "IdProjeto" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeTarefaTarefa",
                columns: table => new
                {
                    TarefaIdTarefa = table.Column<int>(type: "int", nullable: false),
                    LAtividadeTarefaIdAtividade = table.Column<int>(type: "int", nullable: false),
                    LAtividadeTarefaIdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeTarefaTarefa", x => new { x.TarefaIdTarefa, x.LAtividadeTarefaIdAtividade, x.LAtividadeTarefaIdTarefa });
                    table.ForeignKey(
                        name: "FK_AtividadeTarefaTarefa_AtividadeTarefa_LAtividadeTarefaIdAtividade_LAtividadeTarefaIdTarefa",
                        columns: x => new { x.LAtividadeTarefaIdAtividade, x.LAtividadeTarefaIdTarefa },
                        principalTable: "AtividadeTarefa",
                        principalColumns: new[] { "IdAtividade", "IdTarefa" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtividadeTarefaTarefa_Tarefa_TarefaIdTarefa",
                        column: x => x.TarefaIdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarefaTarefaUsuario",
                columns: table => new
                {
                    TarefaIdTarefa = table.Column<int>(type: "int", nullable: false),
                    LTarefaUsuarioIdUsuario = table.Column<int>(type: "int", nullable: false),
                    LTarefaUsuarioIdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaTarefaUsuario", x => new { x.TarefaIdTarefa, x.LTarefaUsuarioIdUsuario, x.LTarefaUsuarioIdTarefa });
                    table.ForeignKey(
                        name: "FK_TarefaTarefaUsuario_Tarefa_TarefaIdTarefa",
                        column: x => x.TarefaIdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaTarefaUsuario_TarefaUsuario_LTarefaUsuarioIdUsuario_LTarefaUsuarioIdTarefa",
                        columns: x => new { x.LTarefaUsuarioIdUsuario, x.LTarefaUsuarioIdTarefa },
                        principalTable: "TarefaUsuario",
                        principalColumns: new[] { "IdUsuario", "IdTarefa" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TarefaUsuarioUsuario",
                columns: table => new
                {
                    UsuarioIdUsuario = table.Column<int>(type: "int", nullable: false),
                    LTarefaUsuarioIdUsuario = table.Column<int>(type: "int", nullable: false),
                    LTarefaUsuarioIdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaUsuarioUsuario", x => new { x.UsuarioIdUsuario, x.LTarefaUsuarioIdUsuario, x.LTarefaUsuarioIdTarefa });
                    table.ForeignKey(
                        name: "FK_TarefaUsuarioUsuario_TarefaUsuario_LTarefaUsuarioIdUsuario_LTarefaUsuarioIdTarefa",
                        columns: x => new { x.LTarefaUsuarioIdUsuario, x.LTarefaUsuarioIdTarefa },
                        principalTable: "TarefaUsuario",
                        principalColumns: new[] { "IdUsuario", "IdTarefa" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TarefaUsuarioUsuario_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeAtividadeTarefa_LAtividadeTarefaIdAtividade_LAtividadeTarefaIdTarefa",
                table: "AtividadeAtividadeTarefa",
                columns: new[] { "LAtividadeTarefaIdAtividade", "LAtividadeTarefaIdTarefa" });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeProjetoAtividade_LProjetoAtividadeIdAtividade_LProjetoAtividadeIdProjeto",
                table: "AtividadeProjetoAtividade",
                columns: new[] { "LProjetoAtividadeIdAtividade", "LProjetoAtividadeIdProjeto" });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeTarefaTarefa_LAtividadeTarefaIdAtividade_LAtividadeTarefaIdTarefa",
                table: "AtividadeTarefaTarefa",
                columns: new[] { "LAtividadeTarefaIdAtividade", "LAtividadeTarefaIdTarefa" });

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdUsuarioCadastro",
                table: "Projeto",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoProjetoAtividade_LProjetoAtividadeIdAtividade_LProjetoAtividadeIdProjeto",
                table: "ProjetoProjetoAtividade",
                columns: new[] { "LProjetoAtividadeIdAtividade", "LProjetoAtividadeIdProjeto" });

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTarefaUsuario_LTarefaUsuarioIdUsuario_LTarefaUsuarioIdTarefa",
                table: "TarefaTarefaUsuario",
                columns: new[] { "LTarefaUsuarioIdUsuario", "LTarefaUsuarioIdTarefa" });

            migrationBuilder.CreateIndex(
                name: "IX_TarefaUsuarioUsuario_LTarefaUsuarioIdUsuario_LTarefaUsuarioIdTarefa",
                table: "TarefaUsuarioUsuario",
                columns: new[] { "LTarefaUsuarioIdUsuario", "LTarefaUsuarioIdTarefa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadeAtividadeTarefa");

            migrationBuilder.DropTable(
                name: "AtividadeProjetoAtividade");

            migrationBuilder.DropTable(
                name: "AtividadeTarefaTarefa");

            migrationBuilder.DropTable(
                name: "ProjetoProjetoAtividade");

            migrationBuilder.DropTable(
                name: "TarefaTarefaUsuario");

            migrationBuilder.DropTable(
                name: "TarefaUsuarioUsuario");

            migrationBuilder.DropTable(
                name: "Atividade");

            migrationBuilder.DropTable(
                name: "AtividadeTarefa");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "ProjetoAtividade");

            migrationBuilder.DropTable(
                name: "Tarefa");

            migrationBuilder.DropTable(
                name: "TarefaUsuario");
        }
    }
}
