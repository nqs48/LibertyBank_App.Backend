using Domain.Model.Entities.Clientes;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Domain.Model.Entities.Cuentas
{
    /// <summary>
    /// Clase Cuenta
    /// </summary>
    public class Cuenta
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// IdCliente
        /// </summary>
        public string IdCliente { get; private set; }
        /// <summary>
        /// NumeroCuenta
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// TipoCuenta
        /// </summary>
        public TipoCuenta TipoCuenta { get; private set; }
        /// <summary>
        /// EstadoCuenta
        /// </summary>
        public EstadoCuenta EstadoCuenta { get; private set; }
        /// <summary>
        /// Saldo
        /// </summary>
        public decimal Saldo { get; private set; }
        /// <summary>
        /// SaldoDisponible
        /// </summary>
        public decimal SaldoDisponible { get; set; }
        /// <summary>
        /// Exenta
        /// </summary>
        public bool Exenta { get; private set; }
        /// <summary>
        /// HistorialModificaciones
        /// </summary>
        public List<Modificación> HistorialModificaciones { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCliente"></param>
        /// <param name="numeroCuenta"></param>
        /// <param name="tipoCuenta"></param>
        /// <param name="estadoCuenta"></param>
        /// <param name="saldo"></param>
        /// <param name="saldoDisponible"></param>
        /// <param name="exenta"></param>
        public Cuenta(string id, string idCliente, string numeroCuenta, TipoCuenta tipoCuenta, EstadoCuenta estadoCuenta, decimal saldo, decimal saldoDisponible, bool exenta)
        {
            Id = id;
            IdCliente = idCliente;
            NumeroCuenta = numeroCuenta;
            TipoCuenta = tipoCuenta;
            EstadoCuenta = estadoCuenta;
            Saldo = saldo;
            SaldoDisponible = saldoDisponible;
            Exenta = exenta;
            HistorialModificaciones = new List<Modificación>();
        }

        /// <summary>
        /// Asignar saldo Inicial
        /// </summary>
        public void AsignarSaldoInicial(decimal saldoInicial) => Saldo = saldoInicial;

        /// <summary>
        /// Calculo Saldo Disponible
        /// </summary>
        public void CalcularSaldoDisponible(decimal GMF) => SaldoDisponible =  Saldo - (Saldo * GMF) ;

        /// <summary>
        /// Marcar como Cuenta Exenta.
        /// </summary>
        public void MarcarCuentaExenta() => Exenta = true;

        /// <summary>
        /// Se agrega nueva actualizacion al cliente
        /// </summary>
        /// <param name="nuevaActualizacion"></param>
        public void AgregarModificacion(Modificación nuevaModificación)
        {
            HistorialModificaciones.Add(nuevaModificación);
        }

        /// <summary>
        /// Habilitar una Cuenta.
        /// </summary>
        public void HabilitarCuenta() => EstadoCuenta = EstadoCuenta.Activa;

        /// <summary>
        /// Deshabilitar una Cuenta.
        /// </summary>
        public void DeshabilitarCuenta() => EstadoCuenta = EstadoCuenta.Inactiva;

        /// <summary>
        /// Cancelar una Cuenta.
        /// </summary>
        public void CancelarCuenta() => EstadoCuenta = EstadoCuenta.Cancelada;

        
    }
}