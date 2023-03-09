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
        /// Constructor vació
        /// </summary>
        public CrearCliente()
        {
        }

        /// <summary>
        /// Tipo de identificación
        /// </summary>
        public TipoIdentificación TipoIdentificación { get; set; }

        /// <summary>
        /// Numero de identificación
        /// </summary>
        public string NumeroIdentificación { get; set; }

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
        public DateOnly FechaNacimiento { get; set; }

        public CrearCliente(TipoIdentificación tipoIdentificación, string numeroIdentificación, string nombres,
            string apellidos, string correoElectronico, DateOnly fechaNacimiento)
        {
            TipoIdentificación = tipoIdentificación;
            NumeroIdentificación = numeroIdentificación;
            Nombres = nombres;
            Apellidos = apellidos;
            CorreoElectronico = correoElectronico;
            FechaNacimiento = fechaNacimiento;
        }
    }
}