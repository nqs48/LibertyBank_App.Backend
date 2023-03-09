using AutoMapper;
using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Usuarios;
using Domain.UseCase.Clientes;
using Domain.UseCase.Usuarios;
using EntryPoints.GRPc.Dtos;
using EntryPoints.GRPc.Protos;
using Grpc.Core;

namespace EntryPoints.GRPc.RPCs;

public class ClientesRpcServices : ClienteServices.ClienteServicesBase
{
    private readonly IUsuarioUseCase _usuarioUseCase;
    private readonly IClienteUseCase _clienteUseCase;
    private readonly IMapper _mapper;

    public ClientesRpcServices(IUsuarioUseCase usuarioUseCase, IClienteUseCase clienteUseCase, IMapper mapper)
    {
        _usuarioUseCase = usuarioUseCase;
        _clienteUseCase = clienteUseCase;
        _mapper = mapper;
    }

    public override async Task<UsuarioProto> CrearUsuario(CrearUsuarioRequest request, ServerCallContext context)
    {
        CrearUsuarioProto usuarioDto = _mapper.Map<CrearUsuarioProto>(request);
        Usuario usuarioARetornar = await _usuarioUseCase.Crear(_mapper.Map<Usuario>(usuarioDto));
        return _mapper.Map<UsuarioProto>(usuarioARetornar);
    }

    public override async Task<ClienteProto> CrearCliente(ClienteACrear request, ServerCallContext context)
    {
        var clienteDto = _mapper.Map<CrearClienteProto>(request);
        var clienteARetornar = await _clienteUseCase.CrearCliente(request.IdUsuario, _mapper.Map<Cliente>(clienteDto));
        return _mapper.Map<ClienteProto>(clienteARetornar);
    }
}