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
    public class PostCarritoDet
    {
        private readonly IRepositorioCarritoDet repositorioCarritoDet;

        public PostCarritoDet(IRepositorioCarritoDet repositorioCarritoDet)
        {
            this.repositorioCarritoDet = repositorioCarritoDet;
        }

        /*public async Task<bool> ExecuteAsync(string userId, CarritoDetCreacionDTO carritoDetCreacion)
        {
            var carritoDet = new CarritoDetalle
            {
                InventarioId = carritoDetCreacion.InventarioId,
                Cantidad = carritoDetCreacion.Cantidad
            };

            var resp = await repositorioCarritoDet.NewCarritoDetalle(userId, carritoDet);

            return resp;
        } */
    }
}
