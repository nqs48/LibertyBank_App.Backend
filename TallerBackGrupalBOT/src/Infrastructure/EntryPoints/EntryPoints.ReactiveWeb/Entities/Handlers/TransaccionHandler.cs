using System;
using Domain.Model.Entities.Transacciones;

namespace EntryPoints.ReactiveWeb.Entities.Handlers;

/// <summary>
/// Handler DTO de entidad <see cref="Transacción"/>
/// </summary>
public class TransacciónHandler
{
    public string Id { get; set; }
    public string IdCuenta { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public TipoTransacción TipoTransacción { get; set; }
    public decimal Valor { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal SaldoFinal { get; set; }
    public string Descripción { get; set; }
}