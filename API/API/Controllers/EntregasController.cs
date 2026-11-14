
using Microsoft.AspNetCore.Mvc;
using Fotografia.Infrastructure.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fotografia/entregas")]
    public class EntregasController : ControllerBase
    {
        private readonly EntregaService _entregaService;

        public EntregasController(EntregaService entregaService)
        {
            _entregaService = entregaService;
        }

        // Programar fecha estimada de entrega
        [HttpPut("sesion/{id:int}/programar")]
        public async Task<IActionResult> ProgramarEntrega(
            int id,
            [FromBody] DateTime fechaEstimada)
        {
            try
            {
                await _entregaService.ProgramarEntregaAsync(
                    id, fechaEstimada);

                return Ok(new
                {
                    mensaje = "Fecha de entrega programada correctamente."
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

        // Marcar como entregada
        [HttpPut("sesion/{id:int}/completar")]
        public async Task<IActionResult> CompletarEntrega(int id)
        {
            try
            {
                await _entregaService.MarcarEntregadaAsync(id);

                return Ok(new
                {
                    mensaje = "Entrega completada correctamente."
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        // Consultar entregas pendientes
        [HttpGet("pendientes")]
        public async Task<IActionResult> ObtenerPendientes()
        {
            var sesiones = await _entregaService
                .ObtenerPendientesAsync();

            return Ok(sesiones.Select(s => new
            {
                s.Id,
                s.ClienteId,
                s.FotografoId,
                s.FechaSesion,
                s.FechaEstimadaEntrega,
                s.Entregado,
                s.Estado
            }));
        }

        // Consultar entregas atrasadas
        [HttpGet("atrasadas")]
        public async Task<IActionResult> ObtenerAtrasadas()
        {
            var sesiones = await _entregaService
                .ObtenerAtrasadasAsync();

            return Ok(sesiones.Select(s => new
            {
                s.Id,
                s.ClienteId,
                s.FotografoId,
                s.FechaEstimadaEntrega,
                s.Entregado,
                s.Estado
            }));
        }
    }
}
