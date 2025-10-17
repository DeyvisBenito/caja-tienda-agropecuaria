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

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioClientes
{
    public class RepositorioClientes: IRepositorioClientes
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioClientes(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Cliente>> Get()
        {
            var clientes = await dbContext.Clientes.ToListAsync();

            return clientes;
        }

        public async Task<Cliente> Get(int id)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente no encontrado");
            }

            return cliente;
        }

        public async Task<bool> NewCliente(Cliente cliente)
        {
            var nit = cliente.Nit.ToLower();
            var clienteDb = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Nit.ToLower() == nit);

            if (clienteDb is not null)
            {
                throw new InvalidOperationException("Cliente con Nit ya existente");
            }

            dbContext.Clientes.Add(cliente);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(int id, Cliente cliente)
        {
            var nit = cliente.Nit.ToLower();
            var clienteDb = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (clienteDb is null)
            {
                throw new KeyNotFoundException("El Cliente a actualizar no existe");
            }

            if (nit != clienteDb.Nit.ToLower())
            {
                var clienteBdNit = await dbContext.Clientes
                            .FirstOrDefaultAsync(x => x.Nit.ToLower() == nit && x.Id != id);

                if (clienteBdNit is not null)
                {
                    throw new InvalidOperationException("Cliente con Nit ya existente");
                }

            }

            clienteDb.Nit = cliente.Nit;
            clienteDb.Nombres = cliente.Nombres;
            clienteDb.Apellidos = cliente.Apellidos;
            clienteDb.Telefono = cliente.Telefono;
            clienteDb.Email = cliente.Email;
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {

            var clienteDb = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (clienteDb is null)
            {
                throw new KeyNotFoundException("El Cliente a eliminar no existe");
            }
            var tieneVentas = await dbContext.Ventas.AnyAsync(x => x.ClienteId == id);
            if (tieneVentas)
            {
                throw new InvalidOperationException("El Cliente no puede ser eliminado porque existen ventas a su nombre");
            }

            dbContext.Clientes.Remove(clienteDb);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
