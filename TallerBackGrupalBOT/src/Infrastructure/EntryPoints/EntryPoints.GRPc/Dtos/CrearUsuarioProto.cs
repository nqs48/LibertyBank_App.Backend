using Domain.Model.Entities.Usuarios;

namespace EntryPoints.GRPc.Dtos;

public class CrearUsuarioProto
{
    public string NombreCompleto { get; set; }

    public Roles Rol { get; set; }
}