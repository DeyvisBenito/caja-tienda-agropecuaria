using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgrepecuaria.Domain.Entidades
{
    public class Traslado
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int SucursalOrigenId { get; set; }
        public Sucursal? SucursalOrigen { get; set; }
        public int SucursalDestinoId { get; set; }
        public Sucursal? SucursalDestino { get; set; }
        public required string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
