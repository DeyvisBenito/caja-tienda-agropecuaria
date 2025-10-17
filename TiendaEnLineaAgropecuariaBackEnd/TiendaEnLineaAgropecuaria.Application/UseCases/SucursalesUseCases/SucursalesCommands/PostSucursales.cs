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
    public class PostSucursales
    {
        private readonly IRepositorioSucursal repositorioSucursal;

        public PostSucursales(IRepositorioSucursal repositorioSucursal)
        {
            this.repositorioSucursal = repositorioSucursal;
        }

        public async Task<bool> ExecuteAsync(SucursalCreacionConUserIdDTO sucursalDTO)
        {
            var sucursal = new Sucursal
            {
                UserId = sucursalDTO.UserId,
                Nombre = sucursalDTO.Nombre,
                EstadoId = sucursalDTO.EstadoId,
                Ubicacion = sucursalDTO.Ubicacion
            };

            var result = await repositorioSucursal.NewSucursal(sucursal);

            return result;
        }
    }
}
