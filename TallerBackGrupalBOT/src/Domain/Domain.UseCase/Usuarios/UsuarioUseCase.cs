using credinet.exception.middleware.models;
using Domain.Model.Entities.Gateway;
using Domain.Model.Entities.Usuarios;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Usuarios
{
    /// <summary>
    /// Caso de uso de entidad <see cref="Usuario"/>
    /// </summary>
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Crea una instancia del caso de uso <see cref="UsuarioUseCase"/>
        /// </summary>
        /// <param name="usuarioRepository"></param>
        public UsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Devuelve todas las entidades de tipo <see cref="Usuario"/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _usuarioRepository.ObtenerTodosAsync();
        }

        /// <summary>
        /// Devuelve una entidad de tipo <see cref="Usuario"/> por su Id
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<Usuario> ObtenerPorId(string idUsuario)
        {
            Usuario usuario = await _usuarioRepository.ObtenerPorIdAsync(idUsuario);
            if (usuario is null)
            {
                throw new BusinessException(TipoExcepcionNegocio.EntidadNoEncontrada.GetDescription(),
                    (int)TipoExcepcionNegocio.EntidadNoEncontrada);
            }

            return usuario;
        }

        /// <summary>
        /// Crea una entidad de tipo <see cref="Usuario"/>
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> Crear(Usuario usuario)
        {
            return await _usuarioRepository.CrearAsync(usuario);
        }
    }
}