using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class actualizandoForeignkeysCreadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_u1_Usuario_r1_Rol_R1_RolId",
            table: "u1_Usuario");

            migrationBuilder.AddForeignKey(
            name: "FK_u1_Usuario_r1_Rol_R1_RolId",
            table: "u1_Usuario",
            column: "R1_RolId",
            principalTable: "r1_Rol",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
