using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Transacciones
{
    public interface ITransacciónUseCase
    {
        Task<Transacción> ObtenerTransacciónPorId(string idTransacción);

        Task<List<Transacción>> ObtenerTransaccionesPorIdCuenta(string idCuenta);

        Task<Transacción> RealizarConsignación(Transacción transacción);

        Task<Transacción> RealizarRetiro(Transacción transacción);

        Task<Transacción> RealizarTransferencia(Transacción transacción, string idCuentaReceptor);
    }
}