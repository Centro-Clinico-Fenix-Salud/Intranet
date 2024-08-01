using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class createTableNoticiasYArchivosNoticias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "archivosNoticias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoticiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreFisico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivosNoticias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "noticias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TituloNoticia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextoNoticia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCreador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdModificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaNoticia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Concurrencia = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_noticias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivosNoticias");

            migrationBuilder.DropTable(
                name: "noticias");
        }
    }
}
