using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoAndUnPorCajaDetVent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "DetallesVenta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "DetallesVenta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UnidadesPorCaja",
                table: "DetallesVenta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesVenta_EstadoId",
                table: "DetallesVenta",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesVenta_Estados_EstadoId",
                table: "DetallesVenta",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesVenta_Estados_EstadoId",
                table: "DetallesVenta");

            migrationBuilder.DropIndex(
                name: "IX_DetallesVenta_EstadoId",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "UnidadesPorCaja",
                table: "DetallesVenta");
        }
    }
}
