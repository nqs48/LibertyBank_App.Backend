using Domain.Model.Entities.Usuarios;

namespace Domain.Model.Tests;

public class UsuarioBuilderTest
{
    private string _id = string.Empty;
    private string _nombreCompleto = string.Empty;
    private Roles _rol;

    public UsuarioBuilderTest()
    {
    }

    public UsuarioBuilderTest WithId(string id)
    {
        _id = id;
        return this;
    }

    public UsuarioBuilderTest WithNombreCompleto(string nombreCompleto)
    {
        _nombreCompleto = nombreCompleto;
        return this;
    }

    public UsuarioBuilderTest WithRol(Roles rol)
    {
        _rol = rol;
        return this;
    }

    public Usuario Build() => new(_id, _nombreCompleto, _rol);
}