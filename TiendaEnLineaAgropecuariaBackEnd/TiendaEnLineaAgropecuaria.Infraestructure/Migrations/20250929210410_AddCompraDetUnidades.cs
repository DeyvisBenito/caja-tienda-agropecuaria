using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompraDetUnidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Inventarios",
                newName: "PrecioVenta");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Inventarios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCostoPromedio",
                table: "Inventarios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnidadMedidaId",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_Usuario",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medida = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    InventarioId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesCompra_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesCompra_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallesCompra_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesCompra_UnidadesMedida_UnidadMedidaId",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Codigo",
                table: "Inventarios",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_UnidadMedidaId",
                table: "Inventarios",
                column: "UnidadMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalles_InventarioId",
                table: "CarritoDetalles",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EstadoId",
                table: "Compras",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdUser",
                table: "Compras",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_CompraId",
                table: "DetallesCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_EstadoId",
                table: "DetallesCompra",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_InventarioId",
                table: "DetallesCompra",
                column: "InventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_UnidadMedidaId",
                table: "DetallesCompra",
                column: "UnidadMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoDetalles_Inventarios_InventarioId",
                table: "CarritoDetalles",
                column: "InventarioId",
                principalTable: "Inventarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_UnidadesMedida_UnidadMedidaId",
                table: "Inventarios",
                column: "UnidadMedidaId",
                principalTable: "UnidadesMedida",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoDetalles_Inventarios_InventarioId",
                table: "CarritoDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_UnidadesMedida_UnidadMedidaId",
                table: "Inventarios");

            migrationBuilder.DropTable(
                name: "DetallesCompra");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_Codigo",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_UnidadMedidaId",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_CarritoDetalles_InventarioId",
                table: "CarritoDetalles");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "PrecioCostoPromedio",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "UnidadMedidaId",
                table: "Inventarios");

            migrationBuilder.RenameColumn(
                name: "PrecioVenta",
                table: "Inventarios",
                newName: "Precio");
        }
    }
}
