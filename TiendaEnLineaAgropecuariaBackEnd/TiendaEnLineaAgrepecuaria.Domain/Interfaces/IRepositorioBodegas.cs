using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioBodegas
    {
        Task<bool> DeleteBodega(int id);
        Task<IEnumerable<Bodega>> GetAllBodegas();
        Task<Bodega> GetBodegaById(int id);
        Task<bool> NewBodega(Bodega bodega);
        Task<bool> UpdateBodega(int id, Bodega bodega);
    }
}
