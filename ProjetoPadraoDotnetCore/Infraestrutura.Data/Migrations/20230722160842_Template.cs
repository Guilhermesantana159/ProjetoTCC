using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaTemplate",
                columns: table => new
                {
                    IdCategoriaTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaTemplate", x => x.IdCategoriaTemplate);
                    table.ForeignKey(
                        name: "FK_CategoriaTemplate_Usuario_IdUsuarioCadastro",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    IdTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escala = table.Column<int>(type: "int", nullable: false),
                    QuantidadeTotal = table.Column<int>(type: "int", nullable: false),
                    IdTemplateCategoria = table.Column<int>(type: "int", nullable: true),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.IdTemplate);
                    table.ForeignKey(
                        name: "FK_Template_CategoriaTemplate_IdTemplateCategoria",
                        column: x => x.IdTemplateCategoria,
                        principalTable: "CategoriaTemplate",
                        principalColumn: "IdCategoriaTemplate",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Template_Usuario_IdUsuarioCadastro",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeTemplate",
                columns: table => new
                {
                    IdAtvidadeTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTemplate = table.Column<int>(type: "int", nullable: false),
                    TempoPrevisto = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Posicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeTemplate", x => x.IdAtvidadeTemplate);
                    table.ForeignKey(
                        name: "FK_AtividadeTemplate_Template_IdTemplate",
                        column: x => x.IdTemplate,
                        principalTable: "Template",
                        principalColumn: "IdTemplate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TarefaTemplate",
                columns: table => new
                {
                    IdTarefaTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAtividadeTemplate = table.Column<int>(type: "int", nullable: false),
                    AtividadeTemplateIdAtvidadeTemplate1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarefaTemplate", x => x.IdTarefaTemplate);
                    table.ForeignKey(
                        name: "FK_TarefaTemplate_AtividadeTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                        column: x => x.AtividadeTemplateIdAtvidadeTemplate1,
                        principalTable: "AtividadeTemplate",
                        principalColumn: "IdAtvidadeTemplate");
                    table.ForeignKey(
                        name: "FK_TarefaTemplate_AtividadeTemplate_IdAtividadeTemplate",
                        column: x => x.IdAtividadeTemplate,
                        principalTable: "AtividadeTemplate",
                        principalColumn: "IdAtvidadeTemplate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeTemplate_IdTemplate",
                table: "AtividadeTemplate",
                column: "IdTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaTemplate_IdUsuarioCadastro",
                table: "CategoriaTemplate",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate",
                column: "AtividadeTemplateIdAtvidadeTemplate1");

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTemplate_IdAtividadeTemplate",
                table: "TarefaTemplate",
                column: "IdAtividadeTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_Template_IdTemplateCategoria",
                table: "Template",
                column: "IdTemplateCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Template_IdUsuarioCadastro",
                table: "Template",
                column: "IdUsuarioCadastro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarefaTemplate");

            migrationBuilder.DropTable(
                name: "AtividadeTemplate");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "CategoriaTemplate");
        }
    }
}
