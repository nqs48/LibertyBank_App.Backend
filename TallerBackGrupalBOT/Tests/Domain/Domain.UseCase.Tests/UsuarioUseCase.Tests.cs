using credinet.exception.middleware.models;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Domain.Model.Tests;
using Domain.UseCase.Usuarios;
using Helpers.Commons.Exceptions;
using Moq;
using Xunit;

namespace Domain.UseCase.Tests;

public class UsuarioUseCaseTest
{
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly UsuarioUseCase _usuarioUseCase;

    public UsuarioUseCaseTest()
    {
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _usuarioUseCase = new UsuarioUseCase(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task ObtenerTodos_Usuarios_Exitoso()
    {
        _mockUsuarioRepository
            .Setup(usuario => usuario.ObtenerTodosAsync())
            .ReturnsAsync(ObtenerListaUsuariosTest);

        List<Usuario> usuarios = await _usuarioUseCase.ObtenerTodos();

        Assert.NotNull(usuarios);
        Assert.NotEmpty(usuarios);
        _mockUsuarioRepository.Verify(mock => mock.ObtenerTodosAsync(), Times.Once);
    }

    [Fact]
    public async Task ObtenerTodos_Usuarios_SinDatos()
    {
        _mockUsuarioRepository
            .Setup(usuario => usuario.ObtenerTodosAsync())
            .ReturnsAsync(new List<Usuario>());

        List<Usuario> usuarios = await _usuarioUseCase.ObtenerTodos();

        Assert.NotNull(usuarios);
        Assert.Empty(usuarios);
        _mockUsuarioRepository.Verify(mock => mock.ObtenerTodosAsync(), Times.Once);
    }


    [Fact]
    public async Task ObtenerUsuarioPorId_Exitoso()
    {
        _mockUsuarioRepository
            .Setup(usuario => usuario.ObtenerPorIdAsync(It.IsAny<string>()))
            .ReturnsAsync(ObtenerUsuarioTest);

        var usuario = await _usuarioUseCase.ObtenerPorId(It.IsAny<string>());

        Assert.NotNull(usuario);
        _mockUsuarioRepository.Verify(mock => mock.ObtenerPorIdAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task ObtenerUsuarioPorId_Retorna_Excepcion()
    {
        BusinessException businessException =
            await Assert.ThrowsAsync<BusinessException>(async () =>
                await _usuarioUseCase.ObtenerPorId(It.IsAny<string>()));

        Assert.Equal((int)TipoExcepcionNegocio.EntidadNoEncontrada, businessException.code);
        _mockUsuarioRepository.Verify(mock => mock.ObtenerPorIdAsync(It.IsAny<string>()), Times.Once);
    }

    [Theory]
    [InlineData("4534", "Pepito Perez", Roles.Admin)]
    [InlineData("4525", "Jorge Luis Rodriguez", Roles.Transaccional)]
    [InlineData("4587", "Maicol Ferguson", Roles.Admin)]
    public async Task Crear_Usuario_Exitoso(string id, string nombreCompleto, Roles rol)
    {
        Usuario usuario = new UsuarioBuilder()
            .WithId(id)
            .WithNombreCompleto(nombreCompleto)
            .WithRol(rol)
            .Build();

        _mockUsuarioRepository
            .Setup(mock => mock.CrearAsync(It.IsAny<Usuario>()))
            .ReturnsAsync(usuario);

        var usuarioCreado = await _usuarioUseCase.Crear(usuario);

        Assert.NotNull(usuarioCreado);
        Assert.Equal(usuario.Id, usuarioCreado.Id);
        Assert.Equal(usuario.NombreCompleto, usuarioCreado.NombreCompleto);
        _mockUsuarioRepository.Verify(mock => mock.CrearAsync(It.IsAny<Usuario>()), Times.Once);
    }

    #region Private Methods

    private Usuario ObtenerUsuarioTest() => new UsuarioBuilder()
        .WithId("5262")
        .WithNombreCompleto("Juan Pablo Cano")
        .WithRol(Roles.Admin)
        .Build();

    private List<Usuario> ObtenerListaUsuariosTest() => new()
    {
        new UsuarioBuilder()
            .WithId("5463")
            .WithNombreCompleto("Pepito Perez")
            .WithRol(Roles.Transaccional)
            .Build(),

        new UsuarioBuilder()
            .WithId("0870")
            .WithNombreCompleto("Lucas Montenegro")
            .WithRol(Roles.Transaccional)
            .Build()
    };

    #endregion
}