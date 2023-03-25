using Domain.Model.Entities.Usuarios;
using System;

namespace Domain.Model.Entities.Clientes
{
    /// <summary>
    /// Actualizaciones del cliente
    /// </summary>
    public class Actualización
    {
        /// <summary>
        /// <see cref="TipoActualización"/>
        /// </summary>
        public TipoActualización TipoActualización { get; set; }

        /// <summary>
        /// <see cref="Usuario"/>
        /// </summary>
        public Usuario UsuarioModificación { get; set; }

        /// <summary>
        /// Fecha de la actualización
        /// </summary>
        public DateTime Fecha { get; private set; }

        /// <summary>
        /// Constructor de actualización
        /// </summary>
        /// <param name="tipoActualización"></param>
        /// <param name="usuarioModificación"></param>
        public Actualización(TipoActualización tipoActualización, Usuario usuarioModificación)
        {
            TipoActualización = tipoActualización;
            UsuarioModificación = usuarioModificación;
            Fecha = DateTime.Now;
        }
    }
}