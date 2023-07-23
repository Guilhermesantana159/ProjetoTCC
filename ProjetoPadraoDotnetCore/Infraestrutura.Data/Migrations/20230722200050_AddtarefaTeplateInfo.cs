using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddtarefaTeplateInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoTarefa",
                table: "TarefaTemplate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "TarefaTemplate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTarefaTemplate",
                table: "TagTarefa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TagTarefa_IdTarefaTemplate",
                table: "TagTarefa",
                column: "IdTarefaTemplate");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTarefa_TarefaTemplate_IdTarefaTemplate",
                table: "TagTarefa",
                column: "IdTarefaTemplate",
                principalTable: "TarefaTemplate",
                principalColumn: "IdTarefaTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTarefa_TarefaTemplate_IdTarefaTemplate",
                table: "TagTarefa");

            migrationBuilder.DropIndex(
                name: "IX_TagTarefa_IdTarefaTemplate",
                table: "TagTarefa");

            migrationBuilder.DropColumn(
                name: "DescricaoTarefa",
                table: "TarefaTemplate");

            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "TarefaTemplate");

            migrationBuilder.DropColumn(
                name: "IdTarefaTemplate",
                table: "TagTarefa");
        }
    }
}
