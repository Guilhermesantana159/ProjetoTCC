using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class NewCamposTarefasAndTableTagsTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoTarefa",
                table: "Tarefa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prioridade",
                table: "Tarefa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TagTarefa",
                columns: table => new
                {
                    IdTagTarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTarefa", x => x.IdTagTarefa);
                    table.ForeignKey(
                        name: "FK_TagTarefa_Tarefa_IdTarefa",
                        column: x => x.IdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTarefa_IdTarefa",
                table: "TagTarefa",
                column: "IdTarefa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTarefa");

            migrationBuilder.DropColumn(
                name: "DescricaoTarefa",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "Prioridade",
                table: "Tarefa");
        }
    }
}
