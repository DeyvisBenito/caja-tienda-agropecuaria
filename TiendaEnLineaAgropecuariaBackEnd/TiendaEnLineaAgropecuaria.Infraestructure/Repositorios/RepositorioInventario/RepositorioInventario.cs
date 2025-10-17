using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioInventario
{
    public class RepositorioInventario : IRepositorioInventario
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioInventario(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IEnumerable<Inventario>> Get()
        {
            var inventarios = await dbContext.Inventarios.Include(x => x.TipoProducto)
                .Include(x => x.Estado).Include(x => x.Sucursal).Include(x => x.UnidadMedida)
                .ToListAsync();

            return inventarios;
        }

        public async Task<Inventario> GetById(int id)
        {
            var inventario = await dbContext.Inventarios.Include(x => x.TipoProducto)
                .Include(x => x.Estado).Include(x => x.Sucursal).Include(x => x.UnidadMedida)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (inventario is null)
            {
                throw new KeyNotFoundException("Producto en inventario no encontrado");
            }

            return inventario;
        }

        public async Task<Inventario> GetByCodigo(string codigo, int sucursalId)
        {
            var inventario = await dbContext.Inventarios.Include(x => x.TipoProducto)
                .Include(x => x.Estado).Include(x => x.Sucursal).Include(x => x.UnidadMedida)
                .FirstOrDefaultAsync(x => x.Codigo == codigo && x.SucursalId == sucursalId);
            if (inventario is null)
            {
                throw new KeyNotFoundException("Producto en inventario no encontrado en la sucursal actual");
            }

            return inventario;
        }
        
        public async Task<bool> NewInventario(Inventario inventario)
        {
            var inventarioDB = await dbContext.Inventarios
                    .AnyAsync(x => x.Codigo == inventario.Codigo && x.SucursalId == inventario.SucursalId);

            if (inventarioDB)
            {
                throw new Exception("El producto ya existente en el inventario de esta bodega");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == inventario.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            var tipoProductoDBExist = await dbContext.TipoProductos.AnyAsync(x => x.Id == inventario.TipoProductoId);
            if (!tipoProductoDBExist)
            {
                throw new KeyNotFoundException("El Tipo de producto a colocar no existe");
            }
            var sucursalDB = await dbContext.Sucursales.AnyAsync(x => x.Id == inventario.SucursalId);
            if (!sucursalDB)
            {
                throw new KeyNotFoundException("La sucursal a colocar el inventario del producto no existe");
            }

            dbContext.Inventarios.Add(inventario);
            await dbContext.SaveChangesAsync();

            return true;
        }
        /*
        public async Task<bool> UpdateInventario(int id, Inventario inventario)
        {
            var nombre = inventario.Nombre.ToLower();
            var inventarioDB = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);

            if (inventarioDB is null)
            {
                throw new KeyNotFoundException("El producto del inventario a actualizar no existe");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == inventario.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            var tipoProductoDBExist = await dbContext.TipoProductos.AnyAsync(x => x.Id == inventario.TipoProductoId);
            if (!tipoProductoDBExist)
            {
                throw new KeyNotFoundException("El Tipo de producto a colocar no existe");
            }
            var bodegaDBExist = await dbContext.Bodegas.AnyAsync(x => x.Id == inventario.BodegaId);
            if (!bodegaDBExist)
            {
                throw new KeyNotFoundException("La bodega a colocar el inventario del producto no existe");
            }

            if (nombre != inventarioDB.Nombre.ToLower())
            {
               
                var inventarioDbNombreExist = await dbContext.Inventarios
                            .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id && x.BodegaId == inventario.BodegaId);

                if (inventarioDbNombreExist)
                {
                    throw new Exception("Ya existe un producto en el inventario con este nombre en la bodega seleccionada");
                }


            }else if (inventario.BodegaId != inventarioDB.BodegaId)
            {
                var inventarioDbNombreExistBodega = await dbContext.Inventarios
                        .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id && x.BodegaId == inventario.BodegaId);
                if (inventarioDbNombreExistBodega)
                {
                    throw new Exception("Ya existe un producto en el inventario con este nombre en la bodega seleccionada");
                }
            }

            inventarioDB.EstadoId = inventario.EstadoId;
            inventarioDB.Nombre = inventario.Nombre;
            inventarioDB.TipoProductoId = inventario.TipoProductoId;
            inventarioDB.BodegaId = inventario.BodegaId;
            inventarioDB.Marca = inventario.Marca;
            inventarioDB.Precio = inventario.Precio;
            inventarioDB.UrlFoto = inventario.UrlFoto;
            inventarioDB.Descripcion = inventario.Descripcion;
            inventarioDB.Stock = inventario.Stock;

            if(inventarioDB.Stock == 0)
            {
                inventarioDB.EstadoId = 2;
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteInventario(int id)
        {
            //var productosDB = await dbContext.TipoProductos.AnyAsync(x => x.CategoriaId == id);
            //if (productosDB)
            //{
            //    throw new Exception("No puedes eliminar esta categoria, tiene productos asociados");
            //}
            // TODO VALIDAR AL ELIMINAR QUE TENGAN CARRITODETALLE PENDIENTE, OFERTA ACTIVA Y DETALLECOMPRA PENDIENTE

            var inventarioDB = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);
            if (inventarioDB is null)
            {
                throw new KeyNotFoundException("El producto a eliminar no existe en el inventario");
            }

            dbContext.Inventarios.Remove(inventarioDB);
            await dbContext.SaveChangesAsync();

            return true;
        } */
    }
}
