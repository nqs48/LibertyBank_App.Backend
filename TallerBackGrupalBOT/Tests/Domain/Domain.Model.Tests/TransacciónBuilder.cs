using Domain.Model.Entities.Transacciones;

namespace Domain.Model.Tests
{
    public class TransacciónBuilder
    {
        private string _id = string.Empty;
        private string _idCuenta = string.Empty;
        private DateTime _fechaMovimiento;
        private TipoTransacción _tipoTransacción;
        private decimal _valor = 0M;
        private decimal _saldoInicial = 0M;
        private decimal _saldoFinal = 0M;
        private string _descripción = string.Empty;

        public TransacciónBuilder()
        {
        }

        public Transacción Build() =>
            new(_id, _idCuenta, _fechaMovimiento,
                _tipoTransacción, _valor,
                _saldoInicial, _saldoFinal, _descripción);

        public TransacciónBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public TransacciónBuilder WithIdCuenta(string idCuenta)
        {
            _idCuenta = idCuenta;
            return this;
        }

        public TransacciónBuilder WithFechaMovimiento(DateTime fecha)
        {
            _fechaMovimiento = fecha;
            return this;
        }

        public TransacciónBuilder WithTipoTransacción(TipoTransacción tipoTransacción)
        {
            _tipoTransacción = tipoTransacción;
            return this;
        }

        public TransacciónBuilder WithValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public TransacciónBuilder WithSaldoInicial(decimal saldoInicial)
        {
            _saldoInicial = saldoInicial;
            return this;
        }

        public TransacciónBuilder WithSaldoFinal(decimal saldoFinal)
        {
            _saldoFinal = saldoFinal;
            return this;
        }

        public TransacciónBuilder WithDescripción(string descripción)
        {
            _descripción = descripción;
            return this;
        }
    }
}