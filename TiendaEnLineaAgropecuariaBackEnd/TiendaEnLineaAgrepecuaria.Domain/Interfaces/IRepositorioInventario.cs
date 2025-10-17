using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioInventario
    {
        //Task<bool> DeleteInventario(int id);
        Task<IEnumerable<Inventario>> Get();
        Task<Inventario> GetById(int id);
        Task<bool> NewInventario(Inventario inventario);
        // Task<bool> UpdateInventario(int id, Inventario inventario);
        Task<Inventario> GetByCodigo(string codigo, int sucursalId);
    }
}
