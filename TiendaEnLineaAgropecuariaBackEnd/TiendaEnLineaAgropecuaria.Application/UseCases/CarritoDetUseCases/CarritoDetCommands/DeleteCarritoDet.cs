using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.CarritoDetUseCases.CarritoDetCommands
{
    public class DeleteCarritoDet
    {
        private readonly IRepositorioCarritoDet repositorioCarritoDet;

        public DeleteCarritoDet(IRepositorioCarritoDet repositorioCarritoDet)
        {
            this.repositorioCarritoDet = repositorioCarritoDet;
        }

        /*public async Task<bool> ExecuteAsync(string userId, int id)
        {
            var resp = await repositorioCarritoDet.EliminarCarritoDetalle(userId, id);

            return resp;
        } */
    }
}
