using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;
using TiendaEnLineaAgropecuaria.Infraestructure.Servicios;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCompras
{
    public class RepositorioCompras : IRepositorioCompras
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioCompras(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Compra>> Get()
        {
            var compras = await dbContext.Compras
                                .Include(x => x.Sucursal)
                                .Include(x => x.Estado)
                                .Include(x => x.Proveedor).ThenInclude(p => p!.Estado)
                                .Include(x => x.DetallesCompra!).ThenInclude(d => d.Estado)
                                .Include(x => x.DetallesCompra!).ThenInclude(d => d.UnidadMedida)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Estado)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.TipoProducto)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Sucursal)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.UnidadMedida)
                                .ToListAsync();


            return compras;
        }

        public async Task<Compra> GetCompraPendiente(int sucursalId)
        {
            var estado = await dbContext.Estados.FirstOrDefaultAsync(x => x.Nombre == "Pendiente");
            if (estado is null)
            {
                throw new KeyNotFoundException("El estado Pendiente no esta disponible");
            }
            var compra = await dbContext.Compras.FirstOrDefaultAsync(x => x.EstadoId == estado.Id && x.SucursalId == sucursalId);

            return compra!;
        }

        public async Task<Compra> GetById(int id)
        {
            var compra = await dbContext.Compras
                                .Include(x => x.Sucursal)
                                .Include(x => x.Estado)
                                .Include(x => x.Proveedor).ThenInclude(p => p!.Estado)
                                .Include(x => x.DetallesCompra!).ThenInclude(d => d.Estado)
                                .Include(x => x.DetallesCompra!).ThenInclude(d => d.UnidadMedida)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Estado)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.TipoProducto)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Sucursal)
                                .Include(x => x.DetallesCompra!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.UnidadMedida)
                                .FirstOrDefaultAsync( x => x.Id == id);
            if (compra is null)
            {
                throw new KeyNotFoundException("La compra buscada no existe");
            }

            return compra;
        }

        public async Task<int> NewCompra(Compra compra, string proveedorNit)
        {
            var proveedorExist = await dbContext.Proveedores.FirstOrDefaultAsync(x => x.Nit == proveedorNit);
            if (proveedorExist is null)
            {
                throw new KeyNotFoundException("El proveedor con el Nit ingresado no esta registrado, consulte al administrador");
            }
            if(proveedorExist.EstadoId != (int)EstadosEnum.Activo)
            {
                throw new Exception("El proveedor con el Nit ingresado no esta activo, consulte al administrador");
            }

            // Va a venir del token del usuario
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == compra.SucursalId);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal seleccionado no existe");
            }

            // Estado se coloca en logica
            var estado = await dbContext.Estados.FirstOrDefaultAsync(x => x.Nombre == "Pendiente");
            if (estado is null)
            {
                throw new KeyNotFoundException("El estado Pendiente no esta disponible");
            }
            compra.EstadoId = estado.Id;
            compra.ProveedorId = proveedorExist.Id;
            dbContext.Compras.Add(compra);
            await dbContext.SaveChangesAsync();

            return compra.Id;
        }

        public async Task<bool> Update(int id, Compra compra, string nuevoNit)
        {
            var compraUpdate = await dbContext.Compras.FirstOrDefaultAsync(x => x.Id == id);
            if (compraUpdate is null)
            {
                throw new KeyNotFoundException("La compra a actualizar no existe");
            }

            var proveedorExist = await dbContext.Proveedores.FirstOrDefaultAsync(x => x.Nit == nuevoNit);
            if (proveedorExist is null)
            {
                throw new KeyNotFoundException("El proveedor seleccionado no existe");
            }

            if (proveedorExist.EstadoId != (int)EstadosEnum.Activo)
            {
                throw new KeyNotFoundException("El proveedor seleccionado no esta activo, consulte al administrador");
            }

            // Va a venir del token del usuario
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == compra.SucursalId);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal seleccionado no existe");
            }

            compraUpdate.ProveedorId = proveedorExist.Id;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var compraDelete = await dbContext.Compras.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId != (int)EstadosEnum.Eliminado);
            if (compraDelete is null)
            {
                throw new KeyNotFoundException("La compra a eliminar no existe");
            }
            var estadoEliminado = await dbContext.Estados.FirstOrDefaultAsync(e => e.Id == (int)EstadosEnum.Eliminado);

            if (estadoEliminado is null)
            {
                throw new InvalidOperationException("El estado 'Eliminado' no existe en la base de datos.");
            }

            // Eliminado logico, aun existe el registro en la base de datos
            compraDelete.EstadoId = estadoEliminado.Id;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelCompra(int id, int sucursalId)
        {

            var compraDelete = await dbContext.Compras.FirstOrDefaultAsync(x => x.Id == id && 
                                                x.EstadoId == (int)EstadosEnum.Pendiente && x.SucursalId == sucursalId);
            if (compraDelete is null)
            {
                throw new KeyNotFoundException("La compra a cancelar no existe");
            }

            await dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_Cancel_Compra @idCompra = {id}");
            return true;
        }

        // Post de confirmar compra
        public async Task<bool> ProcesarCompra(int idCompra, int idSucursal)
        {
            var compraExist = await dbContext.Compras.FirstOrDefaultAsync(x => x.Id == idCompra);
            if (compraExist is null)
            {
                throw new KeyNotFoundException("La compra a confirmar no existe");
            }
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == idSucursal);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal a usar no existe");
            }

            var detCompra = await dbContext.DetallesCompra.Where(x => x.CompraId == idCompra)
                        .Include(x => x.Inventario).ThenInclude(x => x!.TipoProducto).ThenInclude(x => x!.TipoMedida)
                        .ToListAsync();
            if (detCompra is null)
            {
                throw new KeyNotFoundException("No puede confirmar una compra sin productos");
            }

            // Validaciones antes de insertar
            foreach (var det in detCompra)
            {
                var inventExist = await dbContext.Inventarios.AnyAsync(x => x.Id == det.InventarioId);
                if (!inventExist)
                {
                    throw new KeyNotFoundException($"El detalle con el producto {det.Inventario!.Nombre} lastimosamente el producto no existe");
                }
                var unidadMExist = await dbContext.UnidadesMedida.AnyAsync(x => x.Id == det.UnidadMedidaId);
                if (!unidadMExist)
                {
                    throw new KeyNotFoundException($"La unidad de medida del detalle del producto {det.Inventario!.Nombre} no existe");
                }
                if (det.Cantidad <= 0)
                {
                    throw new Exception($"La cantidad del detalle del producto {det.Inventario!.Nombre} debe ser mayor a 0");
                }
                if (det.PrecioCosto <= 0)
                {
                    throw new Exception($"El precio del detalle del producto {det.Inventario!.Nombre} debe ser mayor a 0");
                }
                if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.caja)
                {
                    if (det.UnidadesPorCaja <= 0 || det.UnidadesPorCaja is null)
                    {
                        throw new Exception($"Las unidades por caja del detalle del producto {det.Inventario!.Nombre} debe ser mayor a 0");
                    }
                }
            }

            // Volviendo todo a unidades minimas e insertandolos a inventario con una transaccion
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var det in detCompra)
                {
                    // Variable para almacenar la unidad minima de los detalles
                    var cantidadEnUnMinima = 0;
                    // Variable para almacenar el precio costo por minima de los detalles
                    decimal precioCostoPorUnidMinima = 0;
                    // Variable para almacenar el precio venta por unidad minima de los detalles
                    decimal precioVentaPorUniMinima = 0;
                    var inventario = det.Inventario;
                    if (det.Inventario!.TipoProducto!.TipoMedidaId == (int)TipoMedidaEnum.masa)
                    {
                        if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.libra)
                        {
                            cantidadEnUnMinima = det.Cantidad;
                            precioCostoPorUnidMinima = det.PrecioCosto;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (det.Cantidad * det.PrecioCosto))
                                        / (inventario.Stock + det.Cantidad);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.arroba)
                        {
                            // Conversion de arroba a libra
                            var conversionDeArroba = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.libra);
                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeArroba!.Equivalencia);
                            precioCostoPorUnidMinima = det.PrecioCosto / conversionDeArroba!.Equivalencia;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (cantidadEnUnMinima * precioCostoPorUnidMinima))
                                        / (inventario.Stock + cantidadEnUnMinima);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto / conversionDeArroba!.Equivalencia;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.quintal)
                        {
                            // Conversion de quintal a libra
                            var conversionDeQuintal = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.libra);
                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeQuintal!.Equivalencia);
                            precioCostoPorUnidMinima = det.PrecioCosto / conversionDeQuintal!.Equivalencia;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (cantidadEnUnMinima * precioCostoPorUnidMinima))
                                        / (inventario.Stock + cantidadEnUnMinima);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto / conversionDeQuintal!.Equivalencia;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else
                        {
                            throw new KeyNotFoundException($"La medida del detalle del producto {det.Inventario!.Nombre} no existe");
                        }
                    }
                    else if (det.Inventario!.TipoProducto!.TipoMedidaId == (int)TipoMedidaEnum.conteo)
                    {
                        if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.unidad)
                        {
                            cantidadEnUnMinima = det.Cantidad;
                            precioCostoPorUnidMinima = det.PrecioCosto;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (det.Cantidad * det.PrecioCosto))
                                        / (inventario.Stock + det.Cantidad);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.docena)
                        {
                            // Conversion de docena a unidad
                            var conversionDeDocena = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.unidad);
                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeDocena!.Equivalencia);
                            precioCostoPorUnidMinima = det.PrecioCosto / conversionDeDocena!.Equivalencia;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (cantidadEnUnMinima * precioCostoPorUnidMinima))
                                        / (inventario.Stock + cantidadEnUnMinima);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto / conversionDeDocena!.Equivalencia;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.caja)
                        {
                            // Conversion de caja a unidades
                            cantidadEnUnMinima = (int)(det.Cantidad * det.UnidadesPorCaja!);
                            precioCostoPorUnidMinima = det.PrecioCosto / det.UnidadesPorCaja!.Value;
                            precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                            if (inventario!.PrecioCostoPromedio != 0 && inventario.PrecioCostoPromedio != precioCostoPorUnidMinima)
                            {
                                if (inventario.Stock > 0)
                                {
                                    precioCostoPorUnidMinima =
                                        ((inventario.Stock * inventario.PrecioCostoPromedio) + (cantidadEnUnMinima * precioCostoPorUnidMinima))
                                        / (inventario.Stock + cantidadEnUnMinima);
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                                else
                                {
                                    precioCostoPorUnidMinima = det.PrecioCosto / det.UnidadesPorCaja!.Value;
                                    precioVentaPorUniMinima = precioCostoPorUnidMinima + (precioCostoPorUnidMinima * 0.1m);
                                }
                            }
                        }
                        else
                        {
                            throw new KeyNotFoundException($"La medida del detalle del producto {det.Inventario!.Nombre} no existe");
                        }
                    }
                    else
                    {
                        throw new KeyNotFoundException($"El tipo de medida del detalle del producto {det.Inventario!.Nombre} no existe");
                    }

                    await dbContext.Database
                          .ExecuteSqlInterpolatedAsync($@"EXEC SP_Procesar_Compra @idCompra={idCompra}, @idSucursal={idSucursal},
                           @idDetalle = {det.Id}, @precioVenta = {precioVentaPorUniMinima}, @cantidad={cantidadEnUnMinima}, 
                           @precioCosto = {precioCostoPorUnidMinima}");
                }
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Ha ocurrido un error al procesar la compra");
            }
            compraExist.EstadoId = (int)EstadosEnum.Finalizado;
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
