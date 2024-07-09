using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class createtablecategoriasubcategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c1_Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c1_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "s1_SubCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_s1_SubCategorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categoria_SubCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategiriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_SubCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categoria_SubCategorias_c1_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "c1_Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categoria_SubCategorias_s1_SubCategorias_SubCategoriaId",
                        column: x => x.SubCategoriaId,
                        principalTable: "s1_SubCategorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoria_SubCategorias_categoriaId",
                table: "categoria_SubCategorias",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_categoria_SubCategorias_SubCategoriaId",
                table: "categoria_SubCategorias",
                column: "SubCategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoria_SubCategorias");

            migrationBuilder.DropTable(
                name: "c1_Categorias");

            migrationBuilder.DropTable(
                name: "s1_SubCategorias");
        }
    }
}
