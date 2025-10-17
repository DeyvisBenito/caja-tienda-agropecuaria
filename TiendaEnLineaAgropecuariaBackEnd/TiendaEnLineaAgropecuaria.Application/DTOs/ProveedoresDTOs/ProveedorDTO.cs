using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.ProveedoresDTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public required string Nit { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public required string Telefono { get; set; }
        public required string Ubicacion { get; set; }
        public int EstadoId { get; set; }
        public required string Estado { get; set; }
    }
}
