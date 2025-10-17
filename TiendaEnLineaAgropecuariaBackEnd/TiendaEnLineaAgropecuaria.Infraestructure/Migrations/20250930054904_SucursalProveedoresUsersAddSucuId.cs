using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaEnLineaAgropecuaria.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SucursalProveedoresUsersAddSucuId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Bodegas");

            migrationBuilder.RenameColumn(
                name: "BodegaId",
                table: "Inventarios",
                newName: "SucursalId");

            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nit = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursal_Usuario",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sucursales_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_EstadoId",
                table: "Ventas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_SucursalId",
                table: "Inventarios",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProveedorId",
                table: "Compras",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SucursalId",
                table: "AspNetUsers",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_Nit",
                table: "Proveedores",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_EstadoId",
                table: "Sucursales",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_UserId",
                table: "Sucursales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sucursales_SucursalId",
                table: "AspNetUsers",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Sucursales_SucursalId",
                table: "Inventarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Estados_EstadoId",
                table: "Ventas",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sucursales_SucursalId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProveedorId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Sucursales_SucursalId",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Estados_EstadoId",
                table: "Ventas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_EstadoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Compras_ProveedorId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SucursalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SucursalId",
                table: "Inventarios",
                newName: "BodegaId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventarios_SucursalId",
                table: "Inventarios",
                newName: "IX_Inventarios_BodegaId");

            migrationBuilder.CreateTable(
                name: "Bodegas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodegas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodegas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodegas_EstadoId",
                table: "Bodegas",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Bodegas_BodegaId",
                table: "Inventarios",
                column: "BodegaId",
                principalTable: "Bodegas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
