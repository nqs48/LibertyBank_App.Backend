using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities;
using Helpers.ObjectsUtils;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
        /// <param name="idCuenta"></param>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> Actualizar(string idCuenta, Cuenta cuenta)
        {
            await _collectionCuenta.ReplaceOneAsync(
                filtro.Eq(x => x.Id, idCuenta),
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
        /// <see cref="ICuentaRepository.ObtenerPorCliente(string)"/>
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public async Task<List<Cuenta>> ObtenerPorCliente(string idCliente)
        {
            IAsyncCursor<CuentaEntity> cursorCuentas = await _collectionCuenta.FindAsync(Builders<CuentaEntity>.Filter.Empty);

            List<Cuenta> cuentasCliente = cursorCuentas.ToEnumerable().Select(cuentaEntity => _mapper.Map<Cuenta>(cuentaEntity)).ToList();
            List<Cuenta> cuentasClienteFiltradas = cuentasCliente.Where(cuenta => cuenta.IdCliente == idCliente).ToList();

            return cuentasCliente is null ? null : cuentasClienteFiltradas;
        }

        /// <summary>
        /// <see cref="ICuentaRepository.ObtenerPorId"/>
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <returns></returns>
        public async Task<Cuenta> ObtenerPorId(string idCuenta)
        {
            var filter = Builders<CuentaEntity>.Filter.Eq(usuario => usuario.Id, idCuenta);
            var result = await _collectionCuenta.Find(filter).FirstOrDefaultAsync();
            return result is null ? null : _mapper.Map<Cuenta>(result);
        }

        /// <summary>
        /// <see cref="ICuentaRepository.ObtenerTodos"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cuenta>> ObtenerTodos()
        {

            IAsyncCursor<CuentaEntity> cursorCuentas = await _collectionCuenta.FindAsync(Builders<CuentaEntity>.Filter.Empty);

            List<Cuenta> cuentas = cursorCuentas.ToEnumerable().Select(cuentaEntity => _mapper.Map<Cuenta>(cuentaEntity)).ToList();
            if(cuentas is null)
            {
                return null;
            }
            return cuentas;
        }
    }
}