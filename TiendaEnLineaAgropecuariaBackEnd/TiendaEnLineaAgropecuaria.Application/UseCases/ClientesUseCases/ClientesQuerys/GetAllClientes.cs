using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesQuerys
{
    public class GetAllClientes
    {
        private readonly IRepositorioClientes repositorioClientes;

        public GetAllClientes(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<IEnumerable<ClienteDTO>> ExecuteAsync()
        {
            var clientes = await repositorioClientes.Get();

            var clientesDTO = clientes.Select(x => new ClienteDTO
            {
                Id = x.Id,
                Nit = x.Nit,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Telefono = x.Telefono,
                Email = x.Email,
                FechaRegistro = x.FechaRegistro
            }).ToList();

            return clientesDTO;
        }
    }
}
