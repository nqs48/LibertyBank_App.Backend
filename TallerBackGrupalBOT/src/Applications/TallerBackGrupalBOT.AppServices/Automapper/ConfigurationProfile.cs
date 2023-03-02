using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.Entities;
using EntryPoints.ReactiveWeb.Entities.Commands;
using EntryPoints.ReactiveWeb.Entities.Handlers;

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

            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<CrearUsuario, Usuario>();

            CreateMap<ClienteEntity, Cliente>().ReverseMap();

            CreateMap<CrearCliente, Cliente>();
        }
    }
}