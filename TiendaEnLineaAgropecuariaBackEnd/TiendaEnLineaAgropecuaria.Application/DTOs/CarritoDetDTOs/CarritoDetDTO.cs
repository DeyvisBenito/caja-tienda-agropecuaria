using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuaria.Application.DTOs.CarritoDetDTOs
{
    public class CarritoDetDTO
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int InventarioId { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime Fecha { get; set; }
        public bool HasConflict { get; set; }
        public Inventario? Inventario { get; set; }
    }
}
