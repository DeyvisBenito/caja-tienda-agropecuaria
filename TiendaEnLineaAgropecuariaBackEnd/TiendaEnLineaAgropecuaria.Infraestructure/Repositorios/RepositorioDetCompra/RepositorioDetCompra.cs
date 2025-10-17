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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioDetCompra
{
    public class RepositorioDetCompra : IRepositorioDetCompra
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioDetCompra(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DetalleCompra>> Get()
        {
            var detCompra = await dbContext.DetallesCompra.Include(x => x.Compra).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).ToListAsync();

            return detCompra;
        }

        public async Task<IEnumerable<DetalleCompra>> GetByCompraId(int compraId, int sucursalId)
        {
            if(sucursalId != 0)
            {
                var compraExist = await dbContext.Compras.AnyAsync(x => x.Id == compraId && x.SucursalId == sucursalId);
                if (!compraExist)
                {
                    throw new KeyNotFoundException("La compra seleccionada no existe o no existe en la sucursal actual");
                }
            }
            
            var detCompra = await dbContext.DetallesCompra.Include(x => x.Compra).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).Where(x => x.CompraId == compraId).ToListAsync();

            return detCompra;
        }

        public async Task<DetalleCompra?> GetByInvId(int compraId, int invId)
        {
            var invExist = await dbContext.DetallesCompra.FirstOrDefaultAsync(x => x.CompraId == compraId && x.InventarioId == invId);

            return invExist;
        }

        public async Task<DetalleCompra> GetById(int idCompra, int idDet)
        {
            var detCompra = await dbContext.DetallesCompra.Include(x => x.Compra).Include(x => x.Inventario).ThenInclude(x => x!.Sucursal)
                            .Include(x => x.Inventario).ThenInclude(x => x.TipoProducto)
                            .Include(x => x.Inventario).ThenInclude(x => x.UnidadMedida)
                            .Include(x => x.Estado).Include(x => x.UnidadMedida).FirstOrDefaultAsync(x => x.Id == idDet && x.CompraId == idCompra);

            if(detCompra is null)
            {
                throw new KeyNotFoundException("El detalle de la compra no existe");
            }

            return detCompra;
        }

        public async Task<bool> NewDetalle(DetalleCompra detalle, int sucursalId)
        {
            if(detalle.UnidadMedidaId != (int)UnidadesMedidaEnum.caja)
            {
                detalle.UnidadesPorCaja = null;
            }
            var compraExist = await dbContext.Compras.AnyAsync(x => x.Id == detalle.CompraId && x.SucursalId == sucursalId);
            if (!compraExist)
            {
                throw new KeyNotFoundException("La compra seleccionada no existe o no existe en la sucursal actual");
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

            dbContext.DetallesCompra.Add(detalle);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateDetalle(DetalleCompra detalle, int id, int idCompra, int sucursalId)
        {
            if (detalle.UnidadMedidaId != (int)UnidadesMedidaEnum.caja)
            {
                detalle.UnidadesPorCaja = null;
            }
            var compraExist = await dbContext.Compras.AnyAsync(x => x.Id == detalle.CompraId && x.SucursalId == sucursalId);
            if (!compraExist)
            {
                throw new KeyNotFoundException("La compra seleccionada no existe o no existe en la sucursal actual");
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

            var detalleExist = await dbContext.DetallesCompra.FirstOrDefaultAsync(x => x.Id == id && x.EstadoId == (int)EstadosEnum.Activo);
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
            detalleExist.PrecioCosto = detalle.PrecioCosto;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, int compraId, int sucursalId)
        {
            var compraExist = await dbContext.Compras.AnyAsync(x => x.Id == compraId && x.SucursalId == sucursalId);
            if (!compraExist)
            {
                throw new KeyNotFoundException("La compra no existe o no pertenece a la sucursal actual");
            }
            var detExist = await dbContext.DetallesCompra.FirstOrDefaultAsync(x => x.Id == id && x.CompraId == compraId);
            if (detExist is null)
            {
                throw new KeyNotFoundException("El detalle de la compra no existe o no pertenece a la compra actual");
            }

            dbContext.DetallesCompra.Remove(detExist);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
