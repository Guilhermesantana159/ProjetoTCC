using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class ConfigProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlteracaoStatusProjetoNotificar",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlteracaoTarefasProjetoNotificar",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailProjetoAtrasado",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailTarefaAtrasada",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PortalProjetoAtrasado",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PortalTarefaAtrasada",
                table: "Projeto",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlteracaoStatusProjetoNotificar",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "AlteracaoTarefasProjetoNotificar",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "EmailProjetoAtrasado",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "EmailTarefaAtrasada",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "PortalProjetoAtrasado",
                table: "Projeto");

            migrationBuilder.DropColumn(
                name: "PortalTarefaAtrasada",
                table: "Projeto");
        }
    }
}
