using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddColumnFotoProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Projeto",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Projeto");
        }
    }
}
