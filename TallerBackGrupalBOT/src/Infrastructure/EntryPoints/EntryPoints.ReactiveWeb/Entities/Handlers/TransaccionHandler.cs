using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using System;

namespace EntryPoints.ReactiveWeb.Entities.Handlers;

/// <summary>
/// Handler DTO de entidad <see cref="Transacción"/>
/// </summary>
public class TransacciónHandler
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Id de entidad <see cref="Cuenta"/>
    /// </summary>
    public string IdCuenta { get; set; }

    /// <summary>
    /// Fecha en que se hizo el movimiento
    /// </summary>
    public DateTime FechaMovimiento { get; set; }

    /// <summary>
    /// Tipo de transacción
    /// </summary>
    public TipoTransacción TipoTransacción { get; set; }

    /// <summary>
    /// Valor
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Saldo inicial
    /// </summary>
    public decimal SaldoInicial { get; set; }

    /// <summary>
    /// Saldo final
    /// </summary>
    public decimal SaldoFinal { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    public string Descripción { get; set; }
}