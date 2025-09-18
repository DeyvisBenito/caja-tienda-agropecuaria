using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasCommands
{
    public class DeleteBodegas
    {
        private readonly IRepositorioBodegas repositorioBodegas;

        public DeleteBodegas(IRepositorioBodegas repositorioBodegas)
        {
            this.repositorioBodegas = repositorioBodegas;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var result = await repositorioBodegas.DeleteBodega(id);

            return result;
        }
    }
}
