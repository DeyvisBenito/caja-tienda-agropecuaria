using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CarritoDetDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetQuerys
{
    public class GetCarritoDetById
    {
        private readonly IRepositorioCarritoDet repositorioCarritoDet;

        public GetCarritoDetById(IRepositorioCarritoDet repositorioCarritoDet)
        {
            this.repositorioCarritoDet = repositorioCarritoDet;
        }

        /*public async Task<CarritoDetDTO> ExecuteAsync(int id, string userId)
        {
            var carritoDet = await repositorioCarritoDet.GetCarritoDetalleById(id, userId);

            var carritoDetDTO = new CarritoDetDTO
            {
                 Id = carritoDet.Id,
                 Cantidad = carritoDet.Cantidad,
                 CarritoId = carritoDet.CarritoId,
                 Fecha = carritoDet.Fecha,
                 InventarioId = carritoDet.InventarioId,
                 SubTotal = carritoDet.SubTotal,
                 Inventario = carritoDet.Inventario
            };

            return carritoDetDTO;
        }*/
    }
}
