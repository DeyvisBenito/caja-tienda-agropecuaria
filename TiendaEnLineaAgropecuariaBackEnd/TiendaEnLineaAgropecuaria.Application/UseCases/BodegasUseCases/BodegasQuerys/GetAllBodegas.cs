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
    public class GetAllBodegas
    {
        private readonly IRepositorioBodegas repositorioBodegas;

        public GetAllBodegas(IRepositorioBodegas repositorioBodegas)
        {
            this.repositorioBodegas = repositorioBodegas;
        }

        public async Task<List<BodegaDTO>> ExecuteAsync()
        {
            var bodegas = await repositorioBodegas.GetAllBodegas();
            var bodegasDTO = bodegas.Select(x => new BodegaDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Estado = x.Estado!.Nombre,
                EstadoId = x.EstadoId,
                Ubicacion = x.Ubicacion
            }).ToList();

            return bodegasDTO;
        }
    }
}
