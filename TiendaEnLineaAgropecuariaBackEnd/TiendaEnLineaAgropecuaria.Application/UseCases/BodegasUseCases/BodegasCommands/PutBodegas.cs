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
    public class PutBodegas
    {
        private readonly IRepositorioBodegas repositorioBodegas;

        public PutBodegas(IRepositorioBodegas repositorioBodegas)
        {
            this.repositorioBodegas = repositorioBodegas;
        }

        public async Task<bool> ExecuteAsync(int id, BodegaCreacionDTO bodegaCreacionDTO)
        {
            var bodega = new Bodega
            {
                Nombre = bodegaCreacionDTO.Nombre,
                EstadoId = bodegaCreacionDTO.EstadoId,
                Ubicacion = bodegaCreacionDTO.Ubicacion
            };

            var result = await repositorioBodegas.UpdateBodega(id, bodega);

            return result;
        }
    }
}
