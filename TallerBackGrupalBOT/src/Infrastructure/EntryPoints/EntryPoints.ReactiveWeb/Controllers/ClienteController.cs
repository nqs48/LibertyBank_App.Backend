using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.UseCase.Clientes;
using Domain.UseCase.Common;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Controllers
{
    /// <summary>
    /// Controlador de <see cref="Cliente"/> implementando <see cref="ClienteUseCase"/>
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/clientes")]
    public class ClienteController : AppControllerBase<ClienteController>
    {
        private readonly IClienteUseCase _useCase;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de <see cref="ClienteController"/>
        /// </summary>
        /// <param name="eventsUseCase"></param>
        /// <param name="useCase"></param>
        /// <param name="mapper"></param>
        public ClienteController(IManageEventsUseCase eventsUseCase, IClienteUseCase useCase, IMapper mapper) :
            base(eventsUseCase)
        {
            _useCase = useCase;
            _mapper = mapper;
        }

        /// <summary>
        /// <see cref="ClienteUseCase.ActualizarCorreoElectronico(string, string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="nuevoCorreo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update/email/{idCliente}/{nuevoCorreo}")]
        public Task<IActionResult> ActualizarCorreoCliente(string idCliente, string nuevoCorreo) =>
            HandleRequest(async () => await _useCase.ActualizarCorreoElectronico(idCliente, nuevoCorreo), "");

        /// <summary>
        /// <see cref="ClienteUseCase.CrearCliente(string, Cliente)"/>
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nuevoCliente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add/{idUsuario}")]
        public Task<IActionResult> AgregarCliente(string idUsuario, CrearCliente nuevoCliente) =>
            HandleRequest(async () => await _useCase.CrearCliente(idUsuario, _mapper.Map<Cliente>(nuevoCliente)), "");

        /// <summary>
        /// <see cref="ClienteUseCase.DeshabilitarCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/{idCliente}")]
        public Task<IActionResult> DeshabilitarCliente(string idCliente) =>
            HandleRequest(async () => await _useCase.DeshabilitarCliente(idCliente), "");

        /// <summary>
        /// <see cref="ClienteUseCase.DeshabilitarDeudaCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("del/deuda/{idCliente}")]
        public Task<IActionResult> DeshabilitarDeudaCliente(string idCliente) =>
            HandleRequest(async () => await _useCase.DeshabilitarDeudaCliente(idCliente), "");

        /// <summary>
        /// <see cref="ClienteUseCase.HabilitarCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("enable/{idCliente}")]
        public Task<IActionResult> HabilitarCliente(string idCliente) =>
            HandleRequest(async () => await _useCase.HabilitarCliente(idCliente), "");

        /// <summary>
        /// <see cref="ClienteUseCase.HabilitarDeudaCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("enable/deuda/{idCliente}")]
        public Task<IActionResult> HabilitarDeudaCliente(string idCliente) =>
            HandleRequest(async () => await _useCase.HabilitarDeudaCliente(idCliente), "");

        /// <summary>
        /// <see cref="ClienteUseCase.ObtenerClientePorId(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCliente}")]
        public Task<IActionResult> ObtenerClientePorId(string idCliente) =>
                HandleRequest(async () => await _useCase.ObtenerClientePorId(idCliente), "");

        /// <summary>
        /// <see cref="ClienteUseCase.ObtenerTodos()"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> ObtenerTodosClientes() =>
            HandleRequest(async () => await _useCase.ObtenerTodos(), "");
    }
}