using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddFkUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdUsuarioCadastro",
                table: "Usuario",
                column: "IdUsuarioCadastro");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Usuario_IdUsuarioCadastro",
                table: "Usuario",
                column: "IdUsuarioCadastro",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Usuario_IdUsuarioCadastro",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdUsuarioCadastro",
                table: "Usuario");
        }
    }
}
