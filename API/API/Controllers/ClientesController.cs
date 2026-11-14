
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/fotografia/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente(
            [FromBody] ClienteCreateDto datos)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nombre = datos.Nombre,
                    Apellido = datos.Apellido,
                    Telefono = datos.Telefono,
                    Correo = datos.Correo
                };

                await _clienteService.RegistrarClienteAsync(cliente);

                return Created(
                    $"/api/fotografia/clientes/{cliente.Id}",
                    new
                    {
                        mensaje = "Cliente registrado correctamente.",
                        clienteId = cliente.Id
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    mensaje = ex.Message
                });
            }
        }
    }
}
