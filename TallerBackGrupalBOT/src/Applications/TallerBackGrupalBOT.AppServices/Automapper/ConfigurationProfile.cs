using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using Domain.Model.Entities.Usuarios;
using DrivenAdapters.Mongo.entities;
using DrivenAdapters.Mongo.Entities;
using EntryPoints.GRPc.Dtos;
using EntryPoints.GRPc.Protos;
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

            #endregion Domain Models to Mongo Documents

            #region Domain Models To REST Handlers

            CreateMap<Usuario, UsuarioHandler>();

            CreateMap<Transacción, TransacciónHandler>();

            CreateMap<Cuenta, CuentaHandler>().ReverseMap();

            #endregion Domain Models To REST Handlers

            #region REST Commands to Domain Models

            CreateMap<CrearUsuario, Usuario>();

            CreateMap<CrearCliente, Cliente>();

            CreateMap<CrearTransacción, Transacción>();

            CreateMap<Cuenta, CuentaHandler>().ReverseMap();

            CreateMap<CrearCuenta, Cuenta>();

            CreateMap<EstadosCuenta, Cuenta>();

            #endregion REST Commands to Domain Models

            #region GRPc command to HTTP command

            CreateMap<CrearUsuarioRequest, CrearUsuario>();
            CreateMap<CrearClienteProto, CrearCliente>();

            #endregion GRPc command to HTTP command

            #region GRPc command to GRPc DTO

            CreateMap<CrearUsuarioRequest, CrearUsuarioProto>().ReverseMap();

            #endregion GRPc command to GRPc DTO

            #region GRPc DTO to Domain Model

            CreateMap<CrearUsuarioProto, Usuario>().ReverseMap();
            CreateMap<CrearClienteProto, Cliente>().ReverseMap();

            #endregion GRPc DTO to Domain Model

            #region Domain Model to GRPc Model

            CreateMap<Usuario, UsuarioProto>();
            CreateMap<Cliente, ClienteProto>();

            #endregion Domain Model to GRPc Model
        }
    }
}