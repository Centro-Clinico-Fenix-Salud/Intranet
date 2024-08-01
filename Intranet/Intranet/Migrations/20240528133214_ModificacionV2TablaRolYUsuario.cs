using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionV2TablaRolYUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "u1_Usuario",
                newName: "R1_RolId");

            migrationBuilder.RenameIndex(
                name: "IX_u1_Usuario_RolId",
                table: "u1_Usuario",
                newName: "IX_u1_Usuario_R1_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_u1_Usuario_r1_Rol_R1_RolId",
                table: "u1_Usuario",
                column: "R1_RolId",
                principalTable: "r1_Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_u1_Usuario_r1_Rol_R1_RolId",
                table: "u1_Usuario");

            migrationBuilder.RenameColumn(
                name: "R1_RolId",
                table: "u1_Usuario",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_u1_Usuario_R1_RolId",
                table: "u1_Usuario",
                newName: "IX_u1_Usuario_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario",
                column: "RolId",
                principalTable: "r1_Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
