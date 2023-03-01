using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Usuarios;
using Domain.UseCase.Common;
using Domain.UseCase.Usuarios;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Commands;
using EntryPoints.ReactiveWeb.Entities.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers;

/// <summary>
/// Controller de entidad <see cref="Usuario"/>
/// </summary>
/// 
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class UsuarioController : AppControllerBase<UsuarioController>
{
    private readonly IUsuarioUseCase _usuarioUseCase;
    private readonly IMapper _mapper;

    /// <summary>
    /// Crea una instancia de <see cref="UsuarioController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="usuarioUseCase"></param>
    /// <param name="mapper"></param>
    public UsuarioController(IManageEventsUseCase eventsService, IUsuarioUseCase usuarioUseCase, IMapper mapper) :
        base(eventsService)
    {
        _usuarioUseCase = usuarioUseCase;
        _mapper = mapper;
    }

    /// <summary>
    /// Endpoint que retorna todas las entidades de tipo <see cref="Usuario"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> ObtenerTodos() =>
        HandleRequest(async () =>
        {
            IEnumerable<Usuario> usuarios = await _usuarioUseCase.ObtenerTodos();
            return _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
        }, "");

    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Usuario"/> por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> ObtenerPorId([FromQuery] string id) =>
        HandleRequest(async () =>
        {
            Usuario usuario = await _usuarioUseCase.ObtenerPorId(id);
            return _mapper.Map<UsuarioResponse>(usuario);
        }, "");

    /// <summary>
    /// Endpoint para crear entidad de tipo <see cref="Usuario"/>
    /// </summary>
    /// <param name="crearUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IActionResult> Create([FromBody] CrearUsuario crearUsuario) => HandleRequest(async () =>
    {
        Usuario usuarioCreado = await _usuarioUseCase.Crear(_mapper.Map<Usuario>(crearUsuario));

        return _mapper.Map<UsuarioResponse>(usuarioCreado);
    }, "");
}