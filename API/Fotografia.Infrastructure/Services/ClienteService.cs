
using Fotografia.Domain.Entities;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Contract;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fotografia.Infrastructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task RegistrarClienteAsync(Cliente cliente)
        {

            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new ArgumentException(
                    "El nombre del cliente es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
            {
                throw new ArgumentException(
                    "El apellido del cliente es obligatorio.");
            }

            cliente.Nombre = cliente.Nombre.Trim();
            cliente.Apellido = cliente.Apellido.Trim();

            if (cliente.Nombre.Length < 2 ||
                cliente.Nombre.Length > 100)
            {
                throw new ArgumentException(
                    "El nombre debe tener entre 2 y 100 caracteres.");
            }

            if (cliente.Apellido.Length < 2 ||
                cliente.Apellido.Length > 100)
            {
                throw new ArgumentException(
                    "El apellido debe tener entre 2 y 100 caracteres.");
            }

            cliente.Correo = string.IsNullOrWhiteSpace(cliente.Correo)
                ? null
                : cliente.Correo.Trim().ToLowerInvariant();

            cliente.Telefono = string.IsNullOrWhiteSpace(cliente.Telefono)
                ? null
                : cliente.Telefono.Trim();

            if (cliente.Correo == null &&
                cliente.Telefono == null)
            {
                throw new ArgumentException(
                    "Debe proporcionar un correo o teléfono.");
            }

            if (cliente.Correo != null)
            {
                if (!CorreoValido(cliente.Correo))
                {
                    throw new ArgumentException(
                        "El formato del correo electrónico no es válido.");
                }

                bool correoExiste = await _clienteRepository
                    .ExisteCorreoAsync(cliente.Correo);

                if (correoExiste)
                {
                    throw new InvalidOperationException(
                        "Ya existe un cliente con ese correo.");
                }
            }


            if (cliente.Telefono != null)
            {

                string telefonoNormalizado = cliente.Telefono
                    .Replace("-", "")
                    .Replace(" ", "")
                    .Replace("(", "")
                    .Replace(")", "");

                if (telefonoNormalizado.Length != 10 ||
                    !telefonoNormalizado.All(char.IsDigit))
                {
                    throw new ArgumentException(
                        "El teléfono debe contener 10 dígitos.");
                }

                cliente.Telefono = telefonoNormalizado;

                bool telefonoExiste = await _clienteRepository
                    .ExisteTelefonoAsync(cliente.Telefono);

                if (telefonoExiste)
                {
                    throw new InvalidOperationException(
                        "Ya existe un cliente con ese teléfono.");
                }
            }

            await _clienteRepository.AgregarAsync(cliente);
        }

        private static bool CorreoValido(string correo)
        {
            try
            {
                var direccion = new MailAddress(correo);

                return direccion.Address.Equals(
                    correo,
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
