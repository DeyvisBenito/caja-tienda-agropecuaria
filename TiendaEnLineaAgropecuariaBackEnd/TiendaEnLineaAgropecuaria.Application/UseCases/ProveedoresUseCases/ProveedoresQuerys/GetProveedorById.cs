using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys
{
    public class GetProveedorById
    {
        private readonly IRepositorioProveedores repositorioProveedores;

        public GetProveedorById(IRepositorioProveedores repositorioProveedores)
        {
            this.repositorioProveedores = repositorioProveedores;
        }

        public async Task<ProveedorDTO> ExecuteAsync(int id)
        {
            var proveedor = await repositorioProveedores.Get(id);

            var proveedoresDTO = new ProveedorDTO
            {
                Id = proveedor.Id,
                Nit = proveedor.Nit,
                Nombres = proveedor.Nombres,
                Apellidos = proveedor.Apellidos,
                Telefono = proveedor.Telefono,
                Ubicacion = proveedor.Ubicacion,
                EstadoId = proveedor.EstadoId,
                Estado = proveedor.Estado!.Nombre
            };

            return proveedoresDTO;
        }
    }
}
