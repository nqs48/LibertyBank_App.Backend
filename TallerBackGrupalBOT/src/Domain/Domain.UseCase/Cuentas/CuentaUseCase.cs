using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using System;
using System.Threading.Tasks;

namespace Domain.UseCase.Cuentas
{
    /// <summary>
    /// Cuenta UseCase
    /// </summary>
    public class CuentaUseCase : ICuentaUseCase
    {

        private readonly ICuentaRepository _repositoryCuenta;
        private readonly IClienteRepository _clienteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuentaUseCase"/> class.
        /// </summary>
        /// <param name="repositoryCuenta">The logger.</param>
        public CuentaUseCase( ICuentaRepository repositoryCuenta, IClienteRepository clienteRepository)
        {
            _repositoryCuenta = repositoryCuenta;
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.CancelarCuenta(Cuenta)"/>
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> CancelarCuenta(Cuenta cuenta)
        {
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (cuentaEncontrada == null)
            {
                throw new Exception("No se encontró la cuenta");
            }
            cuentaEncontrada.CancelarCuenta();
            return cuentaEncontrada;

        }

        /// <summary>
        /// <see cref="ICuentaUseCase.DeshabilitarCuenta(Cuenta)"/>
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> DeshabilitarCuenta(Cuenta cuenta)
        {
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (cuentaEncontrada == null)
            {
                throw new Exception("No se encontró la cuenta");
            }
            cuentaEncontrada.DeshabilitarCuenta();
            return cuentaEncontrada;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.HabilitarCuenta(Cuenta)"/>
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> HabilitarCuenta(Cuenta cuenta)
        {
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (cuentaEncontrada == null)
            {
                throw new Exception("No se encontró la cuenta");
            }
            cuentaEncontrada.HabilitarCuenta();
            return cuentaEncontrada;
        }

        
    }
}