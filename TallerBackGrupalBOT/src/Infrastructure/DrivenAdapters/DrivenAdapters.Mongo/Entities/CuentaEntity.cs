using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Entities
{

    /// <summary>
    /// DTO de entidad <see cref="Cuenta"/>
    /// </summary>
    public class CuentaEntity  : EntityBase
    {

        /// <summary>
        /// Id Cliente
        /// </summary>
        [BsonElement("idCliente")]
        public string IdCliente { get; set; }

        /// <summary>
        /// Numero de Cuenta
        /// </summary>
        [BsonElement("numeroCuenta")]
        public string NumeroCuenta { get; set; }


    }


    /// <param name="id"></param>
    /// <param name="idCliente"></param>
    /// <param name="numeroCuenta"></param>
    /// <param name="tipoCuenta"></param>
    /// <param name="estadoCuenta"></param>
    /// <param name="saldo"></param>
    /// <param name="saldoDisponible"></param>
    /// <param name="exenta"></param>
}
