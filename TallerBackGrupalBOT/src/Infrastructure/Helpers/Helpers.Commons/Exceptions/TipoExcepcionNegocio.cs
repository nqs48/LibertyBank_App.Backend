using System.ComponentModel;

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

        /// <summary>
        /// Valor Retiro No Permitido
        /// </summary>
        [Description("El valor de retiro excede el saldo disponible de la cuenta")]
        ValorRetiroNoPermitido = 562,
    }
}