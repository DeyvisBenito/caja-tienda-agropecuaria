using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePagoTotVuelt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPago",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalVenta",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Vuelto",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPago",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "TotalVenta",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "Vuelto",
                table: "Pagos");
        }
    }
}
