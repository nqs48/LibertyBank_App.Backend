using AutoMapper;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.entities;
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
        }
    }
}