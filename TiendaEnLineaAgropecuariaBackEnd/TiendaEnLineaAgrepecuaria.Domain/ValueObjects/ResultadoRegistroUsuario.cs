using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.ValueObjects
{
    public class ResultadoRegistroUsuario
    {
        public bool EsExitoso { get; set; }
        public string? IdUsuario { get; set; }
        public int? SucursalId { get; set; }
        public List<string> Errores { get; set; } = [];
    }
}
