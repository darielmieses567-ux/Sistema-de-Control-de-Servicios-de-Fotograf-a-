
using Microsoft.AspNetCore.Mvc;
using Fotografia.Infrastructure.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fotografia/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        // Reporte de ingresos
        [HttpGet("ingresos")]
        public async Task<IActionResult> ObtenerIngresos(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            try
            {
                var total = await _reporteService
                    .ObtenerIngresosAsync(fechaInicio, fechaFin);

                return Ok(new
                {
                    fechaInicio,
                    fechaFin,
                    totalIngresos = total
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Reporte de sesiones por fotógrafo
        [HttpGet("sesiones-por-fotografo")]
        public async Task<IActionResult>
            ObtenerSesionesPorFotografo()
        {
            var datos = await _reporteService
                .ObtenerSesionesPorFotografoAsync();

            return Ok(datos);
        }

        // Reporte de pagos pendientes
        [HttpGet("pagos-pendientes")]
        public async Task<IActionResult>
            ObtenerPagosPendientes()
        {
            var datos = await _reporteService
                .ObtenerPagosPendientesAsync();

            return Ok(datos);
        }
    }
}
