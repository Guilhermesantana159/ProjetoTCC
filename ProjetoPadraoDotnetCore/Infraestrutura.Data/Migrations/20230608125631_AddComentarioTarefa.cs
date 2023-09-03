using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.Migrations
{
    public partial class AddComentarioTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComentarioTarefa",
                columns: table => new
                {
                    IdComentarioTarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioTarefa", x => x.IdComentarioTarefa);
                    table.ForeignKey(
                        name: "FK_ComentarioTarefa_Tarefa_IdTarefa",
                        column: x => x.IdTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "IdTarefa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioTarefa_IdTarefa",
                table: "ComentarioTarefa",
                column: "IdTarefa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioTarefa");
        }
    }
}
