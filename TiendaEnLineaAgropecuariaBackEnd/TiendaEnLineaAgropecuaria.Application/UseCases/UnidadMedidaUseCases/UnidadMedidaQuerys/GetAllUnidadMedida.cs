using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Application.DTOs.UnidadMedidaDTOs;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.UnidadMedidaUseCases.UnidadMedidaQuerys
{
    public class GetAllUnidadMedida
    {
        private readonly IRepositorioUnidadMedida repositorioUnidadMedida;

        public GetAllUnidadMedida(IRepositorioUnidadMedida repositorioUnidadMedida)
        {
            this.repositorioUnidadMedida = repositorioUnidadMedida;
        }

        public async Task<IEnumerable<UnidadMedidaDTO>> ExecuteAsync()
        {
            var unidadMedida = await repositorioUnidadMedida.Get();

            var unidadMedidaDTO = unidadMedida.Select(x => new UnidadMedidaDTO {
              Id = x.Id,
              Abreviatura = x.Abreviatura,
              Medida = x.Medida,
              TipoMedidaId = x.TipoMedidaId,
              TipoMedida = x.TipoMedida!.Nombre
            }).ToList();

            return unidadMedidaDTO;
        }
    }
}
