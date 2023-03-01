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
        /// Identificación del cliente ya existe
        /// </summary>
        [Description("Identificación del cliente ya existe")]
        IdentificacionDeClienteYaExiste = 563,

        /// <summary>
        /// El cliente no es mayor de edad
        /// </summary>
        [Description("El cliente no es mayor de edad")]
        ClienteNoEsMayorDeEdad = 564,

        /// <summary>
        /// Cliente no existe
        /// </summary>
        [Description("Cliente no existe")]
        ClienteNoExiste = 565,

        /// <summary>
        /// Correo electrónico no es valido
        /// </summary>
        [Description("Correo electrónico no es valido")]
        CorreoElectronicoNoValido = 566,

        /// <summary>
        /// Excepción entidad no encontrada
        /// </summary>
        [Description("Excepción entidad no encontrada")]
        EntidadNoEncontrada = 562,

        /// <summary>
        /// Valor Retiro No Permitido
        /// </summary>
        [Description("El valor de retiro excede el saldo disponible de la cuenta")]
        ValorRetiroNoPermitido = 567,

        /// <summary>
        /// Excepción cuenta no encontrada
        /// </summary>
        [Description("La cuenta no fue encontrada")]
        CuentaNoEncontrada = 570,

        /// <summary>
        /// Excepción usuario no encontrado
        /// </summary>
        [Description("El usuario no fue encontrada")]
        UsuarioNoEncontrado = 571,

        /// <summary>
        /// Usuario sin Permisos
        /// </summary>
        [Description("El usuario no tiene permisos para esta accion")]
        UsuarioSinPermisos = 572,
    }
}