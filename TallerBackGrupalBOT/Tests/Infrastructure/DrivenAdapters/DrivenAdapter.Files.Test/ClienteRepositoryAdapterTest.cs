using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Usuarios;
using Domain.Model.Tests;
using DrivenAdapters.Mongo;
using DrivenAdapters.Mongo.Adapters;
using DrivenAdapters.Mongo.Entities;
using EntryPoints.ReactiveWeb.Entities.Commands;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerBackGrupalBOT.AppServices.Automapper;
using Xunit;

namespace DrivenAdapter.Mongo.Tests
{
    public class ClienteRepositoryAdapterTest
    {
        private readonly Mock<IContext> _mockContext;

        private readonly Mock<IMongoCollection<ClienteEntity>> _mockCollectionCliente;

        private readonly Mock<IMongoCollection<UsuarioEntity>> _mockCollectionUsuario;

        private readonly Mock<IAsyncCursor<ClienteEntity>> _mockAsyncCursorCliente;

        private readonly Mock<IAsyncCursor<UsuarioEntity>> _mockAsyncCursorUsuario;

        private readonly IMapper _mapper;

        public ClienteRepositoryAdapterTest()
        {
            _mockContext = new();
            _mockCollectionCliente = new();
            _mockAsyncCursorCliente = new();

            // Configuración Mock de cliente
            _mockCollectionCliente.Object.InsertMany(CrearListaClientesTest());

            _mockAsyncCursorCliente.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _mockAsyncCursorCliente.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            // Configuración Mock de usuario
            _mockCollectionUsuario.Object.InsertMany(CrearListaUsuariosTest());

            _mockAsyncCursorUsuario.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _mockAsyncCursorUsuario.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));

            // Configuración Mapper
            MapperConfiguration mapperConfig = new MapperConfiguration(options => options.AddProfile<ConfigurationProfile>());
            _mapper = mapperConfig.CreateMapper();
        }

        //[Fact]
        //public async Task ActualizarCliente_Correcto()
        //{
        //}

        [Fact]
        public async Task CrearCliente_Correcto()
        {
            // Arrange
            _mockCollectionCliente.Setup(mongo => mongo.InsertOneAsync(
                It.IsAny<ClienteEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()
                ));

            _mockContext.Setup(Context => Context.Usuarios).Returns(_mockCollectionUsuario.Object);
            _mockContext.Setup(context => context.Clientes).Returns(_mockCollectionCliente.Object);
            var repository = new ClienteRepositoryAdapter(_mockContext.Object, _mapper);

            var nuevoCliente = new Cliente("123Id", TipoIdentificación.CE, "identificacion123",
                "Julian", "Mosquera", "julian@gmail.com", new DateOnly(1994, 06, 06));

            // Act
            var result = await repository.CrearAsync("123id", nuevoCliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nuevoCliente.CorreoElectronico, result.CorreoElectronico);
        }

        #region Métodos privados

        private IEnumerable<ClienteEntity> CrearListaClientesTest()
        {
            var list = new List<CrearCliente>();
            list.Add(new CrearCliente(TipoIdentificación.CC, "123Identificacion", "Maria", "Hernandez", "maria@gmail.com",
                new DateOnly(1993, 03, 02)));
            list.Add(new CrearCliente(TipoIdentificación.CE, "345Identificacion", "Carlos", "Carmona", "carlos@gmail.com",
                new DateOnly(1993, 05, 14)));

            return list.Select(c => _mapper.Map<ClienteEntity>(c));
        }

        private List<UsuarioEntity> CrearListaUsuariosTest()
        {
            var list = new List<UsuarioEntity>();
            list.Add(_mapper.Map<UsuarioEntity>(new UsuarioBuilderTest()
                .WithId("123id")
                .WithNombreCompleto("Alberto Velazques")
                .WithRol(Roles.Admin).Build())
                );

            return list;
        }

        #endregion Métodos privados
    }
}