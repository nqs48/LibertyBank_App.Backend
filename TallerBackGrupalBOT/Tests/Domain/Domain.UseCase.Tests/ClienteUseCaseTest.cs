using credinet.exception.middleware.models;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Gateway;
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
            var clienteUseCase = new ClienteUseCase(_clienteMock.Object, _cuentaMock.Object, _usuarioMock.Object);

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
            var clienteUseCase = new ClienteUseCase(_clienteMock.Object, _cuentaMock.Object, _usuarioMock.Object);

            // act
            var result = await Assert.ThrowsAsync<BusinessException>(() => clienteUseCase.ActualizarCorreoElectronico(cliente.Id, nuevoCorreo));

            // Assert
            Assert.Equal((int)TipoExcepcionNegocio.CorreoElectronicoNoValido, result.code);
        }

        [Fact]
        public async Task AgregarproductosAlCliente_correcto()
        {
        }
    }
}