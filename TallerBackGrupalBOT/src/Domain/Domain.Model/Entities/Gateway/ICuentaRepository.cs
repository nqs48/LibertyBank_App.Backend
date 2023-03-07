using Domain.Model.Entities.Cuentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Repositorio de la cuenta
    /// </summary>
    public interface ICuentaRepository
    {
        /// <summary>
        /// Obtiene todos las cuentas
        /// </summary>
        /// <returns></returns>
        Task<List<Cuenta>> ObtenerTodos();

        /// <summary>
        /// Obtiene cuenta por Id
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <returns></returns>
        Task<Cuenta> ObtenerPorId(string IdCuenta);

        /// <summary>
        /// Crea una nueva cuenta
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        Task<Cuenta> Crear(Cuenta cuenta);


        /// <summary>
        /// Actualiza datos de la cuenta
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        Task<Cuenta> Actualizar(string IdCuenta, Cuenta cuenta);
    }
}