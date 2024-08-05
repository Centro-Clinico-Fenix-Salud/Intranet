using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class crearForeignKeyAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_agendaTelefonicas_UbicacionId",
                table: "agendaTelefonicas",
                column: "UbicacionId");           

            migrationBuilder.CreateIndex(
                name: "IX_agendaTelefonicas_UnidadId",
                table: "agendaTelefonicas",
                column: "UnidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_agendaTelefonicas_ubicaciones_UbicacionId",
                table: "agendaTelefonicas",
                column: "UbicacionId",
                principalTable: "ubicaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_agendaTelefonicas_unidades_UnidadId",
                table: "agendaTelefonicas",
                column: "UnidadId",
                principalTable: "unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_agendaTelefonicas_ubicaciones_UbicacionId",
                table: "agendaTelefonicas");

            migrationBuilder.DropForeignKey(
                name: "FK_agendaTelefonicas_unidades_UnidadId",
                table: "agendaTelefonicas");

            migrationBuilder.DropForeignKey(
                name: "FK_agendaTelefonicas_u1_Usuario_Usuario",
                table: "agendaTelefonicas");

            migrationBuilder.DropIndex(
                name: "IX_agendaTelefonicas_UbicacionId",
                table: "agendaTelefonicas");

            migrationBuilder.DropIndex(
                name: "IX_agendaTelefonicas_UnidadId",
                table: "agendaTelefonicas");

        }
    }
}
