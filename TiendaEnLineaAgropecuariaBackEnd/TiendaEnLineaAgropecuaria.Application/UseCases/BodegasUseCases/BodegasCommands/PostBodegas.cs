using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.BodegasDTO;
using TiendaEnLineaAgropecuaria.Application.DTOs.CategoriasDTO;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.BodegasUseCases.BodegasCommands
{
    public class PostBodegas
    {
        private readonly IRepositorioBodegas repositorioBodegas;

        public PostBodegas(IRepositorioBodegas repositorioBodegas)
        {
            this.repositorioBodegas = repositorioBodegas;
        }

        public async Task<bool> ExecuteAsync(BodegaCreacionConUserIdDTO bodegaDTO)
        {
            var bodega = new Bodega
            {
                IdUser = bodegaDTO.UserId,
                Nombre = bodegaDTO.Nombre,
                EstadoId = bodegaDTO.EstadoId,
                Ubicacion = bodegaDTO.Ubicacion
            };

            var result = await repositorioBodegas.NewBodega(bodega);

            return result;
        }
    }
}
