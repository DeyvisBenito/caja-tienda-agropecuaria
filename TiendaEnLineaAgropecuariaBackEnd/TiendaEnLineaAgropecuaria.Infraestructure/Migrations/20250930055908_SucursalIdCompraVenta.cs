using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SucursalIdCompraVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_SucursalId",
                table: "Ventas",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_SucursalId",
                table: "Compras",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Sucursales_SucursalId",
                table: "Compras",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Sucursales_SucursalId",
                table: "Ventas",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Sucursales_SucursalId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Sucursales_SucursalId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_SucursalId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Compras_SucursalId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Compras");
        }
    }
}
