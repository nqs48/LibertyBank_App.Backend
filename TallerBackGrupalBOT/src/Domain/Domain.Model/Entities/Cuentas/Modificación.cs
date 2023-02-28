using Domain.Model.Entities.Usuarios;
using System;

namespace Domain.Model.Entities.Cuentas
{
    public class Modificación
    {
        public TipoModificación TipoModificación { get; set; }
        public Usuario UsuarioModificación { get; set; }
        public DateTime FechaModificación { get; set; }
    }
}