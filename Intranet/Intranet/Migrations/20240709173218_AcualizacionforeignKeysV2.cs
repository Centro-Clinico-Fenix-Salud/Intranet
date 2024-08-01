using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class AcualizacionforeignKeysV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_categoria_SubCategorias_c1_Categorias_CategoriaId",
               table: "categoria_SubCategorias");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_SubCategorias_c1_Categorias_categoriaId",
                table: "categoria_SubCategorias",
                column: "CategoriaId",
                principalTable: "c1_Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
               name: "FK_categoria_SubCategorias_s1_SubCategorias_SubCategoriaId",
               table: "categoria_SubCategorias");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_SubCategorias_s1_SubCategorias_SubCategoriaId",
                table: "categoria_SubCategorias",
                column: "SubCategoriaId",
                principalTable: "s1_SubCategorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
            name: "FK_rol_Permiso_p1_Permiso_P1_PermisoId",
            table: "rol_Permiso");

            migrationBuilder.AddForeignKey(
                name: "FK_rol_Permiso_p1_Permiso_P1_PermisoId",
                table: "rol_Permiso",
                column: "P1_PermisoId",
                principalTable: "p1_Permiso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_permisos_SubCategorias_p1_Permiso_PermisoId",
                table: "permisos_SubCategorias");

            migrationBuilder.DropForeignKey(
            name: "FK_permisos_SubCategorias_s1_SubCategorias_SubCategoriaId",
            table: "permisos_SubCategorias");

            migrationBuilder.AddForeignKey(
               name: "FK_permisos_SubCategorias_p1_Permiso_PermisoId",
               table: "permisos_SubCategorias",
               column: "PermisoId",
               principalTable: "p1_Permiso",
               principalColumn: "Id",
               onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
               name: "FK_permisos_SubCategorias_s1_SubCategorias_SubCategoriaId",
               table: "permisos_SubCategorias",
               column: "SubCategoriaId",
               principalTable: "s1_SubCategorias",
               principalColumn: "Id",
               onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
