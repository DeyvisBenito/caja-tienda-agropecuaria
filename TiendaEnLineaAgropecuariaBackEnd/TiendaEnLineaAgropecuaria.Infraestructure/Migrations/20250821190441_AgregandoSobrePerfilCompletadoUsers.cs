using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoSobrePerfilCompletadoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PerfilCompletado",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerfilCompletado",
                table: "AspNetUsers");
        }
    }
}
