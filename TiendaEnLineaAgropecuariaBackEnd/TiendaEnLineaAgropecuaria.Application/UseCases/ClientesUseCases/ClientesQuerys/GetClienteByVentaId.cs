using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesQuerys
{
    public class GetClienteByVentaId
    {
        private readonly IRepositorioClientes repositorioClientes;

        public GetClienteByVentaId(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<ClienteDTO> ExecuteAsync(int ventaId)
        {
            var cliente = await repositorioClientes.GetByVentaId(ventaId);
            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nit = cliente.Nit,
                Email = cliente.Email,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                Telefono = cliente.Telefono,
                FechaRegistro = cliente.FechaRegistro
            };

            return clienteDTO;
        }
    }
}
