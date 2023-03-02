using Domain.Model.Entities.Usuarios;
using Xunit;

namespace Domain.Model.Tests.Entities;

public class UsuarioTest
{
    [Theory]
    [InlineData("32423", Roles.Admin)]
    public void ValidarEsAdmin_Verdadero(string id, Roles rol)
    {
        var usuario = new UsuarioBuilder()
            .WithId(id)
            .Build();

        bool esAdmin = usuario.EsAdmin(rol);

        Assert.NotNull(usuario);
        Assert.True(esAdmin);
    }

    [Theory]
    [InlineData("32423", Roles.Transaccional)]
    public void ValidarEsAdmin_Falso(string id, Roles rol)
    {
        var usuario = new UsuarioBuilder()
            .WithId(id)
            .Build();

        bool esAdmin = usuario.EsAdmin(rol);

        Assert.NotNull(usuario);
        Assert.False(esAdmin);
    }
}