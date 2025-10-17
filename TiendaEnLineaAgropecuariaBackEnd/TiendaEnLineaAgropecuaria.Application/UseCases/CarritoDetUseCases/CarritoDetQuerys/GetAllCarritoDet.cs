using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CarritoDetDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetQuerys
{
    public class GetAllCarritoDet
    {
        private readonly IRepositorioCarritoDet repositorioCarritoDet;

        public GetAllCarritoDet(IRepositorioCarritoDet repositorioCarritoDet)
        {
            this.repositorioCarritoDet = repositorioCarritoDet;
        }

       /* public async Task<List<CarritoDetDTO>> ExecuteAsync(string userId)
        {
            var carritoDets = await repositorioCarritoDet.GetAllCarritoDetalle(userId);

            var carritoDetsDTO = carritoDets.Select(x => new CarritoDetDTO
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                CarritoId = x.CarritoId,
                Fecha = x.Fecha,
                InventarioId = x.InventarioId,
                SubTotal = x.SubTotal,
                Inventario = x.Inventario
            }).ToList();

            return carritoDetsDTO;
        }*/
    }
}
