using Domain.Model.Entities.Usuarios;
using System;

namespace Domain.Model.Entities.Cuentas
{
    /// <summary>
    /// Clase Modificacion
    /// </summary>
    public class Modificación
    {
        /// <summary>
        /// TipoModificación
        /// </summary>
        public TipoModificación TipoModificación { get; set; }
        /// <summary>
        /// UsuarioModificación
        /// </summary>
        public Usuario UsuarioModificación { get; set; }
        /// <summary>
        /// FechaModificación
        /// </summary>
        public DateTime FechaModificación { get; set; }
    }
}