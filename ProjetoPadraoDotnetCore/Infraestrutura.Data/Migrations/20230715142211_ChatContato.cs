using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class ChatContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContatoChat",
                columns: table => new
                {
                    IdContatoChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioCadastro = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioContato = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusContato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoChat", x => x.IdContatoChat);
                    table.ForeignKey(
                        name: "FK_ContatoChat_Usuario_IdUsuarioCadastro",
                        column: x => x.IdUsuarioCadastro,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContatoChat_Usuario_IdUsuarioContato",
                        column: x => x.IdUsuarioContato,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContatoChat_IdUsuarioCadastro",
                table: "ContatoChat",
                column: "IdUsuarioCadastro");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoChat_IdUsuarioContato",
                table: "ContatoChat",
                column: "IdUsuarioContato");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContatoChat");
        }
    }
}
