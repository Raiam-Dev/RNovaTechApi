using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RNovaTech.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoAutenticacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AplicacaoDeTarefas");

            migrationBuilder.EnsureSchema(
                name: "Autenticacao");

            migrationBuilder.RenameTable(
                name: "Tarefas",
                schema: "AppTarefas",
                newName: "Tarefas",
                newSchema: "AplicacaoDeTarefas");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Autenticacao",
                columns: table => new
                {
                    Uid = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Uid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas",
                column: "UsuarioUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas",
                column: "UsuarioUid",
                principalSchema: "Autenticacao",
                principalTable: "Usuarios",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Autenticacao");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "UsuarioUid",
                schema: "AplicacaoDeTarefas",
                table: "Tarefas");

            migrationBuilder.EnsureSchema(
                name: "AppTarefas");

            migrationBuilder.RenameTable(
                name: "Tarefas",
                schema: "AplicacaoDeTarefas",
                newName: "Tarefas",
                newSchema: "AppTarefas");
        }
    }
}
