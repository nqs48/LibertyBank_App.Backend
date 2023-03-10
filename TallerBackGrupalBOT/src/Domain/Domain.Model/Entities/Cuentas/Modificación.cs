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

        /// <summary>
        /// Constructor de Modificacion
        /// </summary>
        /// <param name="tipoModificación"></param>
        /// <param name="usuarioModificación"></param>
        public Modificación(TipoModificación tipoModificación, Usuario usuarioModificación)
        {
            TipoModificación = tipoModificación;
            UsuarioModificación = usuarioModificación;
            FechaModificación = DateTime.Now;
        }
    }
}