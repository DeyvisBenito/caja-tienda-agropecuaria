using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioAuth
    {
        Task<bool> LoginConEmail(Usuario usuario);
        Task<bool> LoginConGoogle(string credenciales);
    }
}
