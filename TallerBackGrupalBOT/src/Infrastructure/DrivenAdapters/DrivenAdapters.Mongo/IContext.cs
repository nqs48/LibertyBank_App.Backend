using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.entities;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Interfaz Mongo context contract.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Colección de tipo <see cref="Usuario"/>
        /// </summary>
        public IMongoCollection<UsuarioEntity> Usuarios { get; }

        public IMongoCollection<ClienteEntity> Clientes { get; }
    }
}