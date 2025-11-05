using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgropecuaria.Application.UseCases.ReportesUseCases.ReportesQuerys;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private readonly GetBestClientesPorVentas getBestClientesPorVentas;
        private readonly GetVentasDelDia getVentasDelDia;
        private readonly GetComprasDelDia getComprasDelDia;
        private readonly GetBestProveedores getBestProveedores;

        public ReportesController(GetBestClientesPorVentas getBestClientesPorVentas, GetVentasDelDia getVentasDelDia, GetComprasDelDia getComprasDelDia,
                                  GetBestProveedores getBestProveedores)
        {
            this.getBestClientesPorVentas = getBestClientesPorVentas;
            this.getVentasDelDia = getVentasDelDia;
            this.getComprasDelDia = getComprasDelDia;
            this.getBestProveedores = getBestProveedores;
        }

        [HttpGet("bestClientesPorVentas")]
        public async Task<ActionResult<IEnumerable<object>>> GetBestClientesPorVentas()
        {
            try
            {
                var clientes = await getBestClientesPorVentas.ExecuteAsync();
                return Ok(clientes);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("bestProveedores")]
        public async Task<ActionResult<IEnumerable<object>>> GetBestProveedores()
        {
            try
            {
                var proveedores = await getBestProveedores.ExecuteAsync();
                return Ok(proveedores);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("ventasDelDia")]
        public async Task<ActionResult<IEnumerable<object>>> GetVentasDelDia()
        {
            try
            {
                var ventas = await getVentasDelDia.ExecuteAsync();
                return Ok(ventas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }

        [HttpGet("getComprasDelDia")]
        public async Task<ActionResult<IEnumerable<object>>> GetComprasDelDia()
        {
            try
            {
                var compras = await getComprasDelDia.ExecuteAsync();
                return Ok(compras);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return ValidationProblem();
            }
        }
    }
}
