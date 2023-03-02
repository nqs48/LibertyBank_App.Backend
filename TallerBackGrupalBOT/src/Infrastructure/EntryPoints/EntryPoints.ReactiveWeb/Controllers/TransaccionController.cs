using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Transacciones;
using Domain.UseCase.Common;
using Domain.UseCase.Transacciones;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers;

/// <summary>
/// Controller de entidad <see cref="Transacción"/>
/// </summary>
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/[controller]/[action]")]
public class TransacciónController : AppControllerBase<TransacciónController>
{
    private readonly ITransacciónUseCase _transacciónUseCase;
    private readonly IMapper _mapper;

    /// <summary>
    /// Crea una instancia de <see cref="TransacciónController"/>
    /// </summary>
    /// <param name="eventsService"></param>
    /// <param name="transacciónUseCase"></param>
    /// <param name="mapper"></param>
    public TransacciónController(IManageEventsUseCase eventsService, ITransacciónUseCase transacciónUseCase,
        IMapper mapper) : base(eventsService)
    {
        _transacciónUseCase = transacciónUseCase;
        _mapper = mapper;
    }

    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Transacción"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("id")]
    public Task<IActionResult> ObtenerTransacciónPorId([FromQuery] string id) => 
        HandleRequest(async () =>
    {
        Transacción transacción = await _transacciónUseCase.ObtenerTransacciónPorId(id);
        return _mapper.Map<TransacciónHandler>(transacción);
    }, "");
}