﻿using Domain.Model.Entities.Cuentas;

namespace EntryPoints.ReactiveWeb.Entities.Commands
{
    /// <summary>
    /// Comando para crear una entidad de tipo <see cref="Cuenta"/>
    /// </summary>
    public class CrearCuenta
    {
        /// <summary>
        /// Id del cliente
        /// </summary>
        public string IdCliente { get; set; }

        /// <summary>
        /// Tipo de cuenta
        /// </summary>
        public TipoCuenta TipoCuenta { get; set; }

        /// <summary>
        /// Saldo de la cuenta
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// Indica si la cuenta es exenta
        /// </summary>
        public bool Exenta { get; set; }
    }
}