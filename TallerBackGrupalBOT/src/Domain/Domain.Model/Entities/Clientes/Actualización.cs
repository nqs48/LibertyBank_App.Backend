using System;

namespace Domain.Model.Entities.Clientes
{
    public class Actualización
    {
        public TipoActualización TipoActualización { get; set; }
        public Usuario UsuarioModificación { get; set; }
        public DateTime Fecha { get; set; }
    }
}