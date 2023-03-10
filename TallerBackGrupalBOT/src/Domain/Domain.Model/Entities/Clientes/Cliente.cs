using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Model.Entities.Clientes
{
    /// <summary>
    /// Entidad de cliente
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Fecha actual
        /// </summary>
        private readonly DateTime FechaActual = DateTime.Now;

        /// <summary>
        /// Id de cliente
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Tipo de identificación
        /// </summary>
        public TipoIdentificación TipoIdentificacion { get; private set; }

        /// <summary>
        /// Numero de identificación
        /// </summary>
        public string NumeroIdentificacion { get; private set; }

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
        public DateTime FechaNacimiento { get; private set; }

        /// <summary>
        /// Fecha en que se creo el cliente
        /// </summary>
        public DateTime FechaCreación { get; set; }

        /// <summary>
        /// Historial de actualizaciones de datos del cliente
        /// </summary>
        public List<Actualización> HistorialActualizaciones { get; private set; }

        /// <summary>
        /// Estado del cliente
        /// </summary>
        public bool EstaHabilitado { get; private set; }

        /// <summary>
        /// si el cliente tiene deudas activas
        /// </summary>
        public bool TieneDeudasActivas { get; private set; }

        /// <summary>
        /// Cuentas del cliente
        /// </summary>
        public List<string> Productos { get; private set; }

        /// <summary>
        /// Constructor vació
        /// </summary>
        public Cliente()
        {
        }

        /// <summary>
        /// Constructor cliente
        /// </summary>
        /// <param name="tipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <param name="correoElectronico"></param>
        /// <param name="fechaNacimiento"></param>

        public Cliente(string id, TipoIdentificación tipoIdentificacion, string numeroIdentificacion, string nombres,
            string apellidos, string correoElectronico, DateTime fechaNacimiento)
        {
            Id = id;
            TipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = numeroIdentificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            CorreoElectronico = correoElectronico;
            FechaNacimiento = fechaNacimiento;
            FechaCreación = DateTime.Now;
            HistorialActualizaciones = null;
            EstaHabilitado = true;
            TieneDeudasActivas = false;
            Productos = null;
        }

        /// <summary>
        /// Verifica si el cliente ingreso un correo valido
        /// </summary>
        /// <returns></returns>
        public bool VerificarCampoCorreo() =>
            Regex.IsMatch(CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Verifica si el cliente es mayor de edad
        /// </summary>
        /// <returns></returns>
        public bool VerificarEdadCliente(EdadLegal edadLegal)
        {
            var sumaDeAños = (FechaNacimiento.AddYears((int)edadLegal)).Year;
            return sumaDeAños <= FechaActual.Year;
        }

        /// <summary>
        /// Deshabilita cliente
        /// </summary>
        public void Deshabilitar() => EstaHabilitado = false;

        /// <summary>
        /// Habilita cliente
        /// </summary>
        public void Habilitar() => EstaHabilitado = true;

        /// <summary>
        /// Habilita deuda
        /// </summary>
        public void HabilitarDeuda() => TieneDeudasActivas = true;

        /// <summary>
        /// Deshabilita deuda
        /// </summary>
        public void DeshabilitarDeuda() => TieneDeudasActivas = false;

        /// <summary>
        /// Cambia el correo electrónico actual del cliente
        /// </summary>
        /// <param name="nuevoCorreo"></param>
        public void CambiarCorreoElectronico(string nuevoCorreo) => CorreoElectronico = nuevoCorreo;

        /// <summary>
        /// Agrega el id de un producto al cliente
        /// </summary>
        /// <param name="cuenta"></param>
        public void AgregarIdProducto(string cuenta)
        {
            Productos ??= new List<string>();
            Productos.Add(cuenta);
        }

        /// <summary>
        /// Se agrega nueva actualización al cliente
        /// </summary>
        /// <param name="nuevaActualizacion"></param>
        public void AgregarActualizacion(Actualización nuevaActualizacion)
        {
            HistorialActualizaciones ??= new List<Actualización>();
            HistorialActualizaciones.Add(nuevaActualizacion);
        }
    }
}