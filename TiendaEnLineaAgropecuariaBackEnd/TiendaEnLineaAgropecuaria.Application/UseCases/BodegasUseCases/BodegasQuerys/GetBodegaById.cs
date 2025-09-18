using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasQuerys
{
    public class GetBodegaById
    {
        private readonly IRepositorioBodegas repositorioBodegas;

        public GetBodegaById(IRepositorioBodegas repositorioBodegas)
        {
            this.repositorioBodegas = repositorioBodegas;
        }

        public async Task<BodegaDTO> ExecuteAsync(int id)
        {
            var bodega = await repositorioBodegas.GetBodegaById(id);

            var bodegaDTO = new BodegaDTO
            {
                Id = bodega.Id,
                Nombre = bodega.Nombre,
                Estado = bodega.Estado!.Nombre,
                EstadoId = bodega.EstadoId,
                Ubicacion = bodega.Ubicacion
            };

            return bodegaDTO;
        }
    }
}
