using Domain.Model.Entities.Cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Tests
{
    public class CuentaBuilderTest
    {
        private string _id = string.Empty;
        private string _idCliente = string.Empty;
        private string _numeroCuenta = string.Empty;
        private TipoCuenta _tipoCuenta = TipoCuenta.Ahorros;
        private decimal _saldo = 0M;
        private decimal _saldoDisponible = 0M;
        private bool _exenta = false;


        public CuentaBuilderTest() { }

        public Cuenta Build() => new(_id, _idCliente, _numeroCuenta, _tipoCuenta, _saldo, _saldoDisponible, _exenta);
        

        public CuentaBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public CuentaBuilderTest ConIdCliente(string idCliente)
        {
            _idCliente = idCliente;
            return this;
        }

        public CuentaBuilderTest ConNumeroCuenta(string numeroCuenta)
        {
            _numeroCuenta = numeroCuenta;
            return this;
        }

        public CuentaBuilderTest ConTipoCuenta(TipoCuenta tipoCuenta)
        {
            _tipoCuenta = tipoCuenta;
            return this;
        }

        public CuentaBuilderTest ConSaldo(decimal saldo)
        {
            _saldo = saldo;
            return this;
        }


        public CuentaBuilderTest ConSaldoDisponible(decimal saldoDisponible)
        {
            _saldoDisponible = saldoDisponible;
            return this;
        }

        public CuentaBuilderTest ConExenta(bool exenta)
        {
            _exenta = exenta;
            return this;
        }
    }


}
