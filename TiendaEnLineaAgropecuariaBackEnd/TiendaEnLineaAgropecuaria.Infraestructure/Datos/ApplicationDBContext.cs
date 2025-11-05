using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Datos
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Producto.Nombre único
            modelBuilder.Entity<TipoProducto>()
                .HasIndex(p => p.Nombre)
                .IsUnique();

            // Categoria.Nombre único
            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            // Codigo unico de inventario en cada sucursal
            modelBuilder.Entity<Inventario>()
                .HasIndex(i => new { i.SucursalId, i.Codigo })
                .IsUnique(); ;

            // Foreign key de sucursal a User
            modelBuilder.Entity<Sucursal>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Sucursal_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de compra a User
            modelBuilder.Entity<Compra>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.IdUser)
                .HasConstraintName("FK_Compra_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreigns key de conversiones a UnidadMedida
            modelBuilder.Entity<Conversion>()
                   .HasOne(c => c.UnidadMedidaOrigen)
                   .WithMany()
                   .HasForeignKey(c => c.UnidadMedidaOrigenId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversion>()
                   .HasOne(c => c.UnidadMedidaDestino)
                   .WithMany()
                   .HasForeignKey(c => c.UnidadMedidaDestinoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de movimiento a User
            modelBuilder.Entity<Movimiento>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Movimiento_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Nit unico de cliente
            modelBuilder.Entity<Cliente>()
                .HasIndex(i => i.Nit)
                .IsUnique();

            // Foreign key de venta a User
            modelBuilder.Entity<Venta>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Venta_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de Proveedor a User
            modelBuilder.Entity<Proveedor>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Proveedor_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Nit unico de proveedor
            modelBuilder.Entity<Proveedor>()
                .HasIndex(i => i.Nit)
                .IsUnique();

            // Foreign key de perdida a User
            modelBuilder.Entity<Perdida>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Perdida_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de Traslado a User
            modelBuilder.Entity<Traslado>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Traslado_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreigns key de Traslado a Sucursales
            modelBuilder.Entity<Traslado>()
                   .HasOne(c => c.SucursalOrigen)
                   .WithMany()
                   .HasForeignKey(c => c.SucursalOrigenId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Traslado>()
                   .HasOne(c => c.SucursalDestino)
                   .WithMany()
                   .HasForeignKey(c => c.SucursalDestinoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de Cliente a User
            modelBuilder.Entity<Cliente>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Cliente_Usuario")
                .OnDelete(DeleteBehavior.Restrict);

            // Foreign key de Pago a User
            modelBuilder.Entity<Pago>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .HasConstraintName("FK_Pago_Usuario")
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<TipoProducto> TipoProductos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalles { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<ListaPrecio> ListaPrecios { get; set; }
        public DbSet<Conversion> Conversiones { get; set; }
        public DbSet<TipoMovimiento> TiposMovimiento { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<DetalleMovimiento> DetallesMovimiento { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Perdida> Perdidas { get; set; }
        public DbSet<DetallePerdida> DetallesPerdida { get; set; }
        public DbSet<MovimientoCompra> MovimientosCompra { get; set; }
        public DbSet<MovimientoVenta> MovimientosVenta { get; set; }
        public DbSet<MovimientoPerdida> MovimientosPerdida { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<DetalleTraslado> DetallesTraslado { get; set; }
        public DbSet<MovimientoTraslado> MovimientosTraslado { get; set; }
        public DbSet<TipoMedida> TiposMedida { get; set; }
        public DbSet<TipoPago> TipoPagos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
    }
}
