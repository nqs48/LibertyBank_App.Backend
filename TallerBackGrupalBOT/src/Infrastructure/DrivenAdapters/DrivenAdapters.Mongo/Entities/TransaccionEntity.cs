using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace DrivenAdapters.Mongo.entities;

/// <summary>
/// DTO de entidad <see cref="Transacción"/>
/// </summary>
public class TransacciónEntity : EntityBase
{
    /// <summary>
    /// Id de entidad <see cref="Cuenta"/>
    /// </summary>
    [JsonProperty("idCuenta")]
    [BsonElement("idCuenta")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdCuenta { get; set; }

    /// <summary>
    /// Fecha De movimiento
    /// </summary>
    [BsonElement("fecha_movimiento")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime FechaMovimiento { get; set; }

    /// <summary>
    /// Enum <see cref="TipoTransacción"/>
    /// </summary>
    [JsonConverter(typeof(TipoTransacción))]
    [BsonRepresentation(BsonType.String)]
    public TipoTransacción TipoTransacción { get; set; }

    /// <summary>
    /// Valor
    /// </summary>
    [BsonElement("valor")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Valor { get; set; }

    /// <summary>
    /// Saldo inicial
    /// </summary>
    [BsonElement("saldo_inicial")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal SaldoInicial { get; set; }

    /// <summary>
    /// Saldo final
    /// </summary>
    [BsonElement("saldo_final")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal SaldoFinal { get; set; }

    /// <summary>
    /// Descripcion
    /// </summary>
    [BsonElement("descripcion")] public string Descripción { get; set; }
}