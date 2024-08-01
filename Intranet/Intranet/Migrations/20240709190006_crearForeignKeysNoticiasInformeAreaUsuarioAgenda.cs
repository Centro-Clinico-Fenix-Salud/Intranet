using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class crearForeignKeysNoticiasInformeAreaUsuarioAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "IdModificador",
                table: "noticias",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
               name: "FK_noticias_u1_Usuario_IdCreador",
               table: "noticias",
               column: "IdCreador",
               principalTable: "u1_Usuario",
               principalColumn: "Id",
               onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_noticias_u1_Usuario_IdModificador",
                table: "noticias",
                column: "IdModificador",
                principalTable: "u1_Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_informeArea_informeTitulo_InformeTituloId",
                table: "informeArea",
                column: "InformeTituloId",
                principalTable: "informeTitulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarioAgendaTelefonica_u1_Usuario_UsuarioModificador",
                table: "usuarioAgendaTelefonica",
                column: "UsuarioModificador",
                principalTable: "u1_Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "IdModificador",
                table: "noticias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
