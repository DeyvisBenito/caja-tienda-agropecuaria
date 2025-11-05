using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioReportes
    {
        Task<IEnumerable<object>> BestClientesPorVentas();
        Task<IEnumerable<object>> BestProveedores();
        Task<IEnumerable<object>> ComprasDelDia();
        Task<IEnumerable<object>> VentasDelDia();
    }
}
