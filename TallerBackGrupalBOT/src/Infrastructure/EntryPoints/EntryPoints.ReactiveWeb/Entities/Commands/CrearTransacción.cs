using System;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;

namespace EntryPoints.ReactiveWeb.Entities.Commands;

/// <summary>
/// Comando para crear una entidad de tipo <see cref="Transacción"/>
/// </summary>
public class CrearTransacción
{
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