using System;

namespace Domain.Model.Entities.Transacciones
{
    public class Transacción
    {
        public string Id { get; private set; }
        public string IdCuenta { get; private set; }
        public DateTime FechaMovimiento { get; private set; }
        public TipoTransacción TipoTransacción { get; private set; }
        public decimal Valor { get; private set; }
        public decimal SaldoInicial { get; private set; }
        public decimal SaldoFinal { get; private set; }
        public string Descripción { get; private set; }

        public Transacción(string idCuenta, decimal valor)
        {
            IdCuenta = idCuenta;
            Valor = valor;
        }

        public Transacción(string idCuenta, DateTime fechaMovimiento,
            TipoTransacción tipoTransacción, decimal valor, decimal saldoInicial,
            decimal saldoFinal, string descripción)
        {
            IdCuenta = idCuenta;
            FechaMovimiento = fechaMovimiento;
            TipoTransacción = tipoTransacción;
            Valor = valor;
            SaldoInicial = saldoInicial;
            SaldoFinal = saldoFinal;
            Descripción = descripción;
        }

        public Transacción(string id, string idCuenta, DateTime fechaMovimiento,
            TipoTransacción tipoTransacción, decimal valor, decimal saldoInicial,
            decimal saldoFinal, string descripción)
        {
            Id = id;
            IdCuenta = idCuenta;
            FechaMovimiento = fechaMovimiento;
            TipoTransacción = tipoTransacción;
            Valor = valor;
            SaldoInicial = saldoInicial;
            SaldoFinal = saldoFinal;
            Descripción = descripción;
        }

        public void AsignarTipoTransacción(TipoTransacción tipo) => TipoTransacción = tipo;

        public void AsignarFechaMovimiento() => FechaMovimiento = DateTime.Now;

        public void AsignarSaldoInicial(decimal valor) => SaldoInicial = valor;

        public void AsignarSaldoFinalDebito(decimal valor) => SaldoFinal = SaldoInicial - valor;

        public void AsignarSaldoFinalCredito(decimal valor) => SaldoFinal = SaldoInicial + valor;

        public void GenerarDescripción(string cuentaDestinatario = "")
        {
            Descripción = TipoTransacción switch
            {
                TipoTransacción.Consignación => $"Se Realizo Consignación por ${Valor} a la cuenta con ID {IdCuenta}",

                TipoTransacción.Retiro => $"Se Realizo Retiro por ${Valor} desde la cuenta con ID {IdCuenta}",

                TipoTransacción.Transferencia =>
                    $"Se Realizo Transferencia por ${Valor} desde la cuenta con ID {IdCuenta} a la cuenta con ID {cuentaDestinatario}",

                _ => Descripción
            };
        }
    }
}