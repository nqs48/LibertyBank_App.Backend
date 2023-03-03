using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuentaUseCase"/> class.
        /// </summary>
        /// <param name="repositoryCuenta">The logger.</param>
        /// <param name="clienteRepository">The logger.</param>
        /// <param name="usuarioRepository">The logger.</param>
        public CuentaUseCase( ICuentaRepository repositoryCuenta, IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
        {
            _repositoryCuenta = repositoryCuenta;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.CancelarCuenta(string,Cuenta)"/>
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> CancelarCuenta(string idUsuarioModificacion, Cuenta cuenta)
        {
            var tipoModificacion= TipoModificación.Cancelación;
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (usuario == null || cuentaEncontrada == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }else if (usuario.Rol.Equals(0))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            Modificación nuevaModificacion = new Modificación(tipoModificacion, usuario);
            cuentaEncontrada.CancelarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return cuentaEncontrada;

        }

        /// <summary>
        /// <see cref="ICuentaUseCase.DeshabilitarCuenta(string,Cuenta)"/>
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> DeshabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (usuario == null || cuentaEncontrada == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }
            else if (usuario.Rol.Equals(0))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            Modificación nuevaModificacion = new Modificación(TipoModificación.Inhabilitación, usuario);
            cuentaEncontrada.DeshabilitarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return cuentaEncontrada;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.HabilitarCuenta(string,Cuenta)"/>
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> HabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta)
        {
            var tipoModificacion = TipoModificación.Habilitación;
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (usuario == null || cuentaEncontrada == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }
            else if (usuario.Rol.Equals(0))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            Modificación nuevaModificacion = new Modificación(tipoModificacion, usuario);
            cuentaEncontrada.HabilitarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return cuentaEncontrada;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.ObtenerCuentaPorId(string)"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> ObtenerCuentaPorId(string idCuenta)
        {
            var cuenta = await _repositoryCuenta.ObtenerPorId(idCuenta);
            if (cuenta == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                                                  (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }
            return cuenta;
        }

        /// <summary>
        /// Método para crear una Cuenta
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> Crear(string idUsuarioModificacion,Cuenta cuenta)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            if (usuario == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }
            else if (usuario.Rol.Equals(Roles.Transaccional))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            Modificación nuevaModificacion = new Modificación(TipoModificación.Creacion, usuario);
            cuenta.AgregarModificacion(nuevaModificacion);
            return await _repositoryCuenta.Crear(cuenta);
        }
    }
}