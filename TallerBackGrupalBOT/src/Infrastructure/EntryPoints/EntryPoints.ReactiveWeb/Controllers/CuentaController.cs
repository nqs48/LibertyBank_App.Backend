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
    [Route("api/[controller]/[action]")]
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
        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> ObtenerId(string id) => HandleRequest(async () =>
            await _cuentaUseCase.ObtenerCuentaPorId(id), "");


        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="crearCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{idUsuario}")] 
        public Task<IActionResult> Crear(string idUsuario, [FromBody] CrearCuenta crearCuenta) => HandleRequest(async () =>
        {
            Cuenta cuentaMapeada = _mapper.Map<Cuenta>(crearCuenta);
            Cuenta cuenta = await _cuentaUseCase.Crear(idUsuario, cuentaMapeada);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="estadoCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{idUsuario}")]
        public Task<IActionResult> Cancelar(string idUsuario, [FromBody] EstadosCuenta estadoCuenta) => HandleRequest(async () =>
        {
            Cuenta cuentaMapeada = _mapper.Map<Cuenta>(estadoCuenta);
            Cuenta cuenta = await _cuentaUseCase.CancelarCuenta(idUsuario, cuentaMapeada);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="estadoCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{idUsuario}")]
        public Task<IActionResult> Habilitar(string idUsuario, [FromBody] EstadosCuenta estadoCuenta) => HandleRequest(async () =>
        {
            Cuenta cuentaMapeada = _mapper.Map<Cuenta>(estadoCuenta);
            Cuenta cuenta = await _cuentaUseCase.HabilitarCuenta(idUsuario, cuentaMapeada);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        /// <summary>
        /// Endpoint para crear entidad de tipo <see cref="Cuenta"/>
        /// </summary>
        /// <param name="estadoCuenta"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{idUsuario}")]
        public Task<IActionResult> Deshabilitar(string idUsuario, [FromBody] EstadosCuenta estadoCuenta) => HandleRequest(async () =>
        {
            Cuenta cuentaMapeada = _mapper.Map<Cuenta>(estadoCuenta);
            Cuenta cuenta = await _cuentaUseCase.DeshabilitarCuenta(idUsuario, cuentaMapeada);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        /// <summary>
        /// Endpoint para obtener todas las entidades tipo <see cref="Cuenta"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Obtener() => await HandleRequest(async () =>
        {
            return await _cuentaUseCase.ObtenerTodas();
        }, "");

        /// <summary>
        /// Endpoint para obtener lista de entidades de tipo <see cref="Cuenta"/> por cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCliente}")]
        public async Task<IActionResult> ObtenerCuentasPorId(string idCliente) => await HandleRequest(async () =>
        {
            return await _cuentaUseCase.ObtenerTodasPorCliente(idCliente);
        }, "");



    }
}
