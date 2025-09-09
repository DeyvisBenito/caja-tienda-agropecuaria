using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.ValueObjects
{
    public class LoginRespuestaValuesObject
    {
        public string? IdUsuario { get; set; }
        public bool EsExitoso { get; set; }
    }
}
