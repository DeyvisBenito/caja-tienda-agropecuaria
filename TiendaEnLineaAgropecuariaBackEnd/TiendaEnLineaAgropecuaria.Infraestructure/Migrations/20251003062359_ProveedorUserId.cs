using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ProveedorUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Proveedores",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_UserId",
                table: "Proveedores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedor_Usuario",
                table: "Proveedores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedor_Usuario",
                table: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Proveedores_UserId",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Proveedores");
        }
    }
}
