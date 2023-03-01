using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    public interface ITransacciónRepository
    {
        Task<List<Transacción>> ObtenerTodos();

        Task<Transacción> ObtenerPorId(string IdTransacción);

        Task<Transacción> Crear(Transacción transacción);

        Task<Transacción> Actualizar(string IdTransacción, Transacción transacción);
    }
}