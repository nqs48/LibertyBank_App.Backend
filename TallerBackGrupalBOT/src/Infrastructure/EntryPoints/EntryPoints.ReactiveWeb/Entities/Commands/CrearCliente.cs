using Domain.Model.Entities.Clientes;
using System;

namespace EntryPoints.ReactiveWeb.Entities.Commands
{
    /// <summary>
    /// DTO de <see cref="Cliente"/> para crear un cliente
    /// </summary>
    public class CrearCliente
    {
        /// <summary>
        /// Constructor vació
        /// </summary>
        public CrearCliente()
        {
        }

        /// <summary>
        /// Tipo de identificación
        /// </summary>
        public TipoIdentificación TipoIdentificacion { get; set; }

        /// <summary>
        /// Numero de identificación
        /// </summary>
        public string NumeroIdentificacion { get; set; }

        /// <summary>
        /// Nombres del cliente
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }
    }
}