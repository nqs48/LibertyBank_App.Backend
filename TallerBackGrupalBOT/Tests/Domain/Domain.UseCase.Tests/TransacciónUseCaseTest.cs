using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Tests;
using Domain.UseCase.Transacciones;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class TransacciónUseCaseTest
    {
        private readonly ConfiguradorAppSettings _appSettings;
        private readonly Mock<ICuentaRepository> _mockCuentaRepository;
        private readonly Mock<ITransacciónRepository> _mockTransacciónRepository;
        private readonly Mock<IOptions<ConfiguradorAppSettings>> _mockOptions;
        private readonly TransacciónUseCase _transacciónUseCase;

        public TransacciónUseCaseTest()
        {
            _appSettings = new ConfiguradorAppSettings
            {
                GMF = 0.004M,
                ValorSobregiro = 3000000
            };

            _mockOptions = new Mock<IOptions<ConfiguradorAppSettings>>();

            _mockCuentaRepository = new();
            _mockTransacciónRepository = new();

            _transacciónUseCase = new(_mockCuentaRepository.Object, _mockTransacciónRepository.Object,
                _mockOptions.Object);
        }

        [Fact]
        public async Task ObtenerTransacciónPorId_Exitoso()
        {
            _mockTransacciónRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerUnaTransacciónTest);

            var transacción = await _transacciónUseCase.ObtenerTransacciónPorId(It.IsAny<string>());

            Assert.NotNull(transacción);
            _mockTransacciónRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
        }

        [Fact]
        public async Task ObtenerTransaccionesPorIdCuenta_Exitoso()
        {
            _mockTransacciónRepository
                .Setup(repository => repository.ObtenerPorIdCuenta(It.IsAny<string>()))
                .ReturnsAsync(ObtenerListaTransacciónTest);

            var transacciones = await _transacciónUseCase.ObtenerTransaccionesPorIdCuenta(It.IsAny<string>());

            Assert.NotNull(transacciones);
            Assert.Equal(ObtenerListaTransacciónTest().Count, transacciones.Count);
            _mockTransacciónRepository.Verify(mock => mock.ObtenerPorIdCuenta((It.IsAny<string>())), Times.Once());
        }

        [Fact]
        public async Task RealizarConsignación_Exitoso()
        {
            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(100000)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockCuentaRepository
                .Setup(repository => repository.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockTransacciónRepository
                .Setup(repository => repository.Crear(It.IsAny<Transacción>()))
                .ReturnsAsync(ObtenerUnaTransacciónTest);

            var transacción = await _transacciónUseCase.RealizarConsignación(transacciónTest);

            Assert.NotNull(transacción);

            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Once());
            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()), Times.Once());
        }

        [Fact]
        public async Task RealizarConsignación_EstadoCuentaCancelada_Retorna_Excepcion()
        {
            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaCanceladaTest);

            _mockCuentaRepository
                .Setup(repository => repository.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()))
                .ReturnsAsync(ObtenerCuentaCanceladaTest);

            _mockTransacciónRepository
                .Setup(repository => repository.Crear(It.IsAny<Transacción>()))
                .ReturnsAsync(ObtenerUnaTransacciónTest);

            BusinessException businessException = await Assert.ThrowsAsync<BusinessException>(async () =>
                await _transacciónUseCase.RealizarConsignación(ObtenerUnaTransacciónTest()));

            Assert.Equal((int)TipoExcepcionNegocio.EstadoCuentaCancelada, businessException.code);
        }

        [Fact]
        public async Task RealizarRetiroCuentaExenta_Exitoso()
        {
            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(100000)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockCuentaRepository
                .Setup(repository => repository.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockTransacciónRepository
                .Setup(repository => repository.Crear(It.IsAny<Transacción>()))
                .ReturnsAsync(ObtenerUnaTransacciónTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var transacción = await _transacciónUseCase.RealizarRetiro(transacciónTest);

            Assert.NotNull(transacción);

            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Once());
            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()), Times.Once());
        }

        [Fact]
        public async Task RealizarRetiroCuentaAhorros_Exenta_MontoARetirarMayorSaldo_LanzaExcepción()
        {
            var valorRetiroMayorASaldo = ObtenerCuentaExentaAhorrosTest().Saldo + 1;

            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(valorRetiroMayorASaldo)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var exception =
                await Assert.ThrowsAsync<BusinessException>(async () =>
                    await _transacciónUseCase.RealizarRetiro(transacciónTest));

            Assert.Equal((int)TipoExcepcionNegocio.ValorRetiroNoPermitido, exception.code);

            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Never());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()),
                Times.Never());
        }

        [Fact]
        public async Task RealizarRetiroCuentaCorriente_Exenta_MontoARetiraMayorSaldoMasSobregiro_LanzaExcepción()
        {
            var valorRetiroMayorASaldoMasSobregiro =
                ObtenerCuentaExentaCorrienteTest().Saldo + _appSettings.ValorSobregiro + 1;

            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(valorRetiroMayorASaldoMasSobregiro)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaExentaCorrienteTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var exception =
                await Assert.ThrowsAsync<BusinessException>(async () =>
                    await _transacciónUseCase.RealizarRetiro(transacciónTest));

            Assert.Equal((int)TipoExcepcionNegocio.ValorRetiroNoPermitido, exception.code);

            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Never());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()),
                Times.Never());
        }

        [Fact]
        public async Task RealizarRetiroCuentaAhorros_NoExenta_MontoARetirarIgualSaldo_LanzaExcepción()
        {
            var valorRetiroIgualSaldo = ObtenerCuentaNoExentaAhorrosTest().Saldo;

            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(valorRetiroIgualSaldo)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaNoExentaAhorrosTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var exception =
                await Assert.ThrowsAsync<BusinessException>(async () =>
                    await _transacciónUseCase.RealizarRetiro(transacciónTest));

            Assert.Equal((int)TipoExcepcionNegocio.ValorRetiroNoPermitido, exception.code);

            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Never());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()),
                Times.Never());
        }

        [Fact]
        public async Task RealizarRetiroCuentaCorriente_NoExenta_MontoARetiraIgualSaldoMasSobregiro_LanzaExcepción()
        {
            var valorRetiroIgualASaldoMasSobregiro =
                ObtenerCuentaNoExentaCorrienteTest().Saldo + _appSettings.ValorSobregiro + 1;

            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(valorRetiroIgualASaldoMasSobregiro)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaNoExentaCorrienteTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var exception =
                await Assert.ThrowsAsync<BusinessException>(async () =>
                    await _transacciónUseCase.RealizarRetiro(transacciónTest));

            Assert.Equal((int)TipoExcepcionNegocio.ValorRetiroNoPermitido, exception.code);

            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Once());
            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Never());
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()),
                Times.Never());
        }

        [Fact]
        public async Task RealizarTransferenciaCuentaExenta_Exitoso()
        {
            var idCuentaReceptor = "2";
            var transacciónTest = new TransacciónBuilderTest()
                .WithId("1")
                .WithValor(100000)
                .Build();

            _mockCuentaRepository
                .Setup(repository => repository.ObtenerPorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockCuentaRepository
                .Setup(repository => repository.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()))
                .ReturnsAsync(ObtenerCuentaExentaAhorrosTest);

            _mockTransacciónRepository
                .Setup(repository => repository.Crear(It.IsAny<Transacción>()))
                .ReturnsAsync(ObtenerUnaTransacciónTest);

            _mockOptions
                .Setup(config => config.Value)
                .Returns(_appSettings);

            var transacción = await _transacciónUseCase.RealizarTransferencia(transacciónTest, idCuentaReceptor);

            Assert.NotNull(transacción);

            _mockTransacciónRepository.Verify(mock => mock.Crear((It.IsAny<Transacción>())), Times.Exactly(2));
            _mockCuentaRepository.Verify(mock => mock.ObtenerPorId((It.IsAny<string>())), Times.Exactly(2));
            _mockCuentaRepository.Verify(mock => mock.Actualizar(It.IsAny<string>(), It.IsAny<Cuenta>()),
                Times.Exactly(2));
        }

        #region Private Methods

        private Transacción ObtenerUnaTransacciónTest() =>
            new TransacciónBuilderTest()
                .WithId("1")
                .WithIdCuenta("4300000000")
                .WithFechaMovimiento(DateTime.Now)
                .WithTipoTransacción(TipoTransacción.Consignación)
                .WithValor(100000)
                .WithSaldoInicial(1000000)
                .WithSaldoFinal(1100000)
                .Build();

        private List<Transacción> ObtenerListaTransacciónTest() => new()
        {
            new TransacciónBuilderTest()
                .WithId("1")
                .WithIdCuenta("4600000000")
                .WithFechaMovimiento(DateTime.Now)
                .WithTipoTransacción(TipoTransacción.Consignación)
                .WithValor(100000)
                .WithSaldoInicial(1000000)
                .WithSaldoFinal(1100000)
                .Build(),
            new TransacciónBuilderTest()
                .WithId("2")
                .WithIdCuenta("2300000000")
                .WithFechaMovimiento(DateTime.Now)
                .WithTipoTransacción(TipoTransacción.Retiro)
                .WithValor(100000)
                .WithSaldoInicial(1000000)
                .WithSaldoFinal(900000)
                .WithDescripción("Se Realizo Retiro por $100000 desde la cuenta con ID 2")
                .Build(),
        };

        private Cuenta ObtenerCuentaNoExentaAhorrosTest() => new CuentaBuilderTest()
            .WithId("1")
            .WithIdCliente("123456789")
            .WithNumeroDeCuenta("4600000000")
            .WithTipoCuenta(TipoCuenta.Ahorros)
            .WithEstadoCuenta(EstadoCuenta.Activa)
            .WithSaldo(1000000)
            .WithSaldoDisponible(996000)
            .WithExenta(false)
            .Build();

        private Cuenta ObtenerCuentaCanceladaTest()
        {
            Cuenta cuenta = new CuentaBuilderTest()
                .WithId("1")
                .WithIdCliente("123456789")
                .WithNumeroDeCuenta("4600000000")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithSaldo(0)
                .WithSaldoDisponible(0)
                .WithExenta(false)
                .Build();

            cuenta.CancelarCuenta();

            return cuenta;
        }

        private Cuenta ObtenerCuentaExentaAhorrosTest() => new CuentaBuilderTest()
            .WithId("1")
            .WithIdCliente("123456789")
            .WithNumeroDeCuenta("4600000000")
            .WithTipoCuenta(TipoCuenta.Ahorros)
            .WithEstadoCuenta(EstadoCuenta.Activa)
            .WithSaldo(1000000)
            .WithSaldoDisponible(996000)
            .WithExenta(true)
            .Build();

        private Cuenta ObtenerCuentaNoExentaCorrienteTest() => new CuentaBuilderTest()
            .WithId("1")
            .WithIdCliente("123456789")
            .WithNumeroDeCuenta("4600000000")
            .WithTipoCuenta(TipoCuenta.Corriente)
            .WithEstadoCuenta(EstadoCuenta.Activa)
            .WithSaldo(1000000)
            .WithSaldoDisponible(996000)
            .WithExenta(false)
            .Build();

        private Cuenta ObtenerCuentaExentaCorrienteTest() => new CuentaBuilderTest()
            .WithId("1")
            .WithIdCliente("123456789")
            .WithNumeroDeCuenta("4600000000")
            .WithTipoCuenta(TipoCuenta.Corriente)
            .WithEstadoCuenta(EstadoCuenta.Activa)
            .WithSaldo(1000000)
            .WithSaldoDisponible(996000)
            .WithExenta(true)
            .Build();

        #endregion Private Methods
    }
}