using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.Persistence.Migrations
{
    public partial class UsersIsP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEventos",
                table: "UserEventos");

            migrationBuilder.DropIndex(
                name: "IX_UserEventos_EventoId",
                table: "UserEventos");

            migrationBuilder.RenameColumn(
                name: "IsPalestrante",
                table: "Users",
                newName: "IsPalest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEventos",
                table: "UserEventos",
                columns: new[] { "EventoId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserEventos_UserId",
                table: "UserEventos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEventos",
                table: "UserEventos");

            migrationBuilder.DropIndex(
                name: "IX_UserEventos_UserId",
                table: "UserEventos");

            migrationBuilder.RenameColumn(
                name: "IsPalest",
                table: "Users",
                newName: "IsPalestrante");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEventos",
                table: "UserEventos",
                columns: new[] { "UserId", "EventoId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserEventos_EventoId",
                table: "UserEventos",
                column: "EventoId");
        }
    }
}
