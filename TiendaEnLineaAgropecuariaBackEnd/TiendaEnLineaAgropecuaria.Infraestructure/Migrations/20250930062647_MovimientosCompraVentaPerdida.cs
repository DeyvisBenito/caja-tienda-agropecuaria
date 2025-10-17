using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class MovimientosCompraVentaPerdida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimientosCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosCompra_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosCompra_Movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "Movimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosPerdida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    PerdidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosPerdida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosPerdida_Movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "Movimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosPerdida_Perdidas_PerdidaId",
                        column: x => x.PerdidaId,
                        principalTable: "Perdidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosVenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    VentaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosVenta_Movimientos_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "Movimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosVenta_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCompra_CompraId",
                table: "MovimientosCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCompra_MovimientoId",
                table: "MovimientosCompra",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosPerdida_MovimientoId",
                table: "MovimientosPerdida",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosPerdida_PerdidaId",
                table: "MovimientosPerdida",
                column: "PerdidaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosVenta_MovimientoId",
                table: "MovimientosVenta",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosVenta_VentaId",
                table: "MovimientosVenta",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosCompra");

            migrationBuilder.DropTable(
                name: "MovimientosPerdida");

            migrationBuilder.DropTable(
                name: "MovimientosVenta");
        }
    }
}
