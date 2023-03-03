using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.UseCase.Common;
using Domain.UseCase.Cuentas;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoints.ReactiveWeb.Controllers
{
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
            Cuenta cuenta = await _cuentaUseCase.ObtenerCuentaPorId(id);
            return _mapper.Map<CuentaHandler>(cuenta);
        }, "");

        
    }
}
