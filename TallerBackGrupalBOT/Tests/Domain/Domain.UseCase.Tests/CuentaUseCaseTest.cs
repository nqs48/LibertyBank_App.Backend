using Domain.Model.Entities.Gateway;
using Moq;

namespace Domain.UseCase.Tests
{
    public class CuentaUseCaseTest
    {
        private readonly Mock<IClienteRepository> _clienteMock = new Mock<IClienteRepository>();

        private readonly Mock<ICuentaRepository> _cuentaMock = new Mock<ICuentaRepository>();

        private readonly Mock<IUsuarioRepository> _usuarioMock = new Mock<IUsuarioRepository>();

        //[Fact]
        //public void CreaCuenta_coorectamente()
        //{
        //    //Arrange
        //    var cliente = new ClienteBuilderTest().Build();
        //    var cuenta = new CuentaBuilderTest().Build();
        //    var usuario = new UsuarioBuilder().Build();

        // _clienteMock.Setup(x => x.ObtenerPorIdAsync(It.IsAny<string>())).Returns(cliente);
        // _cuentaMock.Setup(x => x.Crear(It.IsAny<Cuenta>())).Returns(cuenta); _usuarioMock.Setup(x
        // => x.ObtenerUsuarioPorId(It.IsAny<string>())).Returns(usuario);

        // var cuentaUseCase = new CuentaUseCase(_clienteMock.Object, _cuentaMock.Object, _usuarioMock.Object);

        //    //Act
        //    var result
        //}

        //[Fact]
        //public void CreaCuenta_con_cliente_inexistente()
        //{
        //    //Arrange
        //    var cliente = new ClienteBuilderTest().Build();
        //    var cuenta = new CuentaBuilderTest().Build();
        //    var usuario = new UsuarioBuilderTest().Build();

        // _clienteMock.Setup(x => x.ObtenerClientePorId(It.IsAny<string>())).Returns(cliente);
        // _cuentaMock.Setup(x => x.CrearCuenta(It.IsAny<Cuenta>())).Returns(cuenta);
        // _usuarioMock.Setup(x => x.ObtenerUsuarioPorId(It.IsAny<string>())).Returns(usuario);

        //    var cuentaUseCase = new CuentaUseCase(_clienteMock.Objec)
        //}
    }
}