using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaEnLineaAgrepecuaria.Domain.Entidades;

namespace TiendaEnLineaAgropecuariaAPI.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ProductosController: ControllerBase
    {
        public ProductosController()
        {
            
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            return Ok();
        }
    }
}
