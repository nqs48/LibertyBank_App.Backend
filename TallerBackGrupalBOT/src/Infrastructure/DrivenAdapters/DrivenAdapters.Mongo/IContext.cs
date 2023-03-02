using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.entities;
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
        
        /// <summary>
        /// Colección de tipo <see cref="Transacción"/>
        /// </summary>
        public IMongoCollection<TransacciónEntity> Transacciones { get; }
    }
}