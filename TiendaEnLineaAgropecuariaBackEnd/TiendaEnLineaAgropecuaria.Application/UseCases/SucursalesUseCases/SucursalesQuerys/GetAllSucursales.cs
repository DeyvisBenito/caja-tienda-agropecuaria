using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesQuerys
{
    public class GetAllSucursales
    {
        private readonly IRepositorioSucursal repositorioSucursal;

        public GetAllSucursales(IRepositorioSucursal repositorioSucursal)
        {
            this.repositorioSucursal = repositorioSucursal;
        }

        public async Task<List<SucursalDTO>> ExecuteAsync()
        {
            var sucursales = await repositorioSucursal.Get();
            var sucursalDTO = sucursales.Select(x => new SucursalDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Estado = x.Estado!.Nombre,
                EstadoId = x.EstadoId,
                Ubicacion = x.Ubicacion
            }).ToList();

            return sucursalDTO;
        }
    }
}
