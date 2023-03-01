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
        /// <param name="idCuenta"></param> 
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> CancelarCuenta(string idCuenta);

        /// <summary>
        /// Habilitar una Cuenta.
        /// <param name="idCuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> HabilitarCuenta(string idCuenta);

        /// <summary>
        /// Deshabilitar una Cuenta.
        /// <param name="idCuenta"></param>
        /// </summary>
        /// <returns></returns>
        Task<Cuenta> DeshabilitarCuenta(string idCuenta);
    }
}
