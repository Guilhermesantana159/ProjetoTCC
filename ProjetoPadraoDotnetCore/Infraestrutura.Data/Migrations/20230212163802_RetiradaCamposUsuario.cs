using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class RetiradaCamposUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Profissao_IdProfissao",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Profissao");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_IdProfissao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Dedicacao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdProfissao",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Rg",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dedicacao",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProfissao",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rg",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profissao",
                columns: table => new
                {
                    IdProfissao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissao", x => x.IdProfissao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdProfissao",
                table: "Usuario",
                column: "IdProfissao");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Profissao_IdProfissao",
                table: "Usuario",
                column: "IdProfissao",
                principalTable: "Profissao",
                principalColumn: "IdProfissao");
        }
    }
}
