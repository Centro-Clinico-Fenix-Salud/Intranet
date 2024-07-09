using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Migrations
{
    /// <inheritdoc />
    public partial class modificacionTablaRolYUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "RolId",
                table: "u1_Usuario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario",
                column: "RolId",
                principalTable: "r1_Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "RolId",
                table: "u1_Usuario",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_u1_Usuario_r1_Rol_RolId",
                table: "u1_Usuario",
                column: "RolId",
                principalTable: "r1_Rol",
                principalColumn: "Id");
        }
    }
}
