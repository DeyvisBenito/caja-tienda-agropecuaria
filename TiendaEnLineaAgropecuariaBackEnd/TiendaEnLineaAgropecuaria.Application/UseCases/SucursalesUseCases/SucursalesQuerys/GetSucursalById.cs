using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesQuerys
{
    public class GetSucursalById
    {
        private readonly IRepositorioSucursal repositorioSucursal;

        public GetSucursalById(IRepositorioSucursal repositorioSucursal)
        {
            this.repositorioSucursal = repositorioSucursal;
        }

        public async Task<SucursalDTO> ExecuteAsync(int id)
        {
            var sucursal = await repositorioSucursal.GetById(id);

            var sucursalDTO = new SucursalDTO
            {
                Id = sucursal.Id,
                Nombre = sucursal.Nombre,
                Estado = sucursal.Estado!.Nombre,
                EstadoId = sucursal.EstadoId,
                Ubicacion = sucursal.Ubicacion
            };

            return sucursalDTO;
        }
    }
}
