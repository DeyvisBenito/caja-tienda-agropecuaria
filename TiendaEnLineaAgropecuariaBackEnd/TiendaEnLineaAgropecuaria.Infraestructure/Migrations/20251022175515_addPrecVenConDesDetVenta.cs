using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class addPrecVenConDesDetVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVentaConDescuento",
                table: "DetallesVenta",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioVentaConDescuento",
                table: "DetallesVenta");
        }
    }
}
