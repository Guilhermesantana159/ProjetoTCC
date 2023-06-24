using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddComentarioTarefaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "ComentarioTarefa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioTarefa_IdUsuario",
                table: "ComentarioTarefa",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ComentarioTarefa_Usuario_IdUsuario",
                table: "ComentarioTarefa",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComentarioTarefa_Usuario_IdUsuario",
                table: "ComentarioTarefa");

            migrationBuilder.DropIndex(
                name: "IX_ComentarioTarefa_IdUsuario",
                table: "ComentarioTarefa");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "ComentarioTarefa");
        }
    }
}
