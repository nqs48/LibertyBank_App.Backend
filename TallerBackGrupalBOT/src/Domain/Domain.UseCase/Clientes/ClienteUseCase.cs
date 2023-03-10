using credinet.exception.middleware.models;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Helpers.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Clientes
{
    /// <summary>
    /// <see cref="IClienteUseCase"/>
    /// </summary>
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteRepository _gatewayCliente;

        private readonly ICuentaRepository _gatewayCuenta;

        private readonly IUsuarioRepository _gatewayUsuario;

        /// <summary>
        /// Constructor para inyectar gateway
        /// </summary>
        /// <param name="gatewayCliente"></param>
        /// <param name="gatewayCuenta"></param>
        /// <param name="gatewayUsuario"></param>
        public ClienteUseCase(IClienteRepository gatewayCliente, ICuentaRepository gatewayCuenta, IUsuarioRepository gatewayUsuario)
        {
            _gatewayCliente = gatewayCliente;
            _gatewayCuenta = gatewayCuenta;
            _gatewayUsuario = gatewayUsuario;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.ActualizarCorreoElectronico(string, string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="nuevoCorreo"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<Cliente> ActualizarCorreoElectronico(string idCliente, string nuevoCorreo)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            clienteSeleccionado.CambiarCorreoElectronico(nuevoCorreo);

            if (!clienteSeleccionado.VerificarCampoCorreo())
                throw new BusinessException($"Correo electrónico nuevo no valido", (int)TipoExcepcionNegocio.CorreoElectronicoNoValido);

            return await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);
        }

        /// <summary>
        /// <see cref="IClienteUseCase.AgregarProductosCliente(string, Cuenta)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="nuevaCuenta"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<Cliente> AgregarProductosCliente(string idCliente, Cuenta nuevaCuenta)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            clienteSeleccionado.AgregarIdProducto(nuevaCuenta.Id);

            return await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);
        }

        /// <summary>
        /// <see cref="IClienteUseCase.CrearCliente(string, Cliente)"/>
        /// </summary>
        /// <param name="idUsuarioCreacion"></param>
        /// <param name="nuevoCliente"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<Cliente> CrearCliente(string idUsuarioCreacion, Cliente nuevoCliente)
        {
            var clienteVerificacion = await _gatewayCliente.ObtenerPorNumeroIdentificacion(nuevoCliente.NumeroIdentificación);

            Usuario usuarioSeleccionado = await _gatewayUsuario.ObtenerPorIdAsync(idUsuarioCreacion);

            if (usuarioSeleccionado is null)
                throw new BusinessException($"No se encontró un usuario para la creación",
                    (int)TipoExcepcionNegocio.UsuarioNoValido);

            if (usuarioSeleccionado.Rol != Roles.Admin)
                throw new BusinessException($"El usuario {usuarioSeleccionado.NombreCompleto} no puede crear nuevos clientes",
                    (int)TipoExcepcionNegocio.UsuarioNoValido);

            if (clienteVerificacion is not null)
                throw new BusinessException($"Cliente con numero de identificación {nuevoCliente.NumeroIdentificación} ya existe",
                    (int)TipoExcepcionNegocio.IdentificacionDeClienteYaExiste);

            if (!nuevoCliente.VerificarEdadCliente(EdadLegal.col))
                throw new BusinessException($"El cliente debe ser mayor de edad",
                    (int)TipoExcepcionNegocio.ClienteNoEsMayorDeEdad);

            if (!nuevoCliente.VerificarCampoCorreo())
                throw new BusinessException($"El correo electrónico {nuevoCliente.CorreoElectronico} no es valido",
                    (int)TipoExcepcionNegocio.CorreoElectronicoNoValido);

            Actualización nuevaActualizacion = new(TipoActualización.Creación, usuarioSeleccionado);

            nuevoCliente.AgregarActualizacion(nuevaActualizacion);

            nuevoCliente.Habilitar();
            nuevoCliente.FechaCreación = DateTime.Now;

            return await _gatewayCliente.CrearAsync(usuarioSeleccionado.Id, nuevoCliente);
        }

        /// <summary>
        /// <see cref="IClienteUseCase.DeshabilitarCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> DeshabilitarCliente(string idCliente)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            if (clienteSeleccionado.TieneDeudasActivas)
                throw new BusinessException($"Cliente no es posible deshabilitar por deudas activas",
                    (int)TipoExcepcionNegocio.ClienteTieneDeudasActivas);

            clienteSeleccionado.Deshabilitar();
            await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);

            return true;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.DeshabilitarDeudaCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> DeshabilitarDeudaCliente(string idCliente)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            clienteSeleccionado.DeshabilitarDeuda();
            await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);

            return true;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.HabilitarCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> HabilitarCliente(string idCliente)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            clienteSeleccionado.Habilitar();
            await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);

            return true;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.HabilitarDeudaCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<bool> HabilitarDeudaCliente(string idCliente)
        {
            var clienteSeleccionado = await _gatewayCliente.ObtenerPorIdAsync(idCliente);

            if (clienteSeleccionado is null)
                throw new BusinessException($"No existe cliente con el id {idCliente}", (int)TipoExcepcionNegocio.ClienteNoExiste);

            clienteSeleccionado.HabilitarDeuda();
            await _gatewayCliente.ActualizarAsync(idCliente, clienteSeleccionado);

            return true;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.ObtenerClientePorId(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public async Task<Cliente> ObtenerClientePorId(string idCliente) =>
            await _gatewayCliente.ObtenerPorIdAsync(idCliente);

        /// <summary>
        /// <see cref="IClienteUseCase.ObtenerTodos"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cliente>> ObtenerTodos() =>
            await _gatewayCliente.ObtenerTodosAsync();
    }
}