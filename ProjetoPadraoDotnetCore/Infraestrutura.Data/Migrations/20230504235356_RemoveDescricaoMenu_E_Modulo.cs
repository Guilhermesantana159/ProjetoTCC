using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class RemoveDescricaoMenu_E_Modulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescricaoLabel",
                table: "SubModulo");

            migrationBuilder.DropColumn(
                name: "DescricaoMenu",
                table: "Menu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoLabel",
                table: "SubModulo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescricaoMenu",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
