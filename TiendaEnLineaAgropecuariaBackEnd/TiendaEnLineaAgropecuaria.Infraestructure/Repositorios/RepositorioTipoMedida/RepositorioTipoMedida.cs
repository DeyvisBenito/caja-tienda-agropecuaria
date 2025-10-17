using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioTipoMedida
{
    public class RepositorioTipoMedida: IRepositorioTipoMedida
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioTipoMedida(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TipoMedida>> Get()
        {
            var tiposMedida = await dbContext.TiposMedida.ToListAsync();

            return tiposMedida;
        }
    }
}
