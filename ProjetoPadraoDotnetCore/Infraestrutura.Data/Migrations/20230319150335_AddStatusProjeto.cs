using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddStatusProjeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projeto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projeto");
        }
    }
}
