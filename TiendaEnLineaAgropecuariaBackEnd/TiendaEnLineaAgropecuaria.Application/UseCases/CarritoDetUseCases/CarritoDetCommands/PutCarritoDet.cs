using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.CarritoDetDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetCommands
{
    public class PutCarritoDet
    {
        private readonly IRepositorioCarritoDet repositorioCarritoDet;

        public PutCarritoDet(IRepositorioCarritoDet repositorioCarritoDet)
        {
            this.repositorioCarritoDet = repositorioCarritoDet;
        }

        /*public async Task<bool> ExecuteAsync(string userId, int id, CarritoDetCreacionDTO carritoDetCreacion)
        {
            var carritoDet = new CarritoDetalle
            {
                Cantidad = carritoDetCreacion.Cantidad,
                InventarioId = carritoDetCreacion.InventarioId
            };

            var result = await repositorioCarritoDet.UpdateCarritoDetalle(userId, id, carritoDet);

            return result;
        } */
    }
}
