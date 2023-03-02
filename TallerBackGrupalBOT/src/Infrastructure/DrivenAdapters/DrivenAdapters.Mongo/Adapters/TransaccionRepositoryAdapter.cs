using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Transacciones;
using DrivenAdapters.Mongo.entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
/// Adaptador de entidad <see cref="Transacción"/>
/// </summary>
public class TransacciónRepositoryAdapter : ITransacciónRepository
{
    private readonly IMongoCollection<TransacciónEntity> _mongoTransacciónCollection;
    private readonly IMapper _mapper;

    /// <summary>
    /// Crea una instancia de repositorio <see cref="TransacciónRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    /// <param name="mapper"></param>
    public TransacciónRepositoryAdapter(IContext mongoDb, IMapper mapper)
    {
        _mongoTransacciónCollection = mongoDb.Transacciones;
        _mapper = mapper;
    }

    /// <summary>
    /// <see cref="ITransacciónRepository.ObtenerPorId"/>
    /// </summary>
    /// <param name="IdTransacción"></param>
    /// <returns></returns>
    public async Task<Transacción> ObtenerPorId(string IdTransacción)
    {
        IAsyncCursor<TransacciónEntity> transacciónCursor =
            await _mongoTransacciónCollection.FindAsync(transacción => transacción.Id == IdTransacción);

        var transacciónSeleccionada = transacciónCursor.FirstOrDefaultAsync();

        return transacciónSeleccionada is null ? null : _mapper.Map<Transacción>(transacciónSeleccionada);
    }

    /// <summary>
    /// <see cref="ITransacciónRepository.ObtenerPorIdCuenta"/>
    /// </summary>
    /// <param name="IdCuenta"></param>
    /// <returns></returns>
    public async Task<List<Transacción>> ObtenerPorIdCuenta(string IdCuenta)
    {
        // TODO: Falta CuentaEntity para poder realizar este método
        throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="ITransacciónRepository.Crear"/>
    /// </summary>
    /// <param name="transacción"></param>
    /// <returns></returns>
    public async Task<Transacción> Crear(Transacción transacción)
    {
        var transacciónEntity = _mapper.Map<TransacciónEntity>(transacción);
        await _mongoTransacciónCollection.InsertOneAsync(transacciónEntity);

        return _mapper.Map<Transacción>(transacciónEntity);
    }
}