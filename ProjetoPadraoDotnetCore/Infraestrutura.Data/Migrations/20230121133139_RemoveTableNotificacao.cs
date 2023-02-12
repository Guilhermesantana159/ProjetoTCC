using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class RemoveTableNotificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificacao_TipoNotificacao_IdTipoNotificacao",
                table: "Notificacao");

            migrationBuilder.DropTable(
                name: "TipoNotificacao");

            migrationBuilder.DropIndex(
                name: "IX_Notificacao_IdTipoNotificacao",
                table: "Notificacao");

            migrationBuilder.RenameColumn(
                name: "IdTipoNotificacao",
                table: "Notificacao",
                newName: "ClassficacaoMensagem");

            migrationBuilder.AddColumn<string>(
                name: "Corpo",
                table: "Notificacao",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Notificacao",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corpo",
                table: "Notificacao");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Notificacao");

            migrationBuilder.RenameColumn(
                name: "ClassficacaoMensagem",
                table: "Notificacao",
                newName: "IdTipoNotificacao");

            migrationBuilder.CreateTable(
                name: "TipoNotificacao",
                columns: table => new
                {
                    IdTipoNotificacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassficacaoMensagem = table.Column<int>(type: "int", nullable: false),
                    Corpo = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNotificacao", x => x.IdTipoNotificacao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_IdTipoNotificacao",
                table: "Notificacao",
                column: "IdTipoNotificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificacao_TipoNotificacao_IdTipoNotificacao",
                table: "Notificacao",
                column: "IdTipoNotificacao",
                principalTable: "TipoNotificacao",
                principalColumn: "IdTipoNotificacao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
