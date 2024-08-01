using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablePermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "p1_Permiso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_p1_Permiso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rol_Permiso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol_Permiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rol_Permiso_p1_Permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "p1_Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rol_Permiso_r1_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "r1_Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rol_Permiso_PermisoId",
                table: "rol_Permiso",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_rol_Permiso_RolId",
                table: "rol_Permiso",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rol_Permiso");

            migrationBuilder.DropTable(
                name: "p1_Permiso");
        }
    }
}
