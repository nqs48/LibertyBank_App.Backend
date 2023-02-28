using Domain.Model.Entities.Usuarios;

namespace EntryPoints.ReactiveWeb.Entities.Handlers;

/// <summary>
/// Request DTO de entidad <see cref="Usuario"/>
/// </summary>
public class UsuarioResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Nombre completo
    /// </summary>
    public string NombreCompleto { get; set; }

    /// <summary>
    /// Rol
    /// </summary>
    public Roles Rol { get; set; }
}