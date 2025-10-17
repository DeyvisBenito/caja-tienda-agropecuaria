using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.ProveedoresUseCases.ProveedoresQuerys
{
    public class GetAllProveedores
    {
        private readonly IRepositorioProveedores repositorioProveedores;

        public GetAllProveedores(IRepositorioProveedores repositorioProveedores)
        {
            this.repositorioProveedores = repositorioProveedores;
        }

        public async Task<List<ProveedorDTO>> ExecuteAsync()
        {
            var proveedores = await repositorioProveedores.Get();
            var proveedoresDTO = proveedores.Select(x => new ProveedorDTO
            {
                Id = x.Id,
                Nit = x.Nit,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Telefono = x.Telefono,
                Ubicacion = x.Ubicacion,
                EstadoId = x.EstadoId,
                Estado = x.Estado!.Nombre
            }).ToList();

            return proveedoresDTO;
        }
    }
}
