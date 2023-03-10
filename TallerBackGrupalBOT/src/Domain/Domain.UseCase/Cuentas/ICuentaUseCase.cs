using Domain.Model.Entities.Cuentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Cuentas
{
    /// <summary>
    /// Interface Cuenta UseCase
    /// </summary>
    public interface ICuentaUseCase
    {
        /// <summary>
        /// Cancelar una Cuenta. <param name="idUsuarioModificacion"></param><param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> CancelarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Habilitar una Cuenta <param name="idUsuarioModificacion"></param><param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> HabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Deshabilitar una Cuenta <param name="idUsuarioModificacion"></param><param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> DeshabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Obtener una Cuenta por Id <param name="idCuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> ObtenerCuentaPorId(string idCuenta);

        /// <summary>
        /// Método para crear una Cuenta
        /// </summary>
        /// <param name="cuenta"></param>
        /// <param name="idUsuarioModificacion"></param>
        /// <returns></returns>
        Task<Cuenta> Crear(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Método para obtener todas las cuentas
        /// </summary>
        /// <returns></returns>
        Task<List<Cuenta>> ObtenerTodas();

        /// <summary>
        /// Método para obtener todas las cuentas de un cliente <param name="idCliente"></param>
        /// </summary>
        /// <returns></returns>
        Task<List<Cuenta>> ObtenerTodasPorCliente(string idCliente);
    }
}