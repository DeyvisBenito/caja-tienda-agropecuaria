using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioCategorias
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioCategorias(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            var categorias = await dbContext.Categorias.Include(x => x.Estado)
                .ToListAsync();

            return categorias;
        }

        public async Task<Categoria> GetCategoria(int id)
        {
            var categoria = await dbContext.Categorias.Include(x => x.Estado)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (categoria is null)
            {
                throw new KeyNotFoundException("Categoria no encontrada");
            }

            return categoria;
        }

        public async Task<bool> NewCategoria(Categoria categoria)
        {
            var nombre = categoria.Nombre.ToLower();
            var categoriaDB = await dbContext.Categorias.AnyAsync(x => x.Nombre.ToLower() == nombre);

            if (categoriaDB)
            {
                throw new Exception("Categoria ya existente");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == categoria.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            dbContext.Categorias.Add(categoria);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateCategoria(int id, Categoria categoria)
        {
            var nombre = categoria.Nombre.ToLower();
            var categoriaDb = await dbContext.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoriaDb is null)
            {
                throw new KeyNotFoundException("La Categoria a actualizar no existe");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == categoria.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            if (nombre != categoriaDb.Nombre.ToLower())
            {
                var categoriaDbNombre = await dbContext.Categorias
                            .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id);

                if (categoriaDbNombre)
                {
                    throw new Exception("Categoria ya existente");
                }
            }

            categoriaDb.EstadoId = categoria.EstadoId;
            categoriaDb.Nombre = categoria.Nombre;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            var productosDB = await dbContext.TipoProductos.AnyAsync(x => x.CategoriaId == id);
            if (productosDB)
            {
                throw new Exception("No puedes eliminar esta categoria, tiene productos asociados");
            }

            var categoriaDb = await dbContext.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            if (categoriaDb is null)
            {
                throw new KeyNotFoundException("La Categoria a eliminar no existe");
            }

            dbContext.Categorias.Remove(categoriaDb);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
