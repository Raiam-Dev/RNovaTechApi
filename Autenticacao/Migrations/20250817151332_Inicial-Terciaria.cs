using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RNovaTech.Migrations
{
    /// <inheritdoc />
    public partial class InicialTerciaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AppTarefas");

            migrationBuilder.RenameTable(
                name: "Tarefas",
                newName: "Tarefas",
                newSchema: "AppTarefas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Tarefas",
                schema: "AppTarefas",
                newName: "Tarefas");
        }
    }
}
