using AutoMapper;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Transacciones;
using DrivenAdapters.Mongo.entities;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
/// Adaptador de entidad <see cref="Transacción"/>
/// </summary>
public class TransacciónRepositoryAdapter : ITransacciónRepository
{
    private readonly IMongoCollection<TransacciónEntity> _mongoTransacciónCollection;
    private readonly IMongoCollection<CuentaEntity> _mongoCuentaCollection;
    private readonly IMapper _mapper;

    /// <summary>
    /// Crea una instancia de repositorio <see cref="TransacciónRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    /// <param name="mapper"></param>
    public TransacciónRepositoryAdapter(IContext mongoDb, IMapper mapper)
    {
        _mongoTransacciónCollection = mongoDb.Transacciones;
        _mongoCuentaCollection = mongoDb.Cuentas;
        _mapper = mapper;
    }

    /// <summary>
    /// <see cref="ITransacciónRepository.ObtenerPorId"/>
    /// </summary>
    /// <param name="idTransacción"></param>
    /// <returns></returns>
    public async Task<Transacción> ObtenerPorId(string idTransacción)
    {
        IAsyncCursor<TransacciónEntity> transacciónCursor =
            await _mongoTransacciónCollection.FindAsync(transacción => transacción.Id == idTransacción);

        TransacciónEntity transacciónSeleccionada = await transacciónCursor.FirstOrDefaultAsync();

        return transacciónSeleccionada is null ? null : _mapper.Map<Transacción>(transacciónSeleccionada);
    }

    /// <summary>
    /// <see cref="ITransacciónRepository.ObtenerPorIdCuenta"/>
    /// </summary>
    /// <param name="idCuenta"></param>
    /// <returns></returns>
    public async Task<List<Transacción>> ObtenerPorIdCuenta(string idCuenta)
    {
        IAsyncCursor<CuentaEntity> cuentaCursor =
            await _mongoCuentaCollection.FindAsync(cuenta => cuenta.Id == idCuenta);

        CuentaEntity cuentaEntity = await cuentaCursor.FirstOrDefaultAsync();

        IAsyncCursor<TransacciónEntity> transacciónCursor =
            await _mongoTransacciónCollection.FindAsync(transacción => transacción.IdCuenta == cuentaEntity.Id);

        return transacciónCursor
            .ToList()
            .Select(transacción => _mapper.Map<Transacción>(transacción))
            .ToList();
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