using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.Adapters
{
    /// <summary>
    /// <see cref="IClienteRepository"/>
    /// </summary>
    public class ClienteRepositoryAdapter : IClienteRepository
    {
        private readonly FilterDefinitionBuilder<ClienteEntity> filtro = Builders<ClienteEntity>.Filter;

        private readonly IMongoCollection<ClienteEntity> _collection;

        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de <see cref="IClienteRepository"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public ClienteRepositoryAdapter(IContext context, IMapper mapper)
        {
            _collection = context.Clientes;
            _mapper = mapper;
        }

        /// <summary>
        /// <see cref="IClienteRepository.ActualizarAsync(string, Cliente)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<Cliente> ActualizarAsync(string IdCliente, Cliente cliente)
        {
            await _collection.ReplaceOneAsync(
                filtro.Eq(x => x.Id, IdCliente),
                _mapper.Map<ClienteEntity>(cliente));

            return cliente;
        }

        /// <summary>
        /// <see cref="IClienteRepository.CrearAsync(string, Cliente)"/>
        /// </summary>
        /// <param name="idUsuarioCreacion"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<Cliente> CrearAsync(string idUsuarioCreacion, Cliente cliente)
        {
            var nuevoCliente = _mapper.Map<ClienteEntity>(cliente);
            await _collection.InsertOneAsync(nuevoCliente);

            return _mapper.Map<Cliente>(nuevoCliente);
        }

        /// <summary>
        /// <see cref="IClienteRepository.ObtenerPorIdAsync(string)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        public async Task<Cliente> ObtenerPorIdAsync(string IdCliente)
        {
            var cursor = await _collection.FindAsync<ClienteEntity>(filtro.Eq(x => x.Id, IdCliente));
            return _mapper.Map<Cliente>(cursor.FirstOrDefault());
        }

        /// <summary>
        /// <see cref="IClienteRepository.ObtenerPorNumeroIdentificacion(string)"/>
        /// </summary>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public async Task<Cliente> ObtenerPorNumeroIdentificacion(string numeroIdentificacion)
        {
            var cursor = await _collection.FindAsync<ClienteEntity>(
                filtro.Eq(x => x.NumeroIdentificación, numeroIdentificacion));
            return _mapper.Map<Cliente>(cursor.FirstOrDefault());
        }

        /// <summary>
        /// <see cref="IClienteRepository.ObtenerTodosAsync"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var cursor = await _collection.FindAsync<ClienteEntity>(_ => true);
            return cursor.ToList()
                .Select(x => _mapper.Map<Cliente>(x))
                .ToList();
        }
    }
}