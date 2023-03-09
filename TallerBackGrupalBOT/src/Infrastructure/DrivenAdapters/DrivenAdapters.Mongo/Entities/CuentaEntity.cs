using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivenAdapters.Mongo.Entities.Base;

namespace DrivenAdapters.Mongo.Entities
{

    /// <summary>
    /// DTO de entidad <see cref="Cuenta"/>
    /// </summary>
    public class CuentaEntity  : EntityBase
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
