using Domain.Model.Entities.Clientes;
using Domain.Model.Entities.Cuentas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Clientes
{
    /// <summary>
    /// Caso de uso de cliente
    /// </summary>
    public interface IClienteUseCase
    {
        /// <summary>
        /// Crear cliente
        /// </summary>
        /// <param name="idUsuarioCreacion"></param>
        /// <param name="nuevoCliente"></param>
        /// <returns></returns>
        Task<Cliente> CrearCliente(string idUsuarioCreacion, Cliente nuevoCliente);

        /// <summary>
        /// Actualizar correo electrónico
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="nuevoCorreo"></param>
        /// <returns></returns>
        Task<Cliente> ActualizarCorreoElectronico(string idCliente, string nuevoCorreo);

        /// <summary>
        /// Obtener cliente por Id
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        Task<Cliente> ObtenerClientePorId(string idCliente);

        /// <summary>
        /// Habilitar cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        Task<bool> HabilitarCliente(string idCliente);

        /// <summary>
        /// Deshabilitar cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        Task<bool> DeshabilitarCliente(string idCliente);

        /// <summary>
        /// Agregar productos al cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="nuevaCuenta"></param>
        /// <returns></returns>
        Task<Cliente> AgregarProductosCliente(string idCliente, Cuenta nuevaCuenta);

        /// <summary>
        /// Habilita cliente tienen deuda
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        Task<bool> HabilitarDeudaCliente(string idCliente);

        /// <summary>
        /// Deshabilita cliente tiene deuda
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        Task<bool> DeshabilitarDeudaCliente(string idCliente);

        /// <summary>
        /// Obtiene una lista de <see cref="Cliente"/>
        /// </summary>
        /// <returns></returns>
        Task<List<Cliente>> ObtenerTodos();
    }
}