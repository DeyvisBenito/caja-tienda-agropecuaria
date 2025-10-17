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
    public class PostCliente
    {
        private readonly IRepositorioClientes repositorioClientes;

        public PostCliente(IRepositorioClientes repositorioClientes)
        {
            this.repositorioClientes = repositorioClientes;
        }

        public async Task<bool> ExecuteAsync(ClienteCreacionConUserId clienteCreacion)
        {
            var cliente = new Cliente
            {
                Nit = clienteCreacion.Nit,
                Nombres = clienteCreacion.Nombres,
                Apellidos = clienteCreacion.Apellidos,
                Telefono = clienteCreacion.Telefono,
                Email = clienteCreacion.Email,
                FechaRegistro = DateTime.UtcNow,
                UserId = clienteCreacion.UserId
            };

            var result = await repositorioClientes.NewCliente(cliente);

            return result;
        }
    }
}
