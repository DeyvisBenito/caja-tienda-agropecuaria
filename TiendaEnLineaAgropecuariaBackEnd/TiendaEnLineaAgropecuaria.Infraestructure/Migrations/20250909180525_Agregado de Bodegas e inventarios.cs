using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregadodeBodegaseinventarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_BodegaId",
                table: "Inventarios",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_EstadoId",
                table: "Inventarios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_TipoProductoId",
                table: "Inventarios",
                column: "TipoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProductos_CategoriaId",
                table: "TipoProductos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProductos_EstadoId",
                table: "TipoProductos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProductos_Nombre",
                table: "TipoProductos",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_EstadoId",
                table: "Productos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Nombre",
                table: "Productos",
                column: "Nombre",
                unique: true);
        }
    }
}
