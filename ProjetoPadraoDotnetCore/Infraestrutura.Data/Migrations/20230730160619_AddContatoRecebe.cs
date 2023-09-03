using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddContatoRecebe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdContatoMandante",
                table: "MensagemChat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChat_IdContatoMandante",
                table: "MensagemChat",
                column: "IdContatoMandante");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_ContatoChat_IdContatoMandante",
                table: "MensagemChat",
                column: "IdContatoMandante",
                principalTable: "ContatoChat",
                principalColumn: "IdContatoChat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_ContatoChat_IdContatoMandante",
                table: "MensagemChat");

            migrationBuilder.DropIndex(
                name: "IX_MensagemChat_IdContatoMandante",
                table: "MensagemChat");

            migrationBuilder.DropColumn(
                name: "IdContatoMandante",
                table: "MensagemChat");
        }
    }
}
