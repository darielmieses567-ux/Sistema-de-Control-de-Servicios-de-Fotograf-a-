
using API.DTOs;
using Fotografia.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fotografia/pagos")]
    public class PagosController : ControllerBase
    {
        private readonly PagoService _pagoService;

        public PagosController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPago(
            [FromBody] PagoCreateDto datos)
        {
            try
            {
                await _pagoService.RegistrarPagoAsync(
                    datos.SesionId,
                    datos.Monto,
                    datos.MetodoPago,
                    datos.Observacion);

                return Ok(new
                {
                    mensaje = "Pago registrado correctamente."
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

        [HttpGet("sesion/{sesionId:int}")]
        public async Task<IActionResult> ObtenerPagos(
            int sesionId)
        {
            var pagos = await _pagoService
                .ObtenerPagosAsync(sesionId);

            return Ok(pagos.Select(p => new
            {
                p.Id,
                p.SesionId,
                p.FechaPago,
                p.Monto,
                p.MetodoPago,
                p.Observacion
            }));
        }

        [HttpGet("sesion/{sesionId:int}/saldo")]
        public async Task<IActionResult> ObtenerSaldo(
            int sesionId)
        {
            try
            {
                var saldo = await _pagoService
                    .ObtenerSaldoPendienteAsync(sesionId);

                return Ok(new
                {
                    sesionId,
                    saldoPendiente = saldo
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
