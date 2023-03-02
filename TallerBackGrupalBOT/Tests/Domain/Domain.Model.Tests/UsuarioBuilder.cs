using Domain.Model.Entities.Usuarios;

namespace Domain.Model.Tests;

public class UsuarioBuilder
{
    private string _id = string.Empty;
    private string _nombreCompleto = string.Empty;
    private Roles _rol;

    public UsuarioBuilder()
    {
    }

    public UsuarioBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public UsuarioBuilder WithNombreCompleto(string nombreCompleto)
    {
        _nombreCompleto = nombreCompleto;
        return this;
    }

    public UsuarioBuilder WithRol(Roles rol)
    {
        _rol = rol;
        return this;
    }

    public Usuario Build() => new(_id, _nombreCompleto, _rol);
}