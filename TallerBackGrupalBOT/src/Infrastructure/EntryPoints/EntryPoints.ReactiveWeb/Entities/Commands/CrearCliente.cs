using Domain.Model.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entities.Commands
{
    /// <summary>
    /// DTO de <see cref="Cliente"/> para crear un cliente
    /// </summary>
    public class CrearCliente
    {
        /// <summary>
        /// Tipo de identificación
        /// </summary>
        public TipoIdentificación TipoIdentificación { get; private set; }

        /// <summary>
        /// Numero de identificación
        /// </summary>
        public string NumeroIdentificación { get; private set; }

        /// <summary>
        /// Nombres del cliente
        /// </summary>
        public string Nombres { get; private set; }

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        public string Apellidos { get; private set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string CorreoElectronico { get; private set; }

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateOnly FechaNacimiento { get; private set; }
    }
}