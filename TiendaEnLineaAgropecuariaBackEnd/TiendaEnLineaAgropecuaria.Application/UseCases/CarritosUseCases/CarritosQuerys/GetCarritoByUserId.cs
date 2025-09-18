using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CarritosUseCases.CarritosQuerys
{
    public class GetCarritoByUserId
    {
        private readonly IRepositorioCarrito repositorioCarrito;

        public GetCarritoByUserId(IRepositorioCarrito repositorioCarrito)
        {
            this.repositorioCarrito = repositorioCarrito;
        }

        public async Task<Carrito> ExecuteAsync(string userId)
        {
            var carrito = await repositorioCarrito.GetCarritoByUserId(userId);

            return carrito;
        }
    }
}
