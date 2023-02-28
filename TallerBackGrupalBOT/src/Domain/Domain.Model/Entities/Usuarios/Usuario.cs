namespace Domain.Model.Entities.Usuarios
{
    public class Usuario
    {
        public string Id { get; private set; }
        public string NombreCompleto { get; private set; }
        public Roles Rol { get; private set; }
    }
}