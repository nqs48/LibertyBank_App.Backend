﻿using System.ComponentModel;

namespace Helpers.Commons.Exceptions
{
    /// <summary>
    /// ResponseError
    /// </summary>
    public enum TipoExcepcionNegocio
    {
        /// <summary>
        /// Tipo de exception no controlada
        /// </summary>
        [Description("Excepción de negocio no controlada")]
        ExceptionNoControlada = 555,
    }
}