using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class TarefaTemplateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarefaTemplate_AtividadeTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate");

            migrationBuilder.DropIndex(
                name: "IX_TarefaTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate");

            migrationBuilder.DropColumn(
                name: "AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TarefaTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate",
                column: "AtividadeTemplateIdAtvidadeTemplate1");

            migrationBuilder.AddForeignKey(
                name: "FK_TarefaTemplate_AtividadeTemplate_AtividadeTemplateIdAtvidadeTemplate1",
                table: "TarefaTemplate",
                column: "AtividadeTemplateIdAtvidadeTemplate1",
                principalTable: "AtividadeTemplate",
                principalColumn: "IdAtvidadeTemplate");
        }
    }
}
