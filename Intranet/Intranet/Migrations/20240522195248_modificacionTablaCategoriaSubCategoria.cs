using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class modificacionTablaCategoriaSubCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoria_SubCategorias_c1_Categorias_categoriaId",
                table: "categoria_SubCategorias");

            migrationBuilder.DropColumn(
                name: "CategiriaId",
                table: "categoria_SubCategorias");

            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "categoria_SubCategorias",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_categoria_SubCategorias_categoriaId",
                table: "categoria_SubCategorias",
                newName: "IX_categoria_SubCategorias_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_SubCategorias_c1_Categorias_CategoriaId",
                table: "categoria_SubCategorias",
                column: "CategoriaId",
                principalTable: "c1_Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoria_SubCategorias_c1_Categorias_CategoriaId",
                table: "categoria_SubCategorias");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "categoria_SubCategorias",
                newName: "categoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_categoria_SubCategorias_CategoriaId",
                table: "categoria_SubCategorias",
                newName: "IX_categoria_SubCategorias_categoriaId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategiriaId",
                table: "categoria_SubCategorias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_categoria_SubCategorias_c1_Categorias_categoriaId",
                table: "categoria_SubCategorias",
                column: "categoriaId",
                principalTable: "c1_Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
