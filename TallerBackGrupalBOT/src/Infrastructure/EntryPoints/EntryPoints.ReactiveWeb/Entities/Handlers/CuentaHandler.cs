using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using System.Collections.Generic;

namespace EntryPoints.ReactiveWeb.Entities.Handlers
{
    /// <summary>
    /// Handler DTO de entidad <see cref="Cuenta"/>
    /// </summary>
    public class CuentaHandler
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id de entidad <see cref="Cliente"/>
        /// </summary>
        public string IdCliente { get; set; }

        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Tipo de cuenta
        /// </summary>
        public TipoCuenta tipoCuenta { get; set; }

        /// <summary>
        /// Estado de cuenta
        /// </summary>
        public EstadoCuenta EstadoCuenta { get; set; }

        /// <summary>
        /// Saldo
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Saldo disponible
        /// </summary>
        public decimal SaldoDisponible { get; set; }

        /// <summary>
        /// Exenta
        /// </summary>
        public bool Exenta { get; set; }

        /// <summary>
        /// Lista de tipo <see cref="Transacción"/>
        /// </summary>
        public List<TransacciónHandler> Transacciones { get; set; }
    }
}