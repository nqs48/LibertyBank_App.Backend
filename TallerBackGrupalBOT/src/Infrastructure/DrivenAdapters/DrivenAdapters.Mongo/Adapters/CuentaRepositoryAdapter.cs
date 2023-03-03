using AutoMapper;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Adapters
{
    /// <summary>
    /// <see cref="ICuentaRepository"/>
    /// </summary>
    public class CuentaRepositoryAdapter : ICuentaRepository
    {

        private readonly IMongoCollection<CuentaEntity> _collectionCuenta;

        private readonly FilterDefinitionBuilder<CuentaEntity> filtro = Builders<CuentaEntity>.Filter;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor <see cref="ICuentaRepository"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public CuentaRepositoryAdapter(IContext context, IMapper mapper)
        {
            _collectionCuenta = context.Cuentas;
            _mapper = mapper;
        }

        /// <summary>
        /// <see cref="ICuentaRepository.Actualizar(string, Cuenta)"/>
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> Actualizar(string IdCuenta, Cuenta cuenta)
        {
            await _collectionCuenta.ReplaceOneAsync(
                               filtro.Eq(x => x.Id, IdCuenta),
                                           _mapper.Map<CuentaEntity>(cuenta));
            return cuenta;
        }

        /// <summary>
        /// <see cref="ICuentaRepository.Crear"/>
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> Crear(Cuenta cuenta)
        {
            var nuevaCuenta = _mapper.Map<CuentaEntity>(cuenta);
            await _collectionCuenta.InsertOneAsync(nuevaCuenta);
            return _mapper.Map<Cuenta>(nuevaCuenta);
        }

        /// <summary>
        /// <see cref="ICuentaRepository.ObtenerPorId"/>
        /// </summary>
        /// <param name="IdCuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> ObtenerPorId(string IdCuenta)
        {
            IAsyncCursor<CuentaEntity> cursor= await _collectionCuenta.FindAsync(x => x.Id == IdCuenta);
            var cuentaEncontrada= cursor.FirstOrDefaultAsync();
            if(cuentaEncontrada is null)
            {
                return null;
            }
            return _mapper.Map<Cuenta>(cuentaEncontrada);

        }

        /// <summary>
        /// <see cref="ICuentaRepository.ObtenerTodos"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cuenta>> ObtenerTodos()
        {
            var cursor= await _collectionCuenta.FindAsync(x => true);
            var cuentasEncontradas= cursor.ToListAsync();
            if(cuentasEncontradas is null)
            {
                return null;
            }
            return _mapper.Map<List<Cuenta>>(cuentasEncontradas);
        }
    }
}
