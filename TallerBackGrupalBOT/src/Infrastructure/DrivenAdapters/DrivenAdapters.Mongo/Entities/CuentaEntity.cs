using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// DTO de entidad <see cref="Cuenta"/>
    /// </summary>
    public class CuentaEntity : EntityBase
    {
        /// <summary>
        /// Id de entidad <see cref="Cliente"/>
        /// </summary>
        [JsonProperty("idCliente")]
        [BsonElement("idCliente")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCliente { get; set; }

        /// <summary>
        /// Numero de Cuenta
        /// </summary>
        [BsonElement("numeroCuenta")]
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Enum <see cref="TipoCuenta"/>
        /// </summary>
        [JsonConverter(typeof(TipoCuenta))]
        [BsonRepresentation(BsonType.String)]
        public TipoCuenta TipoCuenta { get; set; }

        /// <summary>
        /// Enum <see cref="EstadoCuenta"/>
        /// </summary>
        [JsonConverter(typeof(EstadoCuenta))]
        [BsonRepresentation(BsonType.String)]
        public EstadoCuenta EstadoCuenta { get; set; }

        /// <summary>
        /// Saldo
        /// </summary>
        [BsonElement("saldo")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Saldo { get; set; }

        /// <summary>
        /// Saldo Disponible
        /// </summary>
        [BsonElement("saldo_disponible")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal SaldoDisponible { get; set; }

        /// <summary>
        /// Excenta GMF
        /// </summary>
        [BsonElement("exenta")]
        public bool Exenta { get; private set; }

        /// <summary>
        /// Historial de Modificaciones de la cuenta
        /// </summary>
        [BsonElement("historial_modificaciones")]
        public List<Modificación> HistorialModificaciones { get; private set; }
    }
}