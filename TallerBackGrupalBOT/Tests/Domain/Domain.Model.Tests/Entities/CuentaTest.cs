using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Model.Tests.Entities
{
    [ExcludeFromCodeCoverage]
    public class CuentaTest
    {
        [Fact]
        public void CrearCuenta_Exitoso()
        {
            string id = "1";
            string idCliente = "001";
            string numeroCuenta= "23121";
            TipoCuenta tipoCuenta= TipoCuenta.Ahorros;
            decimal saldo= 2000;
            decimal saldoDisponible= 2000; 
            bool exenta= true;

            var cuentaId = new Cuenta("1");
            var cuentaCrear= new Cuenta(idCliente,tipoCuenta,saldo,exenta);
            var cuentaActualizar = new Cuenta(id, idCliente, numeroCuenta, tipoCuenta, saldo, saldoDisponible, exenta);

            Assert.NotNull(cuentaId);
            Assert.NotNull(cuentaCrear);
            Assert.NotNull(cuentaActualizar);
        }

        [Fact]
        public void AsignarSaldoInicial_Exitoso()
        {
            var valor = 100000;

            var cuentaTest = new CuentaBuilderTest()
                .WithId("01")
                .WithIdCliente("001")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarSaldoInicial(100000);

            Assert.Equal(valor, cuentaTest.Saldo);
        }

        [Fact]
        public void AsignarSaldoInicial_Failure()
        {
            var valor = 100000;

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("2")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarSaldoInicial(2000);

            Assert.NotEqual(valor, cuentaTest.Saldo);
        }

        [Fact]
        public void CalcularSaldoDisponible_Exitoso()
        {
            decimal valorGMF = 0.004M;
            decimal saldoDisponible = 996000M;

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("3")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .WithSaldo(1000000)
                .WithSaldoDisponible(0)
                .Build();

            cuentaTest.CalcularSaldoDisponible(valorGMF);

            Assert.Equal(saldoDisponible, cuentaTest.SaldoDisponible);
        }

        [Fact]
        public void CalcularSaldoDisponible_Failure()
        {
            decimal valorGMF = 0.004M;
            decimal saldoDisponible = 990000M;

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("3")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .WithSaldo(1000000)
                .Build();

            cuentaTest.CalcularSaldoDisponible(valorGMF);

            Assert.NotEqual(saldoDisponible, cuentaTest.SaldoDisponible);
        }

        [Fact]
        public void AsignarNumeroCuentaAhorros_Exitoso()
        {
            var numeroCuenta = "46-";

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("4")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarNumeroCuenta();

            Assert.StartsWith(numeroCuenta, cuentaTest.NumeroCuenta);
        }

        [Fact]
        public void AsignarNumeroCuentaAhorros_Failure()
        {
            var numeroCuenta = "23-";

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("4")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarNumeroCuenta();

            Assert.DoesNotContain(numeroCuenta, cuentaTest.NumeroCuenta);
        }

        [Fact]
        public void AsignarNumeroCuentaCorriente_Exitoso()
        {
            var numeroCuenta = "23-";

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("4")
                .WithTipoCuenta(TipoCuenta.Corriente)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarNumeroCuenta();

            Assert.StartsWith(numeroCuenta, cuentaTest.NumeroCuenta);
        }

        [Fact]
        public void AsignarNumeroCuentaCorriente_Failure()
        {
            var numeroCuenta = "46-";

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("4")
                .WithTipoCuenta(TipoCuenta.Corriente)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AsignarNumeroCuenta();

            Assert.DoesNotContain(numeroCuenta, cuentaTest.NumeroCuenta);
        }

        [Fact]
        public void MarcarCuentaExenta_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("5")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .WithExenta(false)
                .Build();

            cuentaTest.MarcarCuentaExenta();

            Assert.True(cuentaTest.Exenta);
        }

        [Fact]
        public void AgregarModificacion_Exitoso()
        {
            var usuarioTest = new UsuarioBuilderTest()
                .WithId("1")
                .WithRol(Roles.Admin)
                .Build();

            var modificacionTest = new Modificación(TipoModificación.Habilitación, usuarioTest);

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AgregarModificacion(modificacionTest);

            Assert.Contains<Modificación>(modificacionTest, cuentaTest.HistorialModificaciones);
        }

        [Fact]
        public void AgregarModificacion_Failure()
        {
            var usuarioTest = new UsuarioBuilderTest()
                .WithId("1")
                .WithRol(Roles.Admin)
                .Build();

            var modificacionTest = new Modificación(TipoModificación.Habilitación, usuarioTest);
            var modificacionTestExpect = new Modificación(TipoModificación.Habilitación, usuarioTest);

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.AgregarModificacion(modificacionTest);

            Assert.DoesNotContain<Modificación>(modificacionTestExpect, cuentaTest.HistorialModificaciones);
        }

        [Fact]
        public void ActualizarSaldo_Exitoso()
        {
            
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .WithSaldo(2000)
                .Build();

            var nuevoSaldo = cuentaTest.Saldo + 3000;

            cuentaTest.ActualizarSaldo(nuevoSaldo);

            Assert.Equal(cuentaTest.Saldo, nuevoSaldo);
        }

        [Fact]
        public void ActualizarSaldo_Failure()
        {

            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithTipoCuenta(TipoCuenta.Ahorros)
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .WithSaldo(2000)
                .Build();

            var nuevoSaldo = cuentaTest.Saldo + 3000;

            cuentaTest.ActualizarSaldo(nuevoSaldo);

            Assert.NotEqual(3000, nuevoSaldo);
        }

        [Fact]
        public void EstaActiva_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            var result = cuentaTest.EstaActiva();

            Assert.True(result);
        }

        [Fact]
        public void EstaInactiva_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            var result = cuentaTest.EstaInactiva();

            Assert.False(result);
        }

        [Fact]
        public void EstaCancelada_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            var result = cuentaTest.EstaCancelada();

            Assert.False(result);
        }

        [Fact]
        public void HabilitarCuenta_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Inactiva)
                .Build();

            cuentaTest.HabilitarCuenta();

            Assert.Equal(EstadoCuenta.Activa,cuentaTest.EstadoCuenta);
        }

        [Fact]
        public void DehabilitarCuenta_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.DeshabilitarCuenta();

            Assert.Equal(EstadoCuenta.Inactiva, cuentaTest.EstadoCuenta);
        }

        [Fact]
        public void CancelarCuenta_Exitoso()
        {
            var cuentaTest = new CuentaBuilderTest()
                .WithIdCliente("6")
                .WithNumeroDeCuenta("1")
                .WithEstadoCuenta(EstadoCuenta.Activa)
                .Build();

            cuentaTest.CancelarCuenta();

            Assert.Equal(EstadoCuenta.Cancelada, cuentaTest.EstadoCuenta);
        }



    }
}
