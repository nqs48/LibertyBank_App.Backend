using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Cuentas
{
    /// <summary>
    /// Interface Cuenta UseCase
    /// </summary>
    public interface ICuentaUseCase
    {
        /// <summary>
        /// Cancelar una Cuenta.
        /// <param name="idUsuarioModificacion"></param> 
        /// <param name="cuenta"></param> 
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> CancelarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Habilitar una Cuenta
        /// <param name="idUsuarioModificacion"></param> 
        /// <param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> HabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Deshabilitar una Cuenta
        /// <param name="idUsuarioModificacion"></param> 
        /// <param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> DeshabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta);

        /// <summary>
        /// Obtener una Cuenta por Id
        /// <param name="idCuenta"></param> 
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> ObtenerCuentaPorId(string idCuenta);

        /// <summary>
        /// Método para crear una Cuenta
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        Task<Cuenta> Crear(string idUsuarioModificacion,Cuenta cuenta);

        /// <summary>
        /// Método para obtener todas las cuentas
        /// </summary>
        /// <returns></returns>
        Task<List<Cuenta>> ObtenerTodas();

    }
}
