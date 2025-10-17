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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioProveedores
{
    public class RepostiroioProveedores : IRepositorioProveedores
    {
        private readonly ApplicationDBContext dbContext;

        public RepostiroioProveedores(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Proveedor>> Get()
        {
            var proveedores = await dbContext.Proveedores.Include(x => x.Estado)
                .ToListAsync();

            return proveedores;
        }

        public async Task<Proveedor> Get(int id)
        {
            var proveedor = await dbContext.Proveedores.Include(x => x.Estado)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (proveedor is null)
            {
                throw new KeyNotFoundException("Proveedor no encontrado");
            }

            return proveedor;
        }

        public async Task<bool> NewProveedor(Proveedor proveedor)
        {
            var nit = proveedor.Nit.ToLower();
            var proveedorDB = await dbContext.Proveedores.FirstOrDefaultAsync(x => x.Nit.ToLower() == nit);

            if (proveedorDB is not null && proveedorDB.EstadoId != (int)EstadosEnum.Eliminado)
            {
                    throw new Exception("Proveedor con Nit ya existente");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == proveedor.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            if (proveedorDB is not null && proveedorDB.EstadoId == (int)EstadosEnum.Eliminado)
            {
                proveedorDB.Nit = proveedor.Nit;
                proveedorDB.Nombres = proveedor.Nombres;
                proveedorDB.Apellidos = proveedor.Apellidos;
                proveedorDB.Telefono = proveedor.Telefono;
                proveedorDB.Ubicacion = proveedor.Ubicacion;
                proveedorDB.EstadoId = proveedor.EstadoId;
                await dbContext.SaveChangesAsync();

                return true;
            }

            dbContext.Proveedores.Add(proveedor);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(int id, Proveedor proveedor)
        {
            var nit = proveedor.Nit.ToLower();
            var proveedorDB = await dbContext.Proveedores.FirstOrDefaultAsync(x => x.Id == id);

            if (proveedorDB is null)
            {
                throw new KeyNotFoundException("El proveedor a actualizar no existe");
            }

            var estadoBbExist = await dbContext.Estados.AnyAsync(x => x.Id == proveedor.EstadoId);
            if (!estadoBbExist)
            {
                throw new KeyNotFoundException("El Estado a colocar no existe");
            }

            if (nit != proveedorDB.Nit.ToLower())
            {
                var proveedorDBNit = await dbContext.Proveedores
                            .FirstOrDefaultAsync(x => x.Nit.ToLower() == nit && x.Id != id);

                if (proveedorDBNit is not null)
                {
                    if (proveedorDBNit.EstadoId == (int)EstadosEnum.Eliminado)
                    {
                        throw new Exception("Proveedor con Nit ya existente pero está deshabilitado, comunicarse a mantenimiento");
                    }
                    throw new Exception("Proveedor con Nit ya existente");
                }
                
            }

            proveedorDB.Nit = proveedor.Nit;
            proveedorDB.Nombres = proveedor.Nombres;
            proveedorDB.Apellidos = proveedor.Apellidos;
            proveedorDB.Telefono = proveedor.Telefono;
            proveedorDB.Ubicacion = proveedor.Ubicacion;
            proveedorDB.EstadoId = proveedor.EstadoId;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {

            var proveedorDB = await dbContext.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
            if (proveedorDB is null)
            {
                throw new KeyNotFoundException("El Proovedor a eliminar no existe");
            }
            if (proveedorDB.EstadoId == (int)EstadosEnum.Eliminado)
            {
                throw new KeyNotFoundException("El Proovedor a eliminar no existe");
            }

            proveedorDB.EstadoId = (int)EstadosEnum.Eliminado;
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
