using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.Interfaces;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Repositorios.RepositorioUnidadMedida
{
    public class RepositorioUnidadMedida: IRepositorioUnidadMedida
    {
        private readonly ApplicationDBContext dbContext;

        public RepositorioUnidadMedida(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UnidadMedida>> Get()
        {
            var unidadesMedida = await dbContext.UnidadesMedida.Include(x => x.TipoMedida).ToListAsync();

            return unidadesMedida;
        }
    }
}
