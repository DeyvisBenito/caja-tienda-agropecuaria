using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresCommands
{
    public class PutProveedor
    {
        private readonly IRepositorioProveedores repositorioProveedores;

        public PutProveedor(IRepositorioProveedores repositorioProveedores)
        {
            this.repositorioProveedores = repositorioProveedores;
        }

        public async Task<bool> ExecuteAsync(int id, ProveedorCreacionDTO proveedorCreacionDTO)
        {
            var proveedor = new Proveedor
            {
                Nit = proveedorCreacionDTO.Nit,
                Nombres = proveedorCreacionDTO.Nombres,
                Apellidos = proveedorCreacionDTO.Apellidos,
                Telefono = proveedorCreacionDTO.Telefono,
                Ubicacion = proveedorCreacionDTO.Ubicacion,
                EstadoId = proveedorCreacionDTO.EstadoId,
                UserId = string.Empty
            };

            var result = await repositorioProveedores.Update(id, proveedor);

            return result;
        }
    }
}
