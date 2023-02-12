using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddColumnsRecSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoRecuperarSenha",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRecuperacaoSenha",
                table: "Usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TentativasRecuperarSenha",
                table: "Usuario",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoRecuperarSenha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "DataRecuperacaoSenha",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TentativasRecuperarSenha",
                table: "Usuario");
        }
    }
}
