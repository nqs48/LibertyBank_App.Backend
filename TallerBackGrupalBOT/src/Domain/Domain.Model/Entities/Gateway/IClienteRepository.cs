using Domain.Model.Entities.Clientes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObtenerTodos();

        Task<Cliente> ObtenerPorId(string IdCliente);

        Task<Cliente> Crear(Cliente cliente);

        Task<Cliente> Actualizar(string IdCliente, Cliente cliente);
    }
}