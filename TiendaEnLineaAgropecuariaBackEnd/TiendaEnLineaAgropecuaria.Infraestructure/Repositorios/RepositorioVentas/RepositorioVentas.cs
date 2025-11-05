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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioVentas
{
    public class RepositorioVentas : IRepositorioVentas
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioVentas(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Venta>> Get()
        {
            var ventas = await dbContext.Ventas
                                .Include(x => x.Sucursal)
                                .Include(x => x.Estado)
                                .Include(x => x.Cliente)
                                .Include(x => x.DetallesVenta!).ThenInclude(d => d.Estado)
                                .Include(x => x.DetallesVenta!).ThenInclude(d => d.UnidadMedida)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Estado)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.TipoProducto)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Sucursal)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.UnidadMedida)
                                .OrderByDescending(x => x.Id)
                                .ToListAsync();


            return ventas;
        }


        public async Task<Venta> GetById(int id)
        {
            var venta = await dbContext.Ventas
                                .Include(x => x.Sucursal)
                                .Include(x => x.Estado)
                                .Include(x => x.Cliente)
                                .Include(x => x.DetallesVenta!).ThenInclude(d => d.Estado)
                                .Include(x => x.DetallesVenta!).ThenInclude(d => d.UnidadMedida)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Estado)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.TipoProducto)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.Sucursal)
                                .Include(x => x.DetallesVenta!)
                                    .ThenInclude(d => d.Inventario)
                                        .ThenInclude(i => i!.UnidadMedida)
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (venta is null)
            {
                throw new KeyNotFoundException("La venta buscada no existe");
            }

            return venta;
        }

        public async Task<int> NewVenta(Venta venta, string clienteNit)
        {
            var clienteExist = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Nit.Equals(clienteNit));
            if (clienteExist is null)
            {
                throw new KeyNotFoundException("El Cliente con el Nit ingresado no esta registrado");
            }
            // Va a venir del token del usuario
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == venta.SucursalId);
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
            venta.EstadoId = estado.Id;
            venta.ClienteId = clienteExist.Id;
            dbContext.Ventas.Add(venta);
            await dbContext.SaveChangesAsync();

            return venta.Id;
        }


        public async Task<bool> Update(int id, Venta venta, string nuevoNit)
        {
            var ventaUpdate = await dbContext.Ventas.FirstOrDefaultAsync(x => x.Id == id);
            if (ventaUpdate is null)
            {
                throw new KeyNotFoundException("La venta a actualizar no existe");
            }

            var clienteExist = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Nit.Equals(nuevoNit));
            if (clienteExist is null)
            {
                throw new KeyNotFoundException("El cliente seleccionado no existe");
            }

            // Va a venir del token del usuario
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == venta.SucursalId);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal seleccionado no existe");
            }

            ventaUpdate.ClienteId = clienteExist.Id;

            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<bool> Delete(int id)
        {
            var ventaDelete = await dbContext.Ventas.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId != (int)EstadosEnum.Eliminado);
            if (ventaDelete is null)
            {
                throw new KeyNotFoundException("La venta a eliminar no existe");
            }
            var estadoEliminado = await dbContext.Estados.FirstOrDefaultAsync(e => e.Id == (int)EstadosEnum.Eliminado);

            if (estadoEliminado is null)
            {
                throw new InvalidOperationException("El estado 'Eliminado' no existe en la base de datos.");
            }
            if (ventaDelete.EstadoId == (int)EstadosEnum.Pendiente)
            {
                var detallesVenta = await dbContext.DetallesVenta.Where(x => x.VentaId == ventaDelete.Id).ToListAsync();
                dbContext.RemoveRange(detallesVenta);
                dbContext.Remove(ventaDelete);
                await dbContext.SaveChangesAsync();
                return true;
            }

            // Eliminado logico, aun existe el registro en la base de datos
            ventaDelete.EstadoId = estadoEliminado.Id;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelVenta(int id, int sucursalId)
        {

            var ventaDelete = await dbContext.Ventas.FirstOrDefaultAsync(x => x.Id == id &&
                                                x.EstadoId == (int)EstadosEnum.Pendiente && x.SucursalId == sucursalId);
            if (ventaDelete is null)
            {
                throw new KeyNotFoundException("La venta a cancelar no existe");
            }

            await dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC SP_Cancel_Venta @idVenta = {id}");
            return true;
        }

        // Post de confirmar venta
        public async Task<decimal> ProcesarVenta(int idVenta, int idSucursal, string userId, int tipoPagoId, decimal pago)
        {
            var sucursalExist = await dbContext.Sucursales.AnyAsync(x => x.Id == idSucursal);
            if (!sucursalExist)
            {
                throw new KeyNotFoundException("La sucursal a usar no existe");
            }
            var ventaExist = await dbContext.Ventas.FirstOrDefaultAsync(x => x.Id == idVenta && x.SucursalId == idSucursal
                                                                        && x.EstadoId == (int)EstadosEnum.Pendiente);
            if (ventaExist is null)
            {
                throw new KeyNotFoundException("La venta a confirmar no existe");
            }
            

            var detVenta = await dbContext.DetallesVenta.Where(x => x.VentaId == idVenta)
                        .Include(x => x.Inventario).ThenInclude(x => x!.TipoProducto).ThenInclude(x => x!.TipoMedida)
                        .ToListAsync();
            if (detVenta is null || detVenta.Count <= 0)
            {
                throw new KeyNotFoundException("No puede confirmar una venta sin productos");
            }

            // Validaciones antes de vender
            foreach (var det in detVenta)
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
                if (det.PrecioVentaUnidadMinima <= 0)
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

            // Volviendo todo a unidades minimas y restandolos a inventario con una transaccion
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var acumuladoPorInventario = new Dictionary<int, int>(); 
                var cantidadMinimaPorDetalle = new Dictionary<int, int>();

                foreach (var det in detVenta)
                {
                    // Variable para restar la unidad minima de los detalles
                    var cantidadEnUnMinima = 0;
                    var inventario = det.Inventario;
                    if (det.Inventario!.TipoProducto!.TipoMedidaId == (int)TipoMedidaEnum.masa)
                    {
                        if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.libra)
                        {
                            cantidadEnUnMinima = det.Cantidad;
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.arroba)
                        {
                            // Conversion de arroba a libra
                            var conversionDeArroba = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.libra);

                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeArroba!.Equivalencia);
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.quintal)
                        {
                            // Conversion de quintal a libra
                            var conversionDeQuintal = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.libra);

                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeQuintal!.Equivalencia);
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
                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.docena)
                        {
                            // Conversion de docena a unidad
                            var conversionDeDocena = await dbContext.Conversiones
                                .FirstOrDefaultAsync(x => x.UnidadMedidaOrigenId == det.UnidadMedidaId
                                    && x.UnidadMedidaDestinoId == (int)UnidadesMedidaEnum.unidad);

                            cantidadEnUnMinima = (int)(det.Cantidad * conversionDeDocena!.Equivalencia);

                        }
                        else if (det.UnidadMedidaId == (int)UnidadesMedidaEnum.caja)
                        {
                            // Conversion de caja a unidades
                            cantidadEnUnMinima = (int)(det.Cantidad * det.UnidadesPorCaja!);
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

                    if (cantidadEnUnMinima > det.Inventario.Stock)
                    {
                        throw new InvalidOperationException($"No existe suficiente stock del producto {det.Inventario.Nombre}");
                    }
                    // Cantidad minima por detalle
                    cantidadMinimaPorDetalle[det.Id] = cantidadEnUnMinima;

                    // Acumular por inventario
                    if (acumuladoPorInventario.ContainsKey(det.InventarioId))
                        acumuladoPorInventario[det.InventarioId] += cantidadEnUnMinima;
                    else
                        acumuladoPorInventario[det.InventarioId] = cantidadEnUnMinima;

                   
                }

                // Paso 2️: validar stock total por inventario
                foreach (var item in acumuladoPorInventario)
                {
                    var inventario = await dbContext.Inventarios.FindAsync(item.Key);
                    if (item.Value > inventario!.Stock)
                        throw new InvalidOperationException($"No existe suficiente stock del producto {inventario.Nombre}");
                }

                decimal total = 0;
                // Paso 3️: procesar cada detalle (ya validado)
                foreach (var det in detVenta)
                {
                    var cantidadEnUnMinima = cantidadMinimaPorDetalle[det.Id];
                    total = total + det.Total;
                    await dbContext.Database
                         .ExecuteSqlInterpolatedAsync($@"EXEC SP_Procesar_Venta @idVenta={idVenta}, @idDetalle = {det.Id},
                           @idSucursal={idSucursal}, @cantidad={cantidadEnUnMinima}");
                }

                if(pago < total)
                {
                    throw new InvalidOperationException("Saldo insuficiente");
                }

                var pagoTot = new Pago
                {
                    UserId = userId,
                    TipoPagoId = tipoPagoId,
                    VentaId = idVenta,
                    SucursalId = idSucursal,
                    TotalVenta = total,
                    TotalPago = pago,
                    Vuelto = pago - total
                };
                dbContext.Pagos.Add(pagoTot);
                ventaExist.Total = total;
                ventaExist.EstadoId = (int)EstadosEnum.Finalizado;
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return pagoTot.Vuelto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new Exception("Ha ocurrido un error al procesar la venta");
            }      
        }
    }
}
