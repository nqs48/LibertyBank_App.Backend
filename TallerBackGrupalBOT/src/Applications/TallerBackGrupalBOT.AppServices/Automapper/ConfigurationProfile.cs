using AutoMapper;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities;
using EntryPoints.ReactiveWeb.Entities.Commands;
using EntryPoints.ReactiveWeb.Entities.Handlers;
using DrivenAdapters.Mongo.entities;

namespace TallerBackGrupalBOT.AppServices.Automapper
{
    /// <summary>
    /// EntityProfile
    /// </summary>
    public class ConfigurationProfile : Profile
    {
        /// <summary>
        /// ConfigurationProfile
        /// </summary>
        public ConfigurationProfile()
        {
            CreateMap<Usuario, UsuarioEntity>().ReverseMap();

            CreateMap<Transacción, TransacciónEntity>().ReverseMap();

            CreateMap<Usuario, UsuarioHandler>();

            CreateMap<Transacción, TransacciónHandler>();

            CreateMap<CrearUsuario, Usuario>();

            CreateMap<ClienteEntity, Cliente>().ReverseMap();

            CreateMap<CrearCliente, Cliente>();
        }
    }
}