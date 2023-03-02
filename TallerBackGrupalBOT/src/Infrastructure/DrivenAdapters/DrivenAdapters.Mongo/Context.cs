using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities;
using Domain.Model.Entities.Clientes;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Context is an implementation of <see cref="IContext"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;

        /// <summary>
        /// crea una nueva instancia de la clase <see cref="Context"/>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public Context(string connectionString, string databaseName)
        {
            MongoClient _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(databaseName);
        }

        /// <summary>
        /// Tipo de contrato <see cref="Usuario"/>
        /// </summary>
        public IMongoCollection<UsuarioEntity> Usuarios => _database.GetCollection<UsuarioEntity>("Usuarios");

        /// <summary>
        /// Colección en DB de <see cref="Cliente"/>
        /// </summary>
        public IMongoCollection<ClienteEntity> Clientes => _database.GetCollection<ClienteEntity>("Clientes");
    }
}