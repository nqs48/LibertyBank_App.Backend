using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Interfaz de repositorio de entidad <see cref="Transacción"/>
    /// </summary>
    public interface ITransacciónRepository
    {
        /// <summary>
        /// Método para obtener una entidad de tipo <see cref="Transacción"/>
        /// </summary>
        /// <param name="idTransacción"></param>
        /// <returns></returns>
        Task<Transacción> ObtenerPorId(string idTransacción);

        /// <summary>
        /// Método para obtener una lista de transacciones por el id de la entidad <see cref="Cuenta"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        Task<List<Transacción>> ObtenerPorIdCuenta(string idCuenta);

        /// <summary>
        /// Método para crear una entidad de tipo <see cref="Transacción"/>
        /// </summary>
        /// <param name="transacción"></param>
        /// <returns></returns>
        Task<Transacción> Crear(Transacción transacción);
    }
}