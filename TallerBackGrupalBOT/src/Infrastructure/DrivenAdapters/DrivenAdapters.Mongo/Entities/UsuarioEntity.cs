using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DrivenAdapters.Mongo.Entities;

/// <summary>
/// DTO de entidad <see cref="Usuario"/>
/// </summary>
public class UsuarioEntity : EntityBase
{
    /// <summary>
    /// Nombre completo
    /// </summary>
    [BsonElement("nombre_completo")]
    public string NombreCompleto { get; set; }

    /// <summary>
    /// Rol
    /// </summary>
    [JsonConverter(typeof(Roles))]
    [BsonRepresentation(BsonType.String)]
    public Roles Rol { get; set; }
}