using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.VentasDTOs
{
    public class PagoDTO
    {
        [Required]
        public decimal Pago { get; set; }
    }
}
