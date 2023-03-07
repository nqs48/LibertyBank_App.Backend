using Domain.Model.Entities.Cuentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Interfaz de repositorio de entidad <see cref="Cuenta"/>
    /// </summary>
    public interface ICuentaRepository
    {
        /// <summary>
        /// Método para obtener entidades de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <returns></returns>
        Task<List<Cuenta>> ObtenerTodos();

        /// <summary>
        /// Método para obtener una entidad de tipo <see cref="Cuenta"/> por su Id
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        Task<Cuenta> ObtenerPorId(string idCuenta);

        /// <summary>
        /// Método para crear una entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        Task<Cuenta> Crear(Cuenta cuenta);

        /// <summary>
        /// Método para actualizar una entidad de tipo <see cref="Cuenta"/> por su Id
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        Task<Cuenta> Actualizar(string idCuenta, Cuenta cuenta);
    }
}