using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesQuerys
{
    public class GetClienteById
    {
        private readonly IRepositorioClientes repositorioClientes;

        public GetClienteById(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<ClienteDTO> ExecuteAsync(int id)
        {
            var cliente = await repositorioClientes.Get(id);
            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nit = cliente.Nit,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                FechaRegistro = cliente.FechaRegistro
            };

            return clienteDTO;
        }
    }
}
