using Domain.Model.Entities.Transacciones;
using System.Threading.Tasks;

namespace Domain.UseCase.Transacciones
{
    public interface ITransacciónUseCase
    {
        Task<Transacción> RealizarConsignación(Transacción transacción);

        Task<Transacción> RealizarRetiro(Transacción transacción);

        Task<Transacción> RealizarTransferencia(Transacción transacción, string idCuentaReceptor);
    }
}