namespace Domain.Model.Entities.Usuarios
{
    /// <summary>
    /// Clase <see cref="Usuario"/>
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nombre completo
        /// </summary>
        public string NombreCompleto { get; private set; }

        /// <summary>
        /// Rol
        /// </summary>
        public Roles Rol { get; private set; }

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public Usuario()
        {
        }

        /// <summary>
        /// Crea una instancia con todos los atributos de la clase <see cref="Usuario"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombreCompleto"></param>
        /// <param name="rol"></param>
        public Usuario(string id, string nombreCompleto, Roles rol)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Rol = rol;
        }

        /// <summary>
        /// Valida si el rol ingresado es igual a Admin
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public bool EsAdmin(Roles rol)
        {
            return rol == Roles.Admin;
        }
    }
}