using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddMensagemChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MensagemChat",
                columns: table => new
                {
                    IdMensagemChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioMandante = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioRecebe = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplayMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusMessage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemChat", x => x.IdMensagemChat);
                    table.ForeignKey(
                        name: "FK_MensagemChat_Usuario_IdUsuarioMandante",
                        column: x => x.IdUsuarioMandante,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "FK_MensagemChat_Usuario_IdUsuarioRecebe",
                        column: x => x.IdUsuarioRecebe,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChat_IdUsuarioMandante",
                table: "MensagemChat",
                column: "IdUsuarioMandante");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChat_IdUsuarioRecebe",
                table: "MensagemChat",
                column: "IdUsuarioRecebe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensagemChat");
        }
    }
}
