using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Usuarios;
using Domain.UseCase.Clientes;
using Domain.UseCase.Common;
using Domain.UseCase.Cuentas;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Commands;
using EntryPoints.ReactiveWeb.Entities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers
{
    /// <summary>
    /// Controlador de <see cref="Cuenta"/> implementando <see cref="CuentaUseCase"/>
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CuentaController : AppControllerBase<CuentaController>
    {

        private readonly ICuentaUseCase _cuentaUseCase;
        private readonly IMapper _mapper;


        /// <summary>
        /// Crea una instancia de <see cref="CuentaController"/>
        /// </summary>
        /// <param name="eventsService"></param>
        /// <param name="cuentaUseCase"></param>
        /// <param name="mapper"></param>
        public CuentaController(IManageEventsUseCase eventsService, ICuentaUseCase cuentaUseCase, IMapper mapper) : base(eventsService)
        {
            _cuentaUseCase = cuentaUseCase;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint que retorna una entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public Task<IActionResult> ObtenerCuentaPorId([FromQuery] string id) => HandleRequest(async () =>
        {
            return await _cuentaUseCase.ObtenerCuentaPorId(id);   
        }, "");


        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="crearCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add/cuenta/Id")]
        public Task<IActionResult> Crear(string idUsuario, CrearCuenta crearCuenta) => HandleRequest(async () =>
        {

            Cuenta cuentaMapeada = _mapper.Map<Cuenta>(crearCuenta);
            Cuenta cuenta = await _cuentaUseCase.Crear(idUsuario,cuentaMapeada);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="crearCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> ObtenerCuentas() => await HandleRequest(async () =>
        {
            return await _cuentaUseCase.ObtenerTodas();
        }, "");



    }
}
