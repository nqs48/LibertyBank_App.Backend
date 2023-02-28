﻿using credinet.exception.middleware.models;
using Domain.Model.Entities.Cuentas;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;
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

        public void AsignarTipoTransacción(TipoTransacción tipo) => TipoTransacción = tipo;

        public void AsignarFechaMovimiento() => FechaMovimiento = DateTime.Now;

        public void AsignarSaldoInicial(decimal valor) => SaldoInicial = valor;

        public void AsignarSaldoFinalDebito(decimal valor) => SaldoFinal = SaldoInicial - valor;

        public void AsignarSaldoFinalCredito(decimal valor) => SaldoFinal = SaldoInicial + valor;

        public void GenerarDescripción(string cuentaDestinatario = "")
        {
            switch (TipoTransacción)
            {
                case TipoTransacción.Consignación:
                    Descripción = $"Se Realizo Consignación por ${Valor} a la cuenta {IdCuenta}";
                    break;

                case TipoTransacción.Retiro:
                    Descripción = $"Se Realizo Retiro por ${Valor} desde la cuenta {IdCuenta}";
                    break;

                case TipoTransacción.Transferencia:
                    Descripción = $"Se Realizo Transferencia por ${Valor} desde la {IdCuenta} a la cuenta {cuentaDestinatario}";
                    break;
            }
        }
    }
}