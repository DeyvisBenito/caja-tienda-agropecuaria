using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ClientesDTOs;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ClientesUseCases.ClientesCommands
{
    public class PutCliente
    {
        private readonly IRepositorioClientes repositorioClientes;

        public PutCliente(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<bool> ExecuteAsync(int id, ClienteCreacionDTO clienteCreacionDTO)
        {
            var cliente = new Cliente
            {
                Nit = clienteCreacionDTO.Nit,
                Nombres = clienteCreacionDTO.Nombres,
                Apellidos = clienteCreacionDTO.Apellidos,
                Telefono = clienteCreacionDTO.Telefono,
                Email = clienteCreacionDTO.Email,
                UserId = string.Empty
            };

            var result = await repositorioClientes.Update(id, cliente);

            return result;
        }
    }
}
