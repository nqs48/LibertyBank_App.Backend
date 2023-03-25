using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Transacciones
{
    /// <summary>
    /// Interfaz de caso de uso de entidad <see cref="Transacción"/>
    /// </summary>
    public interface ITransacciónUseCase
    {
        /// <summary>
        /// Método para obtener una entidad de tipo <see cref="Transacción"/> por su Id
        /// </summary>
        /// <param name="idTransacción"></param>
        /// <returns></returns>
        Task<Transacción> ObtenerTransacciónPorId(string idTransacción);

        /// <summary>
        /// Método para obtener una lista de entidad <see cref="Transacción"/> por el Id de tipo
        /// <see cref="Cuenta"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        Task<List<Transacción>> ObtenerTransaccionesPorIdCuenta(string idCuenta);

        /// <summary>
        /// Método para realizar una consignación
        /// </summary>
        /// <param name="transacción"></param>
        /// <returns></returns>
        Task<Transacción> RealizarConsignación(Transacción transacción);

        /// <summary>
        /// Método para realizar un retiro
        /// </summary>
        /// <param name="transacción"></param>
        /// <returns></returns>
        Task<Transacción> RealizarRetiro(Transacción transacción);

        /// <summary>
        /// Método para realizar una transferencia
        /// </summary>
        /// <param name="transacción"></param>
        /// <param name="idCuentaReceptor"></param>
        /// <returns></returns>
        Task<Transacción> RealizarTransferencia(Transacción transacción, string idCuentaReceptor);
    }
}