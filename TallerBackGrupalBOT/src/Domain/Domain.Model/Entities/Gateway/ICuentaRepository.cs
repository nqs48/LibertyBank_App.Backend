using Domain.Model.Entities.Cuentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface ICuentaRepository
    {
        Task<List<Cuenta>> ObtenerTodos();

        Task<Cuenta> ObtenerPorId(string IdCuenta);

        Task<Cuenta> Crear(Cuenta cuenta);

        Task<Cuenta> Actualizar(string IdCuenta, Cuenta cuenta);
    }
}