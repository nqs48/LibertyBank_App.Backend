﻿using credinet.exception.middleware.models;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Extensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IOptions<ConfiguradorAppSettings> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CuentaUseCase"/> class.
        /// </summary>
        /// <param name="repositoryCuenta">The logger.</param>
        /// <param name="clienteRepository">The logger.</param>
        /// <param name="usuarioRepository">The logger.</param>
        public CuentaUseCase(ICuentaRepository repositoryCuenta, IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository, IOptions<ConfiguradorAppSettings> options)
        {
            _repositoryCuenta = repositoryCuenta;
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;

            _options = options;
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.CancelarCuenta(string,Cuenta)"/>
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> CancelarCuenta(string idUsuarioModificacion, Cuenta cuenta)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);

            if (usuario is null)
                throw new BusinessException(TipoExcepcionNegocio.UsuarioNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.UsuarioNoExiste);

            if (cuentaEncontrada is null)
                throw new BusinessException(TipoExcepcionNegocio.CuentaNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.CuentaNoEncontrada);

            if (usuario.Rol.Equals(Roles.Transaccional))
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);

            if (cuentaEncontrada.Saldo >= 1)
                throw new BusinessException(TipoExcepcionNegocio.CuentaConSaldo.GetDescription(),
                                                   (int)TipoExcepcionNegocio.CuentaConSaldo);

            Modificación nuevaModificacion = new Modificación(TipoModificación.Cancelación, usuario);
            cuentaEncontrada.CancelarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return await _repositoryCuenta.Actualizar(cuentaEncontrada.Id, cuentaEncontrada);
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
            if (usuario == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.UsuarioNoExiste);
            }
            else if (cuentaEncontrada == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.CuentaNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.CuentaNoEncontrada);
            }
            else if (usuario.Rol.Equals(Roles.Transaccional))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            else if (cuentaEncontrada.EstadoCuenta.Equals(EstadoCuenta.Inactiva))
            {
                throw new BusinessException(TipoExcepcionNegocio.CuentaEstaInactiva.GetDescription(),
                                (int)TipoExcepcionNegocio.CuentaEstaInactiva);
            }
            Modificación nuevaModificacion = new Modificación(TipoModificación.Inhabilitación, usuario);
            cuentaEncontrada.DeshabilitarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return await _repositoryCuenta.Actualizar(cuentaEncontrada.Id, cuentaEncontrada);
        }

        /// <summary>
        /// <see cref="ICuentaUseCase.HabilitarCuenta(string,Cuenta)"/>
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> HabilitarCuenta(string idUsuarioModificacion, Cuenta cuenta)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cuentaEncontrada = await _repositoryCuenta.ObtenerPorId(cuenta.Id);
            if (usuario == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.UsuarioNoExiste);
            }
            else if (cuentaEncontrada == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.CuentaNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.CuentaNoEncontrada);
            }
            else if (usuario.Rol.Equals(Roles.Transaccional))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            else if (cuentaEncontrada.EstadoCuenta.Equals(EstadoCuenta.Activa))
            {
                throw new BusinessException(TipoExcepcionNegocio.CuentaEstaActiva.GetDescription(),
                                (int)TipoExcepcionNegocio.CuentaEstaActiva);
            }
            Modificación nuevaModificacion = new Modificación(TipoModificación.Habilitación, usuario);
            cuentaEncontrada.HabilitarCuenta();
            cuentaEncontrada.AgregarModificacion(nuevaModificacion);
            return await _repositoryCuenta.Actualizar(cuentaEncontrada.Id, cuentaEncontrada);
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
                throw new BusinessException(TipoExcepcionNegocio.CuentaNoEncontrada.GetDescription(),
                               (int)TipoExcepcionNegocio.CuentaNoEncontrada);
            }
            return cuenta;
        }

        /// <summary>
        /// Método para crear una Cuenta
        /// </summary>
        /// <param name="idUsuarioModificacion"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> Crear(string idUsuarioModificacion, Cuenta cuenta)
        {
            if (!cuenta.Exenta) { cuenta.CalcularSaldoDisponible(_options.Value.GMF); }
            else { cuenta.SaldoDisponible = cuenta.Saldo; }
            cuenta.AsignarNumeroCuenta();

            var cuentasCliente = await _repositoryCuenta.ObtenerPorCliente(cuenta.IdCliente);
            var cuentasExentas = cuentasCliente.Where(x => x.Exenta).ToList();

            var usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuarioModificacion);
            var cliente = await _clienteRepository.ObtenerPorIdAsync(cuenta.IdCliente);
            if (usuario == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.UsuarioNoExiste);
            }
            else if (cliente == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.ClienteNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.ClienteNoExiste);
            }
            else if (usuario.Rol.Equals(Roles.Transaccional))
            {
                throw new BusinessException(TipoExcepcionNegocio.UsuarioSinPermisos.GetDescription(),
                                (int)TipoExcepcionNegocio.UsuarioSinPermisos);
            }
            else if (cuentasExentas.Count > 0 && cuenta.Exenta)
            {
                throw new BusinessException(TipoExcepcionNegocio.YaExisteUnaCuentaExenta.GetDescription(),
                                                  (int)TipoExcepcionNegocio.YaExisteUnaCuentaExenta);
            }

            var nuevaModificacion = new Modificación(TipoModificación.Creación, usuario);
            var nuevaActualizacion = new Actualización(TipoActualización.Actualización, usuario);
            cuenta.AgregarModificacion(nuevaModificacion);
            cliente.AgregarIdProducto(cuenta.NumeroCuenta);
            cliente.AgregarActualizacion(nuevaActualizacion);
            await _clienteRepository.ActualizarAsync(cliente.Id, cliente);
            return await _repositoryCuenta.Crear(cuenta);
        }

        /// <summary>
        /// Método para obtener todas la cuentas
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cuenta>> ObtenerTodas()
        {
            return await _repositoryCuenta.ObtenerTodos();
        }

        /// <summary>
        /// Método para obtener todas la cuentas por Cliente
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cuenta>> ObtenerTodasPorCliente(string idCliente)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(idCliente);
            if (cliente == null)
            {
                throw new BusinessException(TipoExcepcionNegocio.ClienteNoExiste.GetDescription(),
                               (int)TipoExcepcionNegocio.ClienteNoExiste);
            }

            var cuentasCliente = await _repositoryCuenta.ObtenerPorCliente(idCliente);

            //Ordenar Cuentas por Saldo
            return cuentasCliente.OrderByDescending(x => x.Saldo).ToList();
        }
    }
}