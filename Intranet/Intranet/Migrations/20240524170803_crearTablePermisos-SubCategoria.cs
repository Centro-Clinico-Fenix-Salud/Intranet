using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class crearTablePermisosSubCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permisos_SubCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermisoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos_SubCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permisos_SubCategorias_p1_Permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "p1_Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permisos_SubCategorias_s1_SubCategorias_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "s1_SubCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permisos_SubCategorias_PermisoId",
                table: "permisos_SubCategorias",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_permisos_SubCategorias_SubCategoriaId",
                table: "permisos_SubCategorias",
                column: "SubCategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permisos_SubCategorias");
        }
    }
}
