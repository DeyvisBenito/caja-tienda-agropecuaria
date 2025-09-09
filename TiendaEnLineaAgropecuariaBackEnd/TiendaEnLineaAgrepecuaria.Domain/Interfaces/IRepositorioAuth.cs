using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;
using TiendaEnLineaAgrepecuaria.Domain.ValueObjects;

namespace TiendaEnLineaAgrepecuaria.Domain.Interfaces
{
    public interface IRepositorioAuth
    {
        Task<LoginRespuestaValuesObject> LoginConEmail(Usuario usuario);
        Task<LoginRespuestaValuesObject> LoginConGoogle(string credenciales);
    }
}
