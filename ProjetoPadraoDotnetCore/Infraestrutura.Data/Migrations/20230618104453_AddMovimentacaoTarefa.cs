using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddMovimentacaoTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentacaoTarefa",
                columns: table => new
                {
                    IdMovimentacaoTarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTarefa = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioMovimentacao = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false),
                    TempoUtilizadoUltimaColuna = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoTarefa", x => x.IdMovimentacaoTarefa);
                    table.ForeignKey(
                        name: "FK_MovimentacaoTarefa_Tarefa_IdTarefa",
                        column: x => x.IdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa");
                    table.ForeignKey(
                        name: "FK_MovimentacaoTarefa_Usuario_IdUsuarioMovimentacao",
                        column: x => x.IdUsuarioMovimentacao,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoTarefa_IdTarefa",
                table: "MovimentacaoTarefa",
                column: "IdTarefa");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoTarefa_IdUsuarioMovimentacao",
                table: "MovimentacaoTarefa",
                column: "IdUsuarioMovimentacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacaoTarefa");
        }
    }
}
