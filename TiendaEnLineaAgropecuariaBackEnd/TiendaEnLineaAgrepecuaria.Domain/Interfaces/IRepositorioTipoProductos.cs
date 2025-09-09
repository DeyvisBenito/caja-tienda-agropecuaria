using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioTipoProductos
    {
        Task<bool> DeleteTipoProducto(int id);
        Task<IEnumerable<TipoProducto>> GetAllTipoProductos();
        Task<TipoProducto> GetTipoProducto(int id);
        Task<bool> NewTipoProducto(TipoProducto tipoProducto);
        Task<bool> UpdateTipoProducto(int id, TipoProducto tipoProducto);
    }
}
