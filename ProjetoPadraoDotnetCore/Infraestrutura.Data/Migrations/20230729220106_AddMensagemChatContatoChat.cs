using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddMensagemChatContatoChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdContatoRecebe",
                table: "MensagemChat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MensagemChat_IdContatoRecebe",
                table: "MensagemChat",
                column: "IdContatoRecebe");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemChat_ContatoChat_IdContatoRecebe",
                table: "MensagemChat",
                column: "IdContatoRecebe",
                principalTable: "ContatoChat",
                principalColumn: "IdContatoChat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagemChat_ContatoChat_IdContatoRecebe",
                table: "MensagemChat");

            migrationBuilder.DropIndex(
                name: "IX_MensagemChat_IdContatoRecebe",
                table: "MensagemChat");

            migrationBuilder.DropColumn(
                name: "IdContatoRecebe",
                table: "MensagemChat");
        }
    }
}
