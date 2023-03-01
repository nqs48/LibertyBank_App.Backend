using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface ITransacciónRepository
    {
        Task<Transacción> ObtenerPorId(string IdTransacción);

        Task<List<Transacción>> ObtenerPorIdCuenta(string IdCuenta);

        Task<Transacción> Crear(Transacción transacción);
    }
}