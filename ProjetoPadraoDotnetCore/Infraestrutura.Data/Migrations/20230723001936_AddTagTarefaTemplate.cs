using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddTagTarefaTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTarefa_TarefaTemplate_IdTarefaTemplate",
                table: "TagTarefa");

            migrationBuilder.DropIndex(
                name: "IX_TagTarefa_IdTarefaTemplate",
                table: "TagTarefa");

            migrationBuilder.DropColumn(
                name: "IdTarefaTemplate",
                table: "TagTarefa");

            migrationBuilder.AlterColumn<int>(
                name: "IdTarefa",
                table: "TagTarefa",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TagTarefaTemplate",
                columns: table => new
                {
                    IdTagTarefaTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTarefaTemplate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTarefaTemplate", x => x.IdTagTarefaTemplate);
                    table.ForeignKey(
                        name: "FK_TagTarefaTemplate_TarefaTemplate_IdTarefaTemplate",
                        column: x => x.IdTarefaTemplate,
                        principalTable: "TarefaTemplate",
                        principalColumn: "IdTarefaTemplate");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTarefaTemplate_IdTarefaTemplate",
                table: "TagTarefaTemplate",
                column: "IdTarefaTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTarefaTemplate");

            migrationBuilder.AlterColumn<int>(
                name: "IdTarefa",
                table: "TagTarefa",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTarefaTemplate",
                table: "TagTarefa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TagTarefa_IdTarefaTemplate",
                table: "TagTarefa",
                column: "IdTarefaTemplate");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTarefa_TarefaTemplate_IdTarefaTemplate",
                table: "TagTarefa",
                column: "IdTarefaTemplate",
                principalTable: "TarefaTemplate",
                principalColumn: "IdTarefaTemplate");
        }
    }
}
