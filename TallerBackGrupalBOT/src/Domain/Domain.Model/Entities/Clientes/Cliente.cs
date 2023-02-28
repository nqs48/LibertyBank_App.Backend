using Domain.Model.Entities.Cuentas;
using System;
using System.Collections.Generic;

namespace Domain.Model.Entities.Clientes
{
    public class Cliente
    {
        public string Id { get; private set; }
        public TipoIdentificación TipoIdentificación { get; private set; }
        public string NumeroIdentificación { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string CorreoElectronico { get; private set; }
        public DateOnly FechaNacimiento { get; private set; }
        public DateTime FechaCreación { get; private set; }
        public List<Actualización> HistorialActualizaciones { get; private set; }
        public bool EstaHabilitado { get; private set; }
        public bool TieneDeudasActivas { get; private set; }
        public List<Cuenta> Productos { get; private set; }
    }
}