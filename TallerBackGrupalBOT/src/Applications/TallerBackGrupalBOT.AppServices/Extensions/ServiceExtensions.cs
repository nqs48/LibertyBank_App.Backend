﻿using AutoMapper.Data;
using credinet.comun.api;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Clientes;
using Domain.UseCase.Common;
using Domain.UseCase.Cuentas;
using Domain.UseCase.Transacciones;
using Domain.UseCase.Usuarios;
using DrivenAdapters.Mongo;
using DrivenAdapters.Mongo.Adapters;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using TallerBackGrupalBOT.AppServices.Automapper;

namespace TallerBackGrupalBOT.AppServices.Extensions
{
    /// <summary>
    /// Service Extensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers the cors.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterCors(this IServiceCollection services, string policyName) =>
            services.AddCors(o => o.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        /// <summary>
        /// Método para registrar AutoMapper
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(cfg => { cfg.AddDataReaderMapping(); }, typeof(ConfigurationProfile));

        /// <summary>
        /// Método para registrar Mongo
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="db">database.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterMongo(this IServiceCollection services, string connectionString,
            string db) =>
            services.AddSingleton<IContext>(provider => new Context(connectionString, db));

        /// <summary>
        /// Método para registrar Redis Cache
        /// </summary>
        /// <param name="services">services.</param>
        /// <param name="connectionString">connection string.</param>
        /// <param name="dbNumber">database number.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterRedis(this IServiceCollection services, string connectionString,
            int dbNumber)
        {
            services.AddSingleton(s => LazyConnection(connectionString).Value.GetDatabase(dbNumber));

            ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(connectionString,
                opt => opt.DefaultDatabase = dbNumber);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            return services;
        }

        /// <summary>
        /// Método para registrar los servicios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Helpers

            services.AddSingleton<IMensajesHelper, MensajesApiHelper>();

            #endregion Helpers

            #region Adaptadores

            services.AddScoped<IUsuarioRepository, UsuarioRepositoryAdapter>();
            services.AddScoped<ICuentaRepository, CuentaRepositoryAdapter>();
            services.AddScoped<IClienteRepository, ClienteRepositoryAdapter>();
            services.AddScoped<ITransacciónRepository, TransacciónRepositoryAdapter>();

            #endregion Adaptadores

            #region UseCases

            services.AddScoped<IManageEventsUseCase, ManageEventsUseCase>();
            services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
            services.AddScoped<ICuentaUseCase, CuentaUseCase>();
            services.AddScoped<IClienteUseCase, ClienteUseCase>();
            services.AddScoped<ITransacciónUseCase, TransacciónUseCase>();

            #endregion UseCases

            return services;
        }

        /// <summary>
        /// Lazies the connection.
        /// </summary>
        /// <param name="connectionString">connection string.</param>
        /// <returns></returns>
        private static Lazy<ConnectionMultiplexer> LazyConnection(string connectionString) =>
            new(() => { return ConnectionMultiplexer.Connect(connectionString); });
    }
}