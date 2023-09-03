using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class ADD_FK_TAREFA_IN_TAG_TAREFA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTarefa_Tarefa_IdTarefa",
                table: "TagTarefa");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTarefa_Tarefa_IdTarefa",
                table: "TagTarefa",
                column: "IdTarefa",
                principalTable: "Tarefa",
                principalColumn: "IdTarefa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTarefa_Tarefa_IdTarefa",
                table: "TagTarefa");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTarefa_Tarefa_IdTarefa",
                table: "TagTarefa",
                column: "IdTarefa",
                principalTable: "Tarefa",
                principalColumn: "IdTarefa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
