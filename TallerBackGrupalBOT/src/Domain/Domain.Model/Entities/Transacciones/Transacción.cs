using Domain.Model.Entities.Cuentas;
using System;

namespace Domain.Model.Entities.Transacciones
{
    /// <summary>
    /// Clase <see cref="Transacción"/>
    /// </summary>
    public class Transacción
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Id de entidad <see cref="Cuenta"/>
        /// </summary>
        public string IdCuenta { get; private set; }

        /// <summary>
        /// Fecha en que se hizo el movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; private set; }

        /// <summary>
        /// Tipo de transacción
        /// </summary>
        public TipoTransacción TipoTransacción { get; private set; }

        /// <summary>
        /// Valor
        /// </summary>
        public decimal Valor { get; private set; }

        /// <summary>
        /// Saldo inicial
        /// </summary>
        public decimal SaldoInicial { get; private set; }

        /// <summary>
        /// Saldo final
        /// </summary>
        public decimal SaldoFinal { get; private set; }

        /// <summary>
        /// Descripción
        /// </summary>
        public string Descripción { get; private set; }

        /// <summary>
        /// Crea una instancia de clase <see cref="Transacción"/> con los atributos id de entidad
        /// <see cref="Cuenta"/> y valor
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <param name="valor"></param>
        public Transacción(string idCuenta, decimal valor)
        {
            IdCuenta = idCuenta;
            Valor = valor;
        }

        /// <summary>
        /// Crea una instancia de la clase <see cref="Transacción"/> sin el Id
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <param name="fechaMovimiento"></param>
        /// <param name="tipoTransacción"></param>
        /// <param name="valor"></param>
        /// <param name="saldoInicial"></param>
        /// <param name="saldoFinal"></param>
        /// <param name="descripción"></param>
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

        /// <summary>
        /// Crea una instancia de clase <see cref="Transacción"/> con todos los atributos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCuenta"></param>
        /// <param name="fechaMovimiento"></param>
        /// <param name="tipoTransacción"></param>
        /// <param name="valor"></param>
        /// <param name="saldoInicial"></param>
        /// <param name="saldoFinal"></param>
        /// <param name="descripción"></param>
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

        /// <summary>
        /// Método para asignar el <see cref="TipoTransacción"/>
        /// </summary>
        /// <param name="tipo"></param>
        public void AsignarTipoTransacción(TipoTransacción tipo) => TipoTransacción = tipo;

        /// <summary>
        /// Método para asignar la fecha en la que se hizo el movimiento
        /// </summary>
        public void AsignarFechaMovimiento() => FechaMovimiento = DateTime.Now;

        /// <summary>
        /// Método para asignar el saldo inicial
        /// </summary>
        /// <param name="valor"></param>
        public void AsignarSaldoInicial(decimal valor) => SaldoInicial = valor;

        /// <summary>
        /// Método para asignar el saldo final del débito
        /// </summary>
        /// <param name="valor"></param>
        public void AsignarSaldoFinalDebito(decimal valor) => SaldoFinal = SaldoInicial - valor;

        /// <summary>
        /// Método para asignar el saldo final del crédito
        /// </summary>
        /// <param name="valor"></param>
        public void AsignarSaldoFinalCredito(decimal valor) => SaldoFinal = SaldoInicial + valor;

        /// <summary>
        /// Método para generar una descripción
        /// </summary>
        /// <param name="cuentaDestinatario"></param>
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