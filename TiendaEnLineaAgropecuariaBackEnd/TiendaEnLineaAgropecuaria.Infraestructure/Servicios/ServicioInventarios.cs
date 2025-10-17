using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgropecuaria.Infraestructure.Datos;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class ServicioInventarios
    {
        private readonly ApplicationDBContext dbContext;

        public ServicioInventarios(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> ObtenerStock(int id)
        {
            var inventario = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);
            if(inventario is null)
            {
                throw new KeyNotFoundException("El producto no existe en el inventario");
            }

            return inventario.Stock;
        }
    }
}
