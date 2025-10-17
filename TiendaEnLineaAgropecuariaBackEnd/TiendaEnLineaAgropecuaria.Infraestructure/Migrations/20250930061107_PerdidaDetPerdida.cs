using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class PerdidaDetPerdida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perdidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrecioCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perdidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perdida_Usuario",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Perdidas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perdidas_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetallesPerdida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerdidaId = table.Column<int>(type: "int", nullable: false),
                    InventarioId = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPerdida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesPerdida_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesPerdida_Perdidas_PerdidaId",
                        column: x => x.PerdidaId,
                        principalTable: "Perdidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesPerdida_UnidadesMedida_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPerdida_InventarioId",
                table: "DetallesPerdida",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPerdida_PerdidaId",
                table: "DetallesPerdida",
                column: "PerdidaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPerdida_UnidadMedidaId",
                table: "DetallesPerdida",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perdidas_EstadoId",
                table: "Perdidas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Perdidas_SucursalId",
                table: "Perdidas",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Perdidas_UserId",
                table: "Perdidas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPerdida");

            migrationBuilder.DropTable(
                name: "Perdidas");
        }
    }
}
