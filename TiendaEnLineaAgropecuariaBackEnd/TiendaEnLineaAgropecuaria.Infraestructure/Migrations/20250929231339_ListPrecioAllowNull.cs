using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ListPrecioAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversiones_ListaPrecios_ListaPrecioId",
                table: "Conversiones");

            migrationBuilder.AlterColumn<int>(
                name: "ListaPrecioId",
                table: "Conversiones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversiones_ListaPrecios_ListaPrecioId",
                table: "Conversiones",
                column: "ListaPrecioId",
                principalTable: "ListaPrecios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversiones_ListaPrecios_ListaPrecioId",
                table: "Conversiones");

            migrationBuilder.AlterColumn<int>(
                name: "ListaPrecioId",
                table: "Conversiones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversiones_ListaPrecios_ListaPrecioId",
                table: "Conversiones",
                column: "ListaPrecioId",
                principalTable: "ListaPrecios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
