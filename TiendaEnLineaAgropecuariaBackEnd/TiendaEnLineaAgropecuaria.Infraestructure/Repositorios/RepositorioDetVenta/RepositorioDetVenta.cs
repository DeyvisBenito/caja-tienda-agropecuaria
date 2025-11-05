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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioDetVenta
{
    public class RepositorioDetVenta : IRepositorioDetVenta
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioDetVenta(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
         
        public async Task<IEnumerable<DetalleVenta>> Get()
        {
            var detVenta = await dbContext.DetallesVenta.Include(x => x.Venta).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).ToListAsync();

            return detVenta;
        }
        
        public async Task<IEnumerable<DetalleVenta>> GetByVentaId(int ventaId, int sucursalId)
        {
            if (sucursalId != 0)
            {
                var ventaExist = await dbContext.Ventas.AnyAsync(x => x.Id == ventaId && x.SucursalId == sucursalId);
                if (!ventaExist)
                {
                    throw new KeyNotFoundException("La venta seleccionada no existe o no existe en la sucursal actual");
                }
            }
            else
            {
                var ventaExist = await dbContext.Ventas.AnyAsync(x => x.Id == ventaId);
                if (!ventaExist)
                {
                    throw new KeyNotFoundException("La venta seleccionada no existe");
                }
            }

            var detVenta = await dbContext.DetallesVenta.Include(x => x.Venta).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Inventario).ThenInclude(x => x.Estado)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).Where(x => x.VentaId == ventaId).ToListAsync();

            return detVenta;
        }
        
        public async Task<DetalleVenta?> GetByInvId(int ventaId, int invId, int unidadMedidaId)
        {
            var invExist = await dbContext.DetallesVenta.FirstOrDefaultAsync(x => x.VentaId == ventaId && x.InventarioId == invId
                                                                    && x.UnidadMedidaId == unidadMedidaId);

            return invExist;
        }

        public async Task<DetalleVenta?> GetByInvIdUpd(int ventaId, int invId, int unidadMedidaId, int detId)
        {
            var invExist = await dbContext.DetallesVenta.FirstOrDefaultAsync(x => x.VentaId == ventaId && x.InventarioId == invId
                                                                    && x.UnidadMedidaId == unidadMedidaId && x.Id != detId);

            return invExist;
        }

        public async Task<DetalleVenta> GetById(int ventaId, int idDet)
        {
            var detVenta = await dbContext.DetallesVenta.Include(x => x.Venta).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).FirstOrDefaultAsync(x => x.Id == idDet && x.VentaId == ventaId);

            if (detVenta is null)
            {
                throw new KeyNotFoundException("El detalle de la venta no existe");
            }

            return detVenta;
        }
        
        public async Task<bool> NewDetalle(DetalleVenta detalle, int sucursalId)
        {
            if (detalle.UnidadMedidaId != (int)UnidadesMedidaEnum.caja)
            {
                detalle.UnidadesPorCaja = null;
            }
            var ventaExist = await dbContext.Ventas.AnyAsync(x => x.Id == detalle.VentaId && x.SucursalId == sucursalId);
            if (!ventaExist)
            {
                throw new KeyNotFoundException("La venta seleccionada no existe o no existe en la sucursal actual");
            }

            var inventarioExist = await dbContext.Inventarios.AnyAsync(x => x.Id == detalle.InventarioId && x.SucursalId == sucursalId);
            if (!inventarioExist)
            {
                throw new KeyNotFoundException("El inventario seleccionado no existe o no existe en la sucursal actual");
            }

            var estadoExist = await dbContext.Estados.AnyAsync(x => x.Id == detalle.EstadoId);
            if (!estadoExist)
            {
                throw new KeyNotFoundException("El estado seleccionado no existe");
            }

            var unidadMedidaId = await dbContext.UnidadesMedida.AnyAsync(x => x.Id == detalle.UnidadMedidaId);
            if (!unidadMedidaId)
            {
                throw new KeyNotFoundException("La unidad de medida seleccionada no existe");
            }

            dbContext.DetallesVenta.Add(detalle);
            await dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> UpdateDetalle(DetalleVenta detalle, int id, int idVenta, int sucursalId)
        {
            if (detalle.UnidadMedidaId != (int)UnidadesMedidaEnum.caja)
            {
                detalle.UnidadesPorCaja = null;
            }
            var ventaExist = await dbContext.Ventas.AnyAsync(x => x.Id == detalle.VentaId && x.SucursalId == sucursalId);
            if (!ventaExist)
            {
                throw new KeyNotFoundException("La venta seleccionada no existe o no existe en la sucursal actual");
            }

            var inventarioExist = await dbContext.Inventarios.AnyAsync(x => x.Id == detalle.InventarioId && x.SucursalId == sucursalId);
            if (!inventarioExist)
            {
                throw new KeyNotFoundException("El inventario seleccionado no existe o no existe en la sucursal actual");
            }

            var estadoExist = await dbContext.Estados.AnyAsync(x => x.Id == detalle.EstadoId);
            if (!estadoExist)
            {
                throw new KeyNotFoundException("El estado seleccionado no existe");
            }

            var unidadMedidaId = await dbContext.UnidadesMedida.AnyAsync(x => x.Id == detalle.UnidadMedidaId);
            if (!unidadMedidaId)
            {
                throw new KeyNotFoundException("La unidad de medida seleccionada no existe");
            }

            var detalleExist = await dbContext.DetallesVenta.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId == (int)EstadosEnum.Activo);
            if (detalleExist is null)
            {
                throw new KeyNotFoundException("El detalle a editar no existe");
            }

            if (detalle.InventarioId != detalleExist.InventarioId)
            {
                throw new KeyNotFoundException("No puede cambiar de inventario");
            }


            detalleExist.UnidadMedidaId = detalle.UnidadMedidaId;
            detalleExist.Cantidad = detalle.Cantidad;
            detalleExist.UnidadesPorCaja = detalle.UnidadesPorCaja;
            detalleExist.Descuento = detalle.Descuento;
            detalleExist.PrecioVentaConDescuentoUnidadMinima = detalle.PrecioVentaConDescuentoUnidadMinima;
            detalleExist.Total = detalle.Total;
            detalleExist.PrecioVentaUnidadMinima = detalle.PrecioVentaUnidadMinima;
            await dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> Delete(int id, int ventaId, int sucursalId)
        {
            var ventaExist = await dbContext.Ventas.AnyAsync(x => x.Id == ventaId && x.SucursalId == sucursalId);
            if (!ventaExist)
            {
                throw new KeyNotFoundException("La venta no existe o no pertenece a la sucursal actual");
            }
            var detExist = await dbContext.DetallesVenta.FirstOrDefaultAsync(x => x.Id == id && x.VentaId == ventaId);
            if (detExist is null)
            {
                throw new KeyNotFoundException("El detalle de la venta no existe o no pertenece a la venta actual");
            }

            dbContext.DetallesVenta.Remove(detExist);
            await dbContext.SaveChangesAsync();

            return true;
        } 
    }
}
