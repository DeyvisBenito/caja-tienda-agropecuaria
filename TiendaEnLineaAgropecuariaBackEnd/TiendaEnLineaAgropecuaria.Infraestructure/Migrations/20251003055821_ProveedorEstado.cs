using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ProveedorEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Proveedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_EstadoId",
                table: "Proveedores",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proveedores_Estados_EstadoId",
                table: "Proveedores",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proveedores_Estados_EstadoId",
                table: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Proveedores_EstadoId",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Proveedores");
        }
    }
}
