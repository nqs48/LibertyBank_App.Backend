using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.entities;
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
            #region Domain Models to Mongo Documents

            CreateMap<Usuario, UsuarioEntity>().ReverseMap();
            CreateMap<Transacción, TransacciónEntity>().ReverseMap();
            CreateMap<Cliente, ClienteEntity>().ReverseMap();
            CreateMap<Cuenta, CuentaEntity>().ReverseMap();

            #endregion

            #region Domain Models To REST Handlers

            CreateMap<Usuario, UsuarioHandler>();

            CreateMap<Transacción, TransacciónHandler>();

            CreateMap<Cuenta, CuentaHandler>().ReverseMap();

            #endregion

            #region REST Commands to Domain Models

            CreateMap<CrearUsuario, Usuario>();

            CreateMap<CrearCliente, Cliente>();

            CreateMap<CrearTransacción, Transacción>();

            #endregion
        }
    }
}