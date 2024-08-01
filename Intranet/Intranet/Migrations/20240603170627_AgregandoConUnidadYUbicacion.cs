using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoConUnidadYUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unidad",
                table: "agendaTelefonicas",
                newName: "UnidadId");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "agendaTelefonicas",
                newName: "UbicacionId");

            migrationBuilder.CreateTable(
                name: "ubicaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidades", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ubicaciones");

            migrationBuilder.DropTable(
                name: "unidades");

            migrationBuilder.RenameColumn(
                name: "UnidadId",
                table: "agendaTelefonicas",
                newName: "Unidad");

            migrationBuilder.RenameColumn(
                name: "UbicacionId",
                table: "agendaTelefonicas",
                newName: "Ubicacion");
        }
    }
}
