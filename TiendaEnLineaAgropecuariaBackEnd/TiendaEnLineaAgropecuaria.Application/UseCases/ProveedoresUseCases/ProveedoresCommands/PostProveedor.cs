using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands
{
    public class PostProveedor
    {
        private readonly IRepositorioProveedores repositorioProveedores;

        public PostProveedor(IRepositorioProveedores repositorioProveedores)
        {
            this.repositorioProveedores = repositorioProveedores;
        }

        public async Task<bool> ExecuteAsync(ProveedorCreacionConUserIdDTO proveedorDTO)
        {
            var proveedor = new Proveedor
            {
                UserId = proveedorDTO.UserId,
                Nit = proveedorDTO.Nit,
                Nombres = proveedorDTO.Nombres,
                Apellidos = proveedorDTO.Apellidos,
                Telefono = proveedorDTO.Telefono,
                Ubicacion = proveedorDTO.Ubicacion,
                EstadoId = proveedorDTO.EstadoId
            };

            var result = await repositorioProveedores.NewProveedor(proveedor);

            return result;
        }
    }
}
