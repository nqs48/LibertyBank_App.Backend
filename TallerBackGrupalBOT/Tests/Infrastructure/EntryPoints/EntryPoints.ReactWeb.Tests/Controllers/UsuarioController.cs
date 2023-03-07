using System.Net;
using AutoMapper;
using Domain.Model.Entities.Usuarios;
using Domain.Model.Tests;
using Domain.UseCase.Common;
using Domain.UseCase.Usuarios;
using EntryPoints.ReactiveWeb.Controllers;
using EntryPoints.ReactiveWeb.Entities.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using TallerBackGrupalBOT.AppServices.Automapper;
using Xunit;

namespace EntryPoints.ReactWeb.Tests.Controllers;

public class UsuarioControllerTest
{
    private readonly Mock<IUsuarioUseCase> _mockUsuarioUseCase;
    private readonly UsuarioController _usuarioController;

    public UsuarioControllerTest()
    {
        MapperConfiguration mapperConfiguration =
            new MapperConfiguration(options => options.AddProfile<ConfigurationProfile>());

        var mapper = mapperConfiguration.CreateMapper();
        Mock<IManageEventsUseCase> mockManageEventsUseCase = new();
        _mockUsuarioUseCase = new Mock<IUsuarioUseCase>();

        _usuarioController = new UsuarioController(mockManageEventsUseCase.Object, _mockUsuarioUseCase.Object, mapper);
        _usuarioController.ControllerContext.HttpContext = new DefaultHttpContext();
        _usuarioController.ControllerContext.HttpContext.Request.Headers["Location"] = "1,1";
        _usuarioController.ControllerContext.RouteData = new RouteData();
        _usuarioController.ControllerContext.RouteData.Values.Add("controller", "Usuarios");
    }

    [Theory(DisplayName = "Crear retorna el usuario creado con status 200")]
    [InlineData("32423", "Pepito Perez", Roles.Admin)]
    [InlineData("78768", "Marsella Ramirez", Roles.Admin)]
    [InlineData("98003", "Marylin Monroe", Roles.Transaccional)]
    public async Task Crear_Retorna_Status200(string id, string nombreCompleto, Roles rol)
    {
        // Arrange
        CrearUsuario crearUsuario = new CrearUsuario()
        {
            NombreCompleto = nombreCompleto,
            Rol = rol
        };

        Usuario usuario = new UsuarioBuilderTest()
            .WithId(id)
            .WithNombreCompleto(nombreCompleto)
            .WithRol(rol)
            .Build();

        _mockUsuarioUseCase
            .Setup(useCase => useCase.Crear(It.IsAny<Usuario>()))
            .ReturnsAsync(usuario);

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "Crear");

        // Act
        var usuarioCreado = await _usuarioController.Crear(crearUsuario);
        var okObjectResult = usuarioCreado as OkObjectResult;

        // Assert
        Assert.NotNull(usuarioCreado);
        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
    }

    [Fact(DisplayName = "ObtenerTodos retorna una lista con todos los usuarios creados con status 200")]
    public async Task ObtenerTodos_Retorna_Status200()
    {
        // Arrange
        List<Usuario> usuarios = new()
        {
            new Usuario(),
            new Usuario()
        };

        _mockUsuarioUseCase
            .Setup(useCase => useCase.ObtenerTodos())
            .ReturnsAsync(usuarios);

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "ObtenerUsuarios");

        // Act
        var usuariosObtenidos = await _usuarioController.ObtenerTodos();
        var okObjectResult = usuariosObtenidos as OkObjectResult;

        // Assert
        Assert.NotNull(usuariosObtenidos);
        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
    }

    [Theory(DisplayName = "ObtenerPorId retorna un usuario por su id con status 200")]
    [InlineData("32423")]
    [InlineData("78768")]
    [InlineData("98003")]
    public async Task ObtenerPorId_Retorna_Status200(string id)
    {
        // Arrange
        Usuario usuario = new UsuarioBuilderTest()
            .WithId(id)
            .Build();

        _mockUsuarioUseCase
            .Setup(useCase => useCase.ObtenerPorId(usuario.Id))
            .ReturnsAsync(usuario);

        _usuarioController.ControllerContext.RouteData.Values.Add("action", "ObtenerPorId");

        // Act
        var usuarioObtenido = await _usuarioController.ObtenerPorId(usuario.Id);
        var okObjectResult = usuarioObtenido as OkObjectResult;

        // Assert
        Assert.NotNull(usuarioObtenido);
        Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
        Assert.Equal(id, usuario.Id);
    }
}