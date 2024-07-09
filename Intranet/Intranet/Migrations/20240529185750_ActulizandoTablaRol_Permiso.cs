using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class ActulizandoTablaRol_Permiso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rol_Permiso_p1_Permiso_PermisoId",
                table: "rol_Permiso");

            migrationBuilder.DropForeignKey(
                name: "FK_rol_Permiso_r1_Rol_RolId",
                table: "rol_Permiso");

            migrationBuilder.DropIndex(
                name: "IX_rol_Permiso_RolId",
                table: "rol_Permiso");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "rol_Permiso",
                newName: "R1_RolId");

            migrationBuilder.RenameColumn(
                name: "PermisoId",
                table: "rol_Permiso",
                newName: "P1_PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_rol_Permiso_PermisoId",
                table: "rol_Permiso",
                newName: "IX_rol_Permiso_P1_PermisoId");

            migrationBuilder.AddForeignKey(
                name: "FK_rol_Permiso_p1_Permiso_P1_PermisoId",
                table: "rol_Permiso",
                column: "P1_PermisoId",
                principalTable: "p1_Permiso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rol_Permiso_p1_Permiso_P1_PermisoId",
                table: "rol_Permiso");

            migrationBuilder.RenameColumn(
                name: "R1_RolId",
                table: "rol_Permiso",
                newName: "RolId");

            migrationBuilder.RenameColumn(
                name: "P1_PermisoId",
                table: "rol_Permiso",
                newName: "PermisoId");

            migrationBuilder.RenameIndex(
                name: "IX_rol_Permiso_P1_PermisoId",
                table: "rol_Permiso",
                newName: "IX_rol_Permiso_PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_rol_Permiso_RolId",
                table: "rol_Permiso",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_rol_Permiso_p1_Permiso_PermisoId",
                table: "rol_Permiso",
                column: "PermisoId",
                principalTable: "p1_Permiso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rol_Permiso_r1_Rol_RolId",
                table: "rol_Permiso",
                column: "RolId",
                principalTable: "r1_Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
