using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.SucursalesUseCases.SucursalesCommands
{
    public class PutSucursales
    {
        private readonly IRepositorioSucursal repositorioSucursal;

        public PutSucursales(IRepositorioSucursal repositorioSucursal)
        {
            this.repositorioSucursal = repositorioSucursal;
        }

        public async Task<bool> ExecuteAsync(int id, SucursalCreacionDTO sucursalCreacionDTO)
        {
            var sucursal = new Sucursal
            {
                Nombre = sucursalCreacionDTO.Nombre,
                EstadoId = sucursalCreacionDTO.EstadoId,
                Ubicacion = sucursalCreacionDTO.Ubicacion
            };

            var result = await repositorioSucursal.UpdateSucursal(id, sucursal);

            return result;
        }
    }
}
