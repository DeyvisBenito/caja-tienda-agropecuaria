using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioCategorias
    {
        Task<bool> DeleteCategoria(int id);
        Task<IEnumerable<Categoria>> GetAllCategorias();
        Task<Categoria> GetCategoria(int id);
        Task<bool> NewCategoria(Categoria categoria);
        Task<bool> UpdateCategoria(int id, Categoria categoria);
    }
}
