using credinet.exception.middleware.models;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Domain.UseCase.Clientes;
using Helpers.Commons.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class ClienteUseCaseTest
    {
        private readonly Mock<IClienteRepository> _clienteMock = new Mock<IClienteRepository>();

        private readonly Mock<ICuentaRepository> _cuentaMock = new Mock<ICuentaRepository>();

        private readonly Mock<IUsuarioRepository> _usuarioMock = new Mock<IUsuarioRepository>();

        [Fact]
        public async Task ActualizarCorreoElectronico_Correctamente()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
            string nuevoCorreo = "nuevo_correo_mario@gmail.com";

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // act
            var result = await clienteUseCase.ActualizarCorreoElectronico(cliente.Id, nuevoCorreo);

            // Assert
            Assert.Equal(nuevoCorreo, result.CorreoElectronico);
        }

        [Fact]
        public async Task ActualizarCorreoElectronico_Error_CorreoNoValido()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
            string nuevoCorreo = "nuevo_correo_mario";

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.ActualizarCorreoElectronico(cliente.Id, nuevoCorreo));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.CorreoElectronicoNoValido, result.code);
        }

        [Fact]
        public async Task ActualizarCorreoElectronico_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
            string nuevoCorreo = "nuevo_correo_mario";

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente));
            var clienteUseCase = CrearCasoDeUso();

            // act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.ActualizarCorreoElectronico(cliente.Id, nuevoCorreo));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task AgregarProductosAlCliente_correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
            string nuevoCorreo = "nuevo_correo_mario";

            Cuenta nuevaCuenta = new("1234idcuenta", cliente.Id, "1234numerocuenta", TipoCuenta.Ahorros, 0, 1, true);

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);

            var clienteUseCase = CrearCasoDeUso();

            // act
            var result = await clienteUseCase.AgregarProductosCliente(cliente.Id, nuevaCuenta);

            // Assert
            Assert.Single(result.Productos);
            Assert.Equal(nuevaCuenta.Id, result.Productos[0]);
        }

        [Fact]
        public async Task AgregarProductosAlCliente_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
            string nuevoCorreo = "nuevo_correo_mario";

            Cuenta nuevaCuenta = new("1234idcuenta", cliente.Id, "1234numerocuenta", TipoCuenta.Ahorros, 0, 1, true);

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);

            var clienteUseCase = CrearCasoDeUso();

            // act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.AgregarProductosCliente(cliente.Id, nuevaCuenta));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task CrearCliente_correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Admin);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.CrearCliente(usuario.Id, cliente);

            // Assert
            Assert.Equal(cliente.NumeroIdentificación, result.NumeroIdentificación);
        }

        [Fact]
        public async Task CrearCliente_Error_UsuarioNovalido()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Transaccional);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.CrearCliente(usuario.Id, cliente));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.UsuarioNoValido, result.code);
        }

        [Fact]
        public async Task CrearCliente_Error_ClienteYaExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Admin);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);

            _clienteMock.Setup(repo => repo.ObtenerPorNumeroIdentificacion(cliente.NumeroIdentificación)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.CrearCliente(usuario.Id, cliente));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.IdentificacionDeClienteYaExiste, result.code);
        }

        [Fact]
        public async Task CrearCliente_Error_NoEsMayorDeEdad()
        {
            // Arrange
            DateOnly fechaNacimiento = new(2015, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Admin);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.CrearCliente(usuario.Id, cliente));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoEsMayorDeEdad, result.code);
        }

        [Fact]
        public async Task CrearCliente_Error_CorreoNoValido()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Admin);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.CrearCliente(usuario.Id, cliente));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.CorreoElectronicoNoValido, result.code);
        }

        [Fact]
        public async Task DeshabilitarCliente_Correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.DeshabilitarCliente(cliente.Id);

            // Assert
            Assert.False(cliente.EstaHabilitado);
        }

        [Fact]
        public async Task DeshabilitarCliente_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.DeshabilitarCliente(cliente.Id));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task DeshabilitarDeuda_Correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.DeshabilitarDeudaCliente(cliente.Id);

            // Assert
            Assert.False(cliente.TieneDeudasActivas);
        }

        [Fact]
        public async Task DeshabilitarDeuda_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.DeshabilitarDeudaCliente(cliente.Id));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task HabilitarCliente_Correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);
            cliente.Deshabilitar();

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.HabilitarCliente(cliente.Id);

            // Assert
            Assert.True(cliente.EstaHabilitado);
        }

        [Fact]
        public async Task HabilitarCliente_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);
            cliente.Deshabilitar();

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.HabilitarCliente(cliente.Id));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task HabilitarDeuda_Correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);
            cliente.DeshabilitarDeuda();

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.HabilitarDeudaCliente(cliente.Id);

            // Assert
            Assert.True(cliente.TieneDeudasActivas);
        }

        [Fact]
        public async Task HabilitarDeuda_Error_ClienteNoExiste()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);
            cliente.DeshabilitarDeuda();

            _clienteMock.Setup(repo => repo.ActualizarAsync(cliente.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.HabilitarDeudaCliente(cliente.Id));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoExiste, result.code);
        }

        [Fact]
        public async Task ObtenerClientePorId_Correcto()
        {
            // Arrange
            DateOnly fechaNacimiento = new(1993, 03, 23);
            Cliente cliente = new("123id", TipoIdentificación.CC, "1234identificacion", "Mario", "Cardona", "mariogmail.com", fechaNacimiento);

            _clienteMock.Setup(repo => repo.ObtenerPorIdAsync(cliente.Id)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.ObtenerClientePorId(cliente.Id);

            // Arrange
            Assert.Equal(cliente.Apellidos, result.Apellidos);
        }

        #region Métodos privados

        private ClienteUseCase CrearCasoDeUso() =>
            new(_clienteMock.Object, _cuentaMock.Object, _usuarioMock.Object);

        #endregion Métodos privados
    }
}