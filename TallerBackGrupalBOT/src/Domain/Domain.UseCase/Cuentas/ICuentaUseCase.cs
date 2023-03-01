using Domain.Model.Entities.Cuentas;
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
        /// <param name="cuenta"></param> 
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> CancelarCuenta(Cuenta cuenta);

        /// <summary>
        /// Habilitar una Cuenta.
        /// <param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> HabilitarCuenta(Cuenta cuenta);

        /// <summary>
        /// Deshabilitar una Cuenta.
        /// <param name="cuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> DeshabilitarCuenta(Cuenta cuenta);
    }
}
