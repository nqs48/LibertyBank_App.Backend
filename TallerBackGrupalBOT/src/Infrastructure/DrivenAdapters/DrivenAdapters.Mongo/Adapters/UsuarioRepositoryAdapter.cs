using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo.Adapters;

/// <summary>
/// Adaptador de entidad <see cref="Usuario"/>
/// </summary>
public class UsuarioRepositoryAdapter : IUsuarioRepository
{
    private readonly IMongoCollection<UsuarioEntity> _mongoUsuarioCollection;
    private readonly IMapper _mapper;

    /// <summary>
    /// Crea una instancia de repositorio <see cref="UsuarioRepositoryAdapter"/>
    /// </summary>
    /// <param name="mongoDb"></param>
    /// <param name="mapper"></param>
    public UsuarioRepositoryAdapter(IContext mongoDb, IMapper mapper)
    {
        _mongoUsuarioCollection = mongoDb.Usuarios;
        _mapper = mapper;
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.ObtenerTodosAsync"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Usuario>> ObtenerTodosAsync()
    {
        IAsyncCursor<UsuarioEntity> usuarioCursor = await _mongoUsuarioCollection
            .FindAsync(Builders<UsuarioEntity>.Filter.Empty);

        return usuarioCursor
            .ToList()
            .Select(usuario => _mapper.Map<Usuario>(usuario))
            .ToList();
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.ObtenerPorIdAsync"/>
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public async Task<Usuario> ObtenerPorIdAsync(string idUsuario)
    {
        IAsyncCursor<UsuarioEntity> usuarioCursor =
            await _mongoUsuarioCollection.FindAsync(usuario => usuario.Id == idUsuario);

        var usuarioSeleccionado = usuarioCursor.FirstOrDefaultAsync();

        return usuarioSeleccionado is null ? null : _mapper.Map<Usuario>(usuarioSeleccionado);
    }

    /// <summary>
    /// <see cref="IUsuarioRepository.CrearAsync"/>
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public async Task<Usuario> CrearAsync(Usuario usuario)
    {
        var usuarioEntity = _mapper.Map<UsuarioEntity>(usuario);
        await _mongoUsuarioCollection.InsertOneAsync(usuarioEntity);

        return _mapper.Map<Usuario>(usuarioEntity);
    }
}