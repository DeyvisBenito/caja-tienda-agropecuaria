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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCarritoDet
{
    public class RepositorioCarritoDet  : IRepositorioCarritoDet
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioCarritoDet(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /*public async Task<IEnumerable<CarritoDetalle>> GetAllCarritoDetalle(string userId)
        {
            var carritoDB = await dbContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDB is null)
            {
                throw new KeyNotFoundException("El carrito no existe");
            }

            var carritoDetallesDB = await dbContext.CarritoDetalles.Where(x => x.CarritoId == carritoDB.Id)
                .Include(x => x.Inventario).ToListAsync();

            return carritoDetallesDB;
        }

        public async Task<CarritoDetalle> GetCarritoDetalleById(int id, string userId)
        {
            var carritoDB = await dbContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDB is null)
            {
                throw new KeyNotFoundException("El carrito no existe");
            }

            var carritoDetalle = await dbContext.CarritoDetalles.Include(x => x.Inventario).FirstOrDefaultAsync(x => x.Id == id);
            if (carritoDetalle is null)
            {
                throw new KeyNotFoundException("El detalle del carrito no existe");
            }

            return carritoDetalle;
        }

        public async Task<bool> NewCarritoDetalle(string userId, CarritoDetalle carritoDet)
        {
            var carritoDb = await dbContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDb is null)
            {
                throw new InvalidOperationException("El carrito del usuario no existe");
            }
            carritoDet.CarritoId = carritoDb.Id;

            var producto = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == carritoDet.InventarioId);
            if (producto is null)
            {
                throw new KeyNotFoundException("El producto a agregar al carrito no existe");
            }

            if (carritoDet.Cantidad > producto.Stock)
            {
                throw new InvalidOperationException("No existe stock suficiente para la cantidad a solicitar");
            }

            var productoExistCarDet = await dbContext.CarritoDetalles
                            .FirstOrDefaultAsync(x => x.InventarioId == producto.Id && x.CarritoId == carritoDb.Id);
            if (productoExistCarDet is not null)
            {
                productoExistCarDet.Cantidad = productoExistCarDet.Cantidad + carritoDet.Cantidad;
                productoExistCarDet.SubTotal = producto.Precio * productoExistCarDet.Cantidad;

                await dbContext.SaveChangesAsync();
                return true;
            }


            carritoDet.SubTotal = producto.Precio * carritoDet.Cantidad;
            carritoDet.Fecha = DateTime.UtcNow;

            dbContext.CarritoDetalles.Add(carritoDet);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCarritoDetalle(string userId, int id, CarritoDetalle carritoDet)
        {
            var carritoDetBd = await dbContext.CarritoDetalles.FirstOrDefaultAsync(x => x.Id == id);
            if (carritoDetBd is null)
            {
                throw new KeyNotFoundException("El producto del carrito a modificar no existe");
            }

            if (carritoDet.InventarioId != carritoDetBd.InventarioId)
            {
                throw new InvalidOperationException("Error, ha cambiado el producto, accion invalida");
            }

            var carritoDb = await dbContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDb is null)
            {
                throw new InvalidOperationException("El carrito del usuario no existe");
            }
            carritoDet.CarritoId = carritoDb.Id;

            var producto = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == carritoDet.InventarioId);
            if (producto is null)
            {
                throw new KeyNotFoundException("El producto a actualizar en el carrito no existe");
            }

            if (producto!.Stock < carritoDet.Cantidad)
            {
                if (carritoDet.Cantidad > carritoDetBd.Cantidad)
                {
                    throw new InvalidOperationException("No existe suficiente stock del producto");
                }
            }

            carritoDetBd.Cantidad = carritoDet.Cantidad;
            carritoDetBd.SubTotal = producto.Precio * carritoDet.Cantidad;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarCarritoDetalle(string userId, int id)
        {
            var carritoDb = await dbContext.Carritos.FirstOrDefaultAsync(x => x.IdUser == userId);
            if (carritoDb is null)
            {
                throw new InvalidOperationException("El carrito no pertenece al usuario con sesión");
            }

            var carritoDetDb = await dbContext.CarritoDetalles.FirstOrDefaultAsync(x => x.Id == id && x.CarritoId == carritoDb.Id);
            if (carritoDetDb is null)
            {
                throw new KeyNotFoundException("El producto a eliminar del carrito ya no existe en el carrito");
            }

            dbContext.CarritoDetalles.Remove(carritoDetDb);
            await dbContext.SaveChangesAsync();

            return true;
        } */
    }
}
