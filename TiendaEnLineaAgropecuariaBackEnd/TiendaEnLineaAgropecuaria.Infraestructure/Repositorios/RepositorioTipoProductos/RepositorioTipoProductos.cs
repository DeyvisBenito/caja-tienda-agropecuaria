using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioTipoProductos
{
    public class RepositorioTipoProductos : IRepositorioTipoProductos
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioTipoProductos(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<TipoProducto>> GetAllTipoProductos()
        {
            var tipoProductos = await dbContext.TipoProductos.Include(x => x.Estado)
                            .Include(x => x.Categoria).Include(x => x.TipoMedida).ToListAsync();

            return tipoProductos;
        }

        public async Task<TipoProducto> GetTipoProducto(int id)
        {
            var tipoProducto = await dbContext.TipoProductos.Include(x => x.Estado)
                .Include(x => x.Categoria).Include(x => x.TipoMedida).FirstOrDefaultAsync(x => x.Id == id);
            if (tipoProducto is null)
            {
                throw new KeyNotFoundException("Tipo de producto no encontrado");
            }

            return tipoProducto;
        }

        public async Task<bool> NewTipoProducto(TipoProducto tipoProducto)
        {
            var nombre = tipoProducto.Nombre.ToLower();
            var tipoProductoDB = await dbContext.TipoProductos.AnyAsync(x => x.Nombre.ToLower() == nombre);

            if (tipoProductoDB)
            {
                throw new Exception("Tipo de producto ya existente");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == tipoProducto.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            var categoriaDbExist = await dbContext.Categorias.AnyAsync(x => x.Id == tipoProducto.CategoriaId);
            if (!categoriaDbExist)
            {
                throw new KeyNotFoundException("La Categoria a colocar no existe");
            }

            var tipoMedidaDBExist = await dbContext.TiposMedida.AnyAsync(x => x.Id == tipoProducto.TipoMedidaId);
            if (!tipoMedidaDBExist)
            {
                throw new KeyNotFoundException("El Tipo de Medida a colocar no existe");
            }

            dbContext.TipoProductos.Add(tipoProducto);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTipoProducto(int id, TipoProducto tipoProducto)
        {
            var nombre = tipoProducto.Nombre.ToLower();
            var tipoProductoDB = await dbContext.TipoProductos.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoProductoDB is null)
            {
                throw new KeyNotFoundException("El Tipo de producto a actualizar no existe");
            }
            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == tipoProducto.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            var categoriaDbExist = await dbContext.Categorias.AnyAsync(x => x.Id == tipoProducto.CategoriaId);
            if (!categoriaDbExist)
            {
                throw new KeyNotFoundException("La Categoria a colocar no existe");
            }

            var tipoMedidaDBExist = await dbContext.TiposMedida.AnyAsync(x => x.Id == tipoProducto.TipoMedidaId);
            if (!tipoMedidaDBExist)
            {
                throw new KeyNotFoundException("El Tipo de Medida a colocar no existe");
            }


            if (nombre != tipoProductoDB.Nombre.ToLower())
            {
                var tipoProductoDbNombre = await dbContext.TipoProductos
                            .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id);

                if (tipoProductoDbNombre)
                {
                    throw new Exception("Tipo de producto ya existente");
                }
            }

            tipoProductoDB.EstadoId = tipoProducto.EstadoId;
            tipoProductoDB.CategoriaId = tipoProducto.CategoriaId;
            tipoProductoDB.Nombre = tipoProducto.Nombre;
            tipoProductoDB.TipoMedidaId = tipoProducto.TipoMedidaId;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTipoProducto(int id)
        {
            var inventarioDB = await dbContext.Inventarios.AnyAsync(x => x.TipoProductoId == id);
            if (inventarioDB)
            {
                throw new Exception("No puedes eliminar este Tipo de producto, tiene productos asociados");
            }

            var tipoProductoDB = await dbContext.TipoProductos.FirstOrDefaultAsync(x => x.Id == id);
            if (tipoProductoDB is null)
            {
                throw new KeyNotFoundException("El Tipo producto a eliminar no existe");
            }

            dbContext.TipoProductos.Remove(tipoProductoDB);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }

}
