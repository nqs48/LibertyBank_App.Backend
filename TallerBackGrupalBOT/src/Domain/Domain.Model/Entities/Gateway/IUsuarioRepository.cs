using Domain.Model.Entities.Usuarios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObtenerTodos();

        Task<Usuario> ObtenerPorId(string IdUsuario);

        Task<Usuario> Crear(Usuario usuario);
    }
}