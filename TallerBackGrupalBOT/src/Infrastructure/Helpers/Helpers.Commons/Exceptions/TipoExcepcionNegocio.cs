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
        CorreoElectronicoNoValido = 566

        /// <summary>
        /// Excepción entidad no encontrada
        /// </summary>
        [Description("Excepción entidad no encontrada")]
        EntidadNoEncontrada = 562,
    }
}