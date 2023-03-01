using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using System.Threading.Tasks;

namespace Domain.UseCase.Cuentas
{
    /// <summary>
    /// Cuenta UseCase
    /// </summary>
    public class CuentaUseCase : ICuentaUseCase
    {

        private readonly ICuentaRepository _repositoryCuenta;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuentaUseCase"/> class.
        /// </summary>
        /// <param name="repositoryCuenta">The logger.</param>
        public CuentaUseCase( ICuentaRepository repositoryCuenta)
        {
            _repositoryCuenta = repositoryCuenta;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.CancelarCuenta(string)"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        public Task<Cuenta> CancelarCuenta(string idCuenta)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.DeshabilitarCuenta(string)"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        public Task<Cuenta> DeshabilitarCuenta(string idCuenta)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.HabilitarCuenta(string)"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        public Task<Cuenta> HabilitarCuenta(string idCuenta)
        {
            throw new System.NotImplementedException();
        }
    }
}