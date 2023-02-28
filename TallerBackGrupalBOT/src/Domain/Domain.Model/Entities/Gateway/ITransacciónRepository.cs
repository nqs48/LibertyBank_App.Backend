using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface ITransacciónRepository
    {
        Task<List<Transacción>> ObtenerTodos();

        Task<Transacción> ObtenerPorId(string IdTransacción);

        Task<Transacción> ObtenerPorIdCuenta(string IdCuenta);

        Task<Transacción> Crear(Transacción transacción);
    }
}