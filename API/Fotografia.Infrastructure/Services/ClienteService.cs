
using Fotografia.Domain.Entities;
using Fotografia.Domain.Repository;

namespace Fotografia.Infrastructure.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task RegistrarClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException(
                    "El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                throw new ArgumentException(
                    "El apellido del cliente es obligatorio.");

            cliente.Nombre = cliente.Nombre.Trim();
            cliente.Apellido = cliente.Apellido.Trim();

            cliente.Correo = string.IsNullOrWhiteSpace(cliente.Correo)
                ? null
                : cliente.Correo.Trim().ToLowerInvariant();

            cliente.Telefono = string.IsNullOrWhiteSpace(cliente.Telefono)
                ? null
                : cliente.Telefono.Trim();

            if (cliente.Correo == null && cliente.Telefono == null)
                throw new ArgumentException(
                    "Debe proporcionar un correo o teléfono.");

            if (cliente.Correo != null)
            {
                bool correoExiste = await _clienteRepository
                    .ExisteCorreoAsync(cliente.Correo);

                if (correoExiste)
                    throw new InvalidOperationException(
                        "Ya existe un cliente con ese correo.");
            }

            if (cliente.Telefono != null)
            {
                bool telefonoExiste = await _clienteRepository
                    .ExisteTelefonoAsync(cliente.Telefono);

                if (telefonoExiste)
                    throw new InvalidOperationException(
                        "Ya existe un cliente con ese teléfono.");
            }

            await _clienteRepository.AgregarAsync(cliente);
        }
    }
}
