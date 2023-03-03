using Domain.Model.Entities.Clientes;
using System;
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
        /// <param name="saldo"></param>
        /// <param name="saldoDisponible"></param>
        /// <param name="exenta"></param>
        public Cuenta(string id, string idCliente, string numeroCuenta, TipoCuenta tipoCuenta, decimal saldo, decimal saldoDisponible, bool exenta)
        {
            Id = id;
            IdCliente = idCliente;
            NumeroCuenta = numeroCuenta;
            TipoCuenta = tipoCuenta;
            EstadoCuenta = EstadoCuenta.Activa;
            Saldo = saldo;
            SaldoDisponible = saldoDisponible;
            Exenta = exenta;
            HistorialModificaciones = new List<Modificación>();
        }

        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idCliente"></param>
        /// <param name="tipoCuenta"></param>
        /// <param name="saldo"></param>
        /// <param name="exenta"></param>
        public Cuenta(string idCliente,TipoCuenta tipoCuenta, decimal saldo, bool exenta)
        {
            IdCliente = idCliente;
            TipoCuenta = tipoCuenta;
            EstadoCuenta = EstadoCuenta.Activa;
            Saldo = saldo;
            Exenta = exenta;
            HistorialModificaciones = new List<Modificación>();
        }

        /// <summary>
        /// Asignar saldo Inicial
        /// </summary>
        public void AsignarSaldoInicial(decimal saldoInicial) => Saldo = saldoInicial;

        /// <summary>
        /// Calcular Saldo Disponible
        /// </summary>
        public void CalcularSaldoDisponible(decimal GMF) => SaldoDisponible = Saldo - (Saldo * GMF);

        /// <summary>
        /// Generar y Asigna Numero una Cuenta.
        /// </summary>
        public void AsignarNumeroCuenta()
        {
            var random = new Random();
            int numeroAleatorio = random.Next(10000000, 99999999);
            if (TipoCuenta.Equals(TipoCuenta.Corriente))
            {
                NumeroCuenta = $"23-{numeroAleatorio}";
            }
            else if (TipoCuenta.Equals(TipoCuenta.Ahorros))
            {
                NumeroCuenta = $"46-{numeroAleatorio}";
            }
        }

        /// <summary>
        /// Marcar como Cuenta Exenta GMF
        /// </summary>
        public void MarcarCuentaExenta() => Exenta = true;

        /// <summary>
        /// Se agrega nueva modificacion
        /// </summary>
        /// <param name="nuevaModificación"></param>
        public void AgregarModificacion(Modificación nuevaModificación)
        {
            HistorialModificaciones.Add(nuevaModificación);
        }

        /// <summary>
        /// Actualizar Saldo
        /// </summary>
        /// <param name="nuevoSaldo"></param>
        public void ActualizarSaldo(decimal nuevoSaldo)
        {
            Saldo = nuevoSaldo;
        }

        /// <summary>
        /// Validar si Esta Activa
        /// </summary>
        public bool EstaActiva() => EstadoCuenta.Equals(EstadoCuenta.Activa);

        /// <summary>
        /// Validar si Esta Inactiva
        /// </summary>
        public bool EstaInactiva() => EstadoCuenta.Equals(EstadoCuenta.Inactiva);

        /// <summary>
        /// Validar si Esta Cancelada.
        /// </summary>
        public bool EstaCancelada() => EstadoCuenta.Equals(EstadoCuenta.Cancelada);

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