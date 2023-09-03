using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddUsuarioExclusao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioExclusao",
                table: "MensagemChat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChat_IdUsuarioExclusao",
                table: "MensagemChat",
                column: "IdUsuarioExclusao");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_Usuario_IdUsuarioExclusao",
                table: "MensagemChat",
                column: "IdUsuarioExclusao",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_Usuario_IdUsuarioExclusao",
                table: "MensagemChat");

            migrationBuilder.DropIndex(
                name: "IX_MensagemChat_IdUsuarioExclusao",
                table: "MensagemChat");

            migrationBuilder.DropColumn(
                name: "IdUsuarioExclusao",
                table: "MensagemChat");
        }
    }
}
