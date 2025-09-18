using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioBodegas
{
    public class RepositorioBodegas : IRepositorioBodegas
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioBodegas(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Bodega>> GetAllBodegas()
        {
            var bodegas = await dbContext.Bodegas.Include(x => x.Estado)
                .ToListAsync();

            return bodegas;
        }

        public async Task<Bodega> GetBodegaById(int id)
        {
            var bodega = await dbContext.Bodegas.Include(x => x.Estado)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (bodega is null)
            {
                throw new KeyNotFoundException("Bodega no encontrada");
            }

            return bodega;
        }

        public async Task<bool> NewBodega(Bodega bodega)
        {
            var nombre = bodega.Nombre.ToLower();
            var bodegaDB = await dbContext.Bodegas.AnyAsync(x => x.Nombre.ToLower() == nombre);

            if (bodegaDB)
            {
                throw new Exception("Bodega ya existente");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == bodega.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            dbContext.Bodegas.Add(bodega);
            await dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> UpdateBodega(int id, Bodega bodega)
        {
            var nombre = bodega.Nombre.ToLower();
            var bodegaDB = await dbContext.Bodegas.FirstOrDefaultAsync(x => x.Id == id);

            if (bodegaDB is null)
            {
                throw new KeyNotFoundException("La Bodega a actualizar no existe");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == bodega.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            if (nombre != bodegaDB.Nombre.ToLower())
            {
                var bodegaDbNombre = await dbContext.Bodegas
                            .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id);

                if (bodegaDbNombre)
                {
                    throw new Exception("Bodega ya existente");
                }
            }

            bodegaDB.EstadoId = bodega.EstadoId;
            bodegaDB.Nombre = bodega.Nombre;
            bodegaDB.Ubicacion = bodega.Ubicacion;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBodega(int id)
        {
            var inventarioDB = await dbContext.Inventarios.AnyAsync(x => x.BodegaId == id);
            if (inventarioDB)
            {
                throw new Exception("No puedes eliminar esta Bodega, tiene inventarios asociados");
            }

            var bodegaDB = await dbContext.Bodegas.FirstOrDefaultAsync(x => x.Id == id);
            if (bodegaDB is null)
            {
                throw new KeyNotFoundException("La Bodega a eliminar no existe");
            }

            dbContext.Bodegas.Remove(bodegaDB);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
