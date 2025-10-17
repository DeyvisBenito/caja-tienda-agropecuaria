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
    public class RepositorioSucursal : IRepositorioSucursal
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioSucursal(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        
        public async Task<IEnumerable<Sucursal>> Get()
        {
            var sucursales = await dbContext.Sucursales.Include(x => x.Estado)
                .ToListAsync();

            return sucursales;
        }

        public async Task<Sucursal> GetById(int id)
        {
            var sucursal = await dbContext.Sucursales.Include(x => x.Estado)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (sucursal is null)
            {
                throw new KeyNotFoundException("Sucursal no encontrada");
            }

            return sucursal;
        }

        public async Task<bool> NewSucursal(Sucursal sucursal)
        {
            var nombre = sucursal.Nombre.ToLower();
            var sucursalDB = await dbContext.Sucursales.AnyAsync(x => x.Nombre.ToLower() == nombre);

            if (sucursalDB)
            {
                throw new Exception("Sucursal ya existente");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == sucursal.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }
            dbContext.Sucursales.Add(sucursal);
            await dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> UpdateSucursal(int id, Sucursal sucursal)
        {
            var nombre = sucursal.Nombre.ToLower();
            var sucursalDB = await dbContext.Sucursales.FirstOrDefaultAsync(x => x.Id == id);

            if (sucursalDB is null)
            {
                throw new KeyNotFoundException("La Sucursal a actualizar no existe");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == sucursal.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            if (nombre != sucursalDB.Nombre.ToLower())
            {
                var sucursalDBNombre = await dbContext.Sucursales
                            .AnyAsync(x => x.Nombre.ToLower() == nombre && x.Id != id);

                if (sucursalDBNombre)
                {
                    throw new Exception("Sucursal ya existente");
                }
            }

            sucursalDB.EstadoId = sucursal.EstadoId;
            sucursalDB.Nombre = sucursal.Nombre;
            sucursalDB.Ubicacion = sucursal.Ubicacion;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSucursal(int id)
        {
            var inventarioDB = await dbContext.Inventarios.AnyAsync(x => x.SucursalId == id);
            if (inventarioDB)
            {
                throw new Exception("No puedes eliminar esta Sucursal, tiene inventarios asociados");
            }

            var sucursalDB = await dbContext.Sucursales.FirstOrDefaultAsync(x => x.Id == id);
            if (sucursalDB is null)
            {
                throw new KeyNotFoundException("La Sucursal a eliminar no existe");
            }

            dbContext.Sucursales.Remove(sucursalDB);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
