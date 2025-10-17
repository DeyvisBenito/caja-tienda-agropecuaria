using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class TiposMedidaIdNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoProductos_TiposMedida_TipoMedidaId",
                table: "TipoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesMedida_TiposMedida_TipoMedidaId",
                table: "UnidadesMedida");

            migrationBuilder.AlterColumn<int>(
                name: "TipoMedidaId",
                table: "UnidadesMedida",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoMedidaId",
                table: "TipoProductos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TipoProductos_TiposMedida_TipoMedidaId",
                table: "TipoProductos",
                column: "TipoMedidaId",
                principalTable: "TiposMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesMedida_TiposMedida_TipoMedidaId",
                table: "UnidadesMedida",
                column: "TipoMedidaId",
                principalTable: "TiposMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "TipoMedidaId",
                table: "UnidadesMedida",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoMedidaId",
                table: "TipoProductos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
