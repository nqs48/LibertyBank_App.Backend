using System.Collections.Generic;

namespace Domain.Model.Entities.Cuentas
{
    public class Cuenta
    {
        public string Id { get; private set; }
        public string IdCliente { get; private set; }
        public string NumeroCuenta { get; private set; }
        public TipoCuenta TipoCuenta { get; private set; }
        public EstadoCuenta EstadoCuenta { get; private set; }
        public decimal Saldo { get; private set; }
        public decimal SaldoDisponible { get; set; }
        public bool Exenta { get; private set; }

        public List<Modificación> HistorialModificaciones { get; private set; }
    }
}