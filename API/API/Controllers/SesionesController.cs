
using Microsoft.AspNetCore.Mvc;
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Contract;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fotografia/sesiones")]
    public class SesionesController : ControllerBase
    {
        private readonly ISesionService _sesionService;

        public SesionesController(ISesionService sesionService)
        {
            _sesionService = sesionService;
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarSesion(
            [FromBody] Sesion sesion)
        {
            try
            {
                await _sesionService.RegistrarSesionAsync(sesion);

                return Ok(new
                {
                    mensaje = "Sesión registrada correctamente.",
                    sesionId = sesion.Id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }


        [HttpPut("{id:int}/reprogramar")]
        public async Task<IActionResult> ReprogramarSesion(
            int id,
            [FromBody] DateTime nuevaFecha)
        {
            try
            {
                await _sesionService.ReprogramarSesionAsync(
                    id, nuevaFecha);

                return Ok(new
                {
                    mensaje = "Sesión reprogramada correctamente."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}/cancelar")]
        public async Task<IActionResult> CancelarSesion(int id)
        {
            try
            {
                await _sesionService.CancelarSesionAsync(id);

                return Ok(new
                {
                    mensaje = "Sesión cancelada correctamente."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }
    }
}
