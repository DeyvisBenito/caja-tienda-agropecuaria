using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCodSucIdForInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventarios_Codigo",
                table: "Inventarios");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_SucursalId",
                table: "Inventarios");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_SucursalId_Codigo",
                table: "Inventarios",
                columns: new[] { "SucursalId", "Codigo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventarios_SucursalId_Codigo",
                table: "Inventarios");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_Codigo",
                table: "Inventarios",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_SucursalId",
                table: "Inventarios",
                column: "SucursalId");
        }
    }
}
