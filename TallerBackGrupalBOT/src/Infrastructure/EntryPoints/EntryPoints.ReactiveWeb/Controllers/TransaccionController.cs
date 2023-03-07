using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.UseCase.Common;
using Domain.UseCase.Transacciones;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Commands;
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
    [HttpGet("{id}")]
    public Task<IActionResult> ObtenerTransacciónPorId([FromRoute] string id) =>
        HandleRequest(async () =>
        {
            Transacción transacción = await _transacciónUseCase.ObtenerTransacciónPorId(id);
            return _mapper.Map<TransacciónHandler>(transacción);
        }, "");

    /// <summary>
    /// Endpoint que retorna una entidad de tipo <see cref="Transacción"/> por el Id de la entidad <see cref="Cuenta"/>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<IActionResult> ObtenerTransaccionesPorIdCuenta([FromRoute] string id) =>
        HandleRequest(async () =>
        {
            IEnumerable<Transacción> transacciones = await _transacciónUseCase.ObtenerTransaccionesPorIdCuenta(id);
            return _mapper.Map<IEnumerable<TransacciónHandler>>(transacciones);
        }, "");

    /// <summary>
    /// Endpoint para realizar una consignación
    /// </summary>
    /// <param name="crearTransacción"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IActionResult> RealizarConsignación([FromBody] CrearTransacción crearTransacción) =>
        HandleRequest(async () =>
        {
            Transacción transacción =
                await _transacciónUseCase.RealizarConsignación(_mapper.Map<Transacción>(crearTransacción));

            return _mapper.Map<TransacciónHandler>(transacción);
        }, "");
    
    /// <summary>
    /// Endpoint para realizar un retiro
    /// </summary>
    /// <param name="crearTransacción"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IActionResult> RealizarRetiro([FromBody] CrearTransacción crearTransacción) =>
        HandleRequest(async () =>
        {
            Transacción transacción =
                await _transacciónUseCase.RealizarRetiro(_mapper.Map<Transacción>(crearTransacción));

            return _mapper.Map<TransacciónHandler>(transacción);
        }, "");

    /// <summary>
    /// Endpoint para realizar una transferencia
    /// </summary>
    /// <param name="crearTransacción"></param>
    /// <param name="idCuentaReceptor"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IActionResult> RealizarTransferencia([FromBody] CrearTransacción crearTransacción, string idCuentaReceptor) =>
        HandleRequest(async () =>
        {
            Transacción transacción =
                await _transacciónUseCase.RealizarTransferencia(_mapper.Map<Transacción>(crearTransacción), idCuentaReceptor);

            return _mapper.Map<TransacciónHandler>(transacción);
        }, "");
}