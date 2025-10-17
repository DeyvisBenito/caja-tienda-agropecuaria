using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;

namespace TiendaEnLineaAgropecuaria.Application.UseCases.TiposMedidaUseCases.TiposMedidaQuerys
{
    public class GetAllTiposMedida
    {
        private readonly IRepositorioTipoMedida repositorioTipoMedida;

        public GetAllTiposMedida(IRepositorioTipoMedida repositorioTipoMedida)
        {
            this.repositorioTipoMedida = repositorioTipoMedida;
        }

        public async Task<IEnumerable<TipoMedida>> ExecuteAsync()
        {
            var tiposMedida = await repositorioTipoMedida.Get();

            return tiposMedida;
        }
    }
}
