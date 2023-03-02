using Domain.Model.Entities.Transacciones;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Domain.Model.Tests.Entities
{
    [ExcludeFromCodeCoverage]
    public class TransacciónTest
    {
        [Fact]
        public void GenerarDescripción_Exitosa_TipoTransacciónConsignación()
        {
            var descripciónEsperada = "Se Realizo Consignación por $100000 a la cuenta con ID 1";
            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithTipoTransacción(TipoTransacción.Consignación)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.GenerarDescripción();

            Assert.Equal(descripciónEsperada, transacciónTest.Descripción);
        }

        [Fact]
        public void GenerarDescripción_Exitosa_TipoTransacciónRetiro()
        {
            var descripciónEsperada = "Se Realizo Retiro por $200000 desde la cuenta con ID 1";
            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(200000)
                .WithTipoTransacción(TipoTransacción.Retiro)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.GenerarDescripción();

            Assert.Equal(descripciónEsperada, transacciónTest.Descripción);
        }

        [Fact]
        public void GenerarDescripción_Exitosa_TipoTransacciónTransferencia()
        {
            var idCuentaDestino = "2";
            var descripciónEsperada = "Se Realizo Transferencia por $100000 desde la cuenta con ID 1 a la cuenta con ID 2";
            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithTipoTransacción(TipoTransacción.Transferencia)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.GenerarDescripción(idCuentaDestino);

            Assert.Equal(descripciónEsperada, transacciónTest.Descripción);
        }

        [Fact]
        public void AsignarTipoTransacción_Consignación()
        {
            var tipoAsignar = TipoTransacción.Consignación;

            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.AsignarTipoTransacción(tipoAsignar);

            Assert.Equal(tipoAsignar, transacciónTest.TipoTransacción);
        }

        [Fact]
        public void AsignarAsignarFechaMovimiento_Exitoso()
        {
            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.AsignarFechaMovimiento();

            Assert.IsType<DateTime>(transacciónTest.FechaMovimiento);
        }

        [Fact]
        public void AsignarSaldoInicial_Exitoso()
        {
            var valor = 100000;

            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithIdCuenta("1")
                .Build();

            transacciónTest.AsignarSaldoInicial(valor);

            Assert.Equal(valor, transacciónTest.SaldoInicial);
        }

        [Fact]
        public void AsignarAsignarSaldoFinalDebito_Exitoso_RestaElValor()
        {
            var valor = 100000;

            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithSaldoInicial(500000)
                .WithIdCuenta("1")
                .Build();

            var saldoFinalEsperado = transacciónTest.SaldoInicial - valor;

            transacciónTest.AsignarSaldoFinalDebito(valor);

            Assert.Equal(saldoFinalEsperado, transacciónTest.SaldoFinal);
        }

        [Fact]
        public void AsignarAsignarSaldoFinalCredito_Exitoso_SumaElValor()
        {
            var valor = 100000;

            var transacciónTest = new TransacciónBuilderTest()
                .WithValor(100000)
                .WithSaldoInicial(500000)
                .WithIdCuenta("1")
                .Build();

            var saldoFinalEsperado = transacciónTest.SaldoInicial + valor;

            transacciónTest.AsignarSaldoFinalCredito(valor);

            Assert.Equal(saldoFinalEsperado, transacciónTest.SaldoFinal);
        }
    }
}