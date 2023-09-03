using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class aAddModuloAndSubModulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Modulo_IdModulo",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "DescricaoLabel",
                table: "Modulo");

            migrationBuilder.DropColumn(
                name: "DescricaoModulo",
                table: "Modulo");

            migrationBuilder.DropColumn(
                name: "Icone",
                table: "Modulo");

            migrationBuilder.RenameColumn(
                name: "IdModulo",
                table: "Menu",
                newName: "IdSubModulo");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_IdModulo",
                table: "Menu",
                newName: "IX_Menu_IdSubModulo");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoMenu",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SubModulo",
                columns: table => new
                {
                    IdSubModulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdModulo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModulo", x => x.IdSubModulo);
                    table.ForeignKey(
                        name: "FK_SubModulo_Modulo_IdModulo",
                        column: x => x.IdModulo,
                        principalTable: "Modulo",
                        principalColumn: "IdModulo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubModulo_IdModulo",
                table: "SubModulo",
                column: "IdModulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_SubModulo_IdSubModulo",
                table: "Menu",
                column: "IdSubModulo",
                principalTable: "SubModulo",
                principalColumn: "IdSubModulo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_SubModulo_IdSubModulo",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "SubModulo");

            migrationBuilder.RenameColumn(
                name: "IdSubModulo",
                table: "Menu",
                newName: "IdModulo");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_IdSubModulo",
                table: "Menu",
                newName: "IX_Menu_IdModulo");

            migrationBuilder.AddColumn<string>(
                name: "DescricaoLabel",
                table: "Modulo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescricaoModulo",
                table: "Modulo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icone",
                table: "Modulo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoMenu",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Modulo_IdModulo",
                table: "Menu",
                column: "IdModulo",
                principalTable: "Modulo",
                principalColumn: "IdModulo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
