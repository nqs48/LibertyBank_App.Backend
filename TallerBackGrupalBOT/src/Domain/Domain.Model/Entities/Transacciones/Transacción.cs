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
    }
}