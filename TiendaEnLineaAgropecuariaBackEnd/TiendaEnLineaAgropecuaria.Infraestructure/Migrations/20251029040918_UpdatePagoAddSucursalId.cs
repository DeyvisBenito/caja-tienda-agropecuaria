using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePagoAddSucursalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_SucursalId",
                table: "Pagos",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Sucursales_SucursalId",
                table: "Pagos",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Sucursales_SucursalId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_SucursalId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Pagos");
        }
    }
}
