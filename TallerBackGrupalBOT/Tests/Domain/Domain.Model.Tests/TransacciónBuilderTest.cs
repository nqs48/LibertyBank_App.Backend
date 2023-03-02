using Domain.Model.Entities.Transacciones;

namespace Domain.Model.Tests
{
    public class TransacciónBuilderTest
    {
        private string _id = string.Empty;
        private string _idCuenta = string.Empty;
        private DateTime _fechaMovimiento;
        private TipoTransacción _tipoTransacción;
        private decimal _valor = 0M;
        private decimal _saldoInicial = 0M;
        private decimal _saldoFinal = 0M;
        private string _descripción = string.Empty;

        public TransacciónBuilderTest()
        {
        }

        public Transacción Build() =>
            new(_id, _idCuenta, _fechaMovimiento,
                _tipoTransacción, _valor,
                _saldoInicial, _saldoFinal, _descripción);

        public TransacciónBuilderTest WithId(string id)
        {
            _id = id;
            return this;
        }

        public TransacciónBuilderTest WithIdCuenta(string idCuenta)
        {
            _idCuenta = idCuenta;
            return this;
        }

        public TransacciónBuilderTest WithFechaMovimiento(DateTime fecha)
        {
            _fechaMovimiento = fecha;
            return this;
        }

        public TransacciónBuilderTest WithTipoTransacción(TipoTransacción tipoTransacción)
        {
            _tipoTransacción = tipoTransacción;
            return this;
        }

        public TransacciónBuilderTest WithValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public TransacciónBuilderTest WithSaldoInicial(decimal saldoInicial)
        {
            _saldoInicial = saldoInicial;
            return this;
        }

        public TransacciónBuilderTest WithSaldoFinal(decimal saldoFinal)
        {
            _saldoFinal = saldoFinal;
            return this;
        }

        public TransacciónBuilderTest WithDescripción(string descripción)
        {
            _descripción = descripción;
            return this;
        }
    }
}