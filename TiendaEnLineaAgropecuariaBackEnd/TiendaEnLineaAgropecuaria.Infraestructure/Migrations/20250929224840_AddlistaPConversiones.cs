using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddlistaPConversiones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListaPrecios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPrecios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadMedidaOrigenId = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaDestinoId = table.Column<int>(type: "int", nullable: false),
                    ListaPrecioId = table.Column<int>(type: "int", nullable: false),
                    Equivalencia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversiones_ListaPrecios_ListaPrecioId",
                        column: x => x.ListaPrecioId,
                        principalTable: "ListaPrecios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversiones_UnidadesMedida_UnidadMedidaDestinoId",
                        column: x => x.UnidadMedidaDestinoId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversiones_UnidadesMedida_UnidadMedidaOrigenId",
                        column: x => x.UnidadMedidaOrigenId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversiones_ListaPrecioId",
                table: "Conversiones",
                column: "ListaPrecioId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversiones_UnidadMedidaDestinoId",
                table: "Conversiones",
                column: "UnidadMedidaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversiones_UnidadMedidaOrigenId",
                table: "Conversiones",
                column: "UnidadMedidaOrigenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversiones");

            migrationBuilder.DropTable(
                name: "ListaPrecios");
        }
    }
}
