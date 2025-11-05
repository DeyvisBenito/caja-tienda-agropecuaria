using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPrecVentaYDescDetVen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioVentaConDescuento",
                table: "DetallesVenta",
                newName: "PrecioVentaConDescuentoUnidadMinima");

            migrationBuilder.RenameColumn(
                name: "PrecioVenta",
                table: "DetallesVenta",
                newName: "PrecioVentaUnidadMinima");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioVentaUnidadMinima",
                table: "DetallesVenta",
                newName: "PrecioVenta");

            migrationBuilder.RenameColumn(
                name: "PrecioVentaConDescuentoUnidadMinima",
                table: "DetallesVenta",
                newName: "PrecioVentaConDescuento");
        }
    }
}
