﻿using credinet.exception.middleware.models;
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);
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
            Cliente cliente = new(TipoIdentificación.CC, "1234iden", "Mario", "Cardona", "mario@gmail.com", fechaNacimiento);

            Usuario usuario = new("123idusuario", "Jose Rosales", Roles.Admin);

            _usuarioMock.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id)).ReturnsAsync(usuario);
            _clienteMock.Setup(repo => repo.CrearAsync(usuario.Id, cliente)).ReturnsAsync(cliente);
            var clienteUseCase = CrearCasoDeUso();

            // Act
            var result = await clienteUseCase.CrearCliente(usuario.Id, cliente);

            // Assert
            Assert.Equal(cliente.NumeroIdentificación, result.NumeroIdentificación);
        }

        #region Métodos privados

        private ClienteUseCase CrearCasoDeUso() =>
            new(_clienteMock.Object, _cuentaMock.Object, _usuarioMock.Object);

        #endregion Métodos privados
    }
}