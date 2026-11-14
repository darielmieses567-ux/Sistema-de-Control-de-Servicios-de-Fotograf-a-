
using Fotografia.Domain.Entities;
using System.Threading.Tasks;

namespace Fotografia.Infrastructure.Contract
{
    public interface IClienteService
    {
        Task RegistrarClienteAsync(Cliente cliente);
    }
}
