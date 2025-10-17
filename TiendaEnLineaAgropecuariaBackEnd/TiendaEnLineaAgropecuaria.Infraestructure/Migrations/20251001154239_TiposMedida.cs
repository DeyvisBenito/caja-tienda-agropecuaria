using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class TiposMedida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoMedidaId",
                table: "UnidadesMedida",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMedidaId",
                table: "TipoProductos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TiposMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMedida", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMedida_TipoMedidaId",
                table: "UnidadesMedida",
                column: "TipoMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoProductos_TipoMedidaId",
                table: "TipoProductos",
                column: "TipoMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoProductos_TiposMedida_TipoMedidaId",
                table: "TipoProductos",
                column: "TipoMedidaId",
                principalTable: "TiposMedida",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesMedida_TiposMedida_TipoMedidaId",
                table: "UnidadesMedida",
                column: "TipoMedidaId",
                principalTable: "TiposMedida",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoProductos_TiposMedida_TipoMedidaId",
                table: "TipoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesMedida_TiposMedida_TipoMedidaId",
                table: "UnidadesMedida");

            migrationBuilder.DropTable(
                name: "TiposMedida");

            migrationBuilder.DropIndex(
                name: "IX_UnidadesMedida_TipoMedidaId",
                table: "UnidadesMedida");

            migrationBuilder.DropIndex(
                name: "IX_TipoProductos_TipoMedidaId",
                table: "TipoProductos");

            migrationBuilder.DropColumn(
                name: "TipoMedidaId",
                table: "UnidadesMedida");

            migrationBuilder.DropColumn(
                name: "TipoMedidaId",
                table: "TipoProductos");
        }
    }
}
