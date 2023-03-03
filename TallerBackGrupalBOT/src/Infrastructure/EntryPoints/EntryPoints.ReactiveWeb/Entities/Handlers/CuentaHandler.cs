using Domain.Model.Entities.Cuentas;
using Domain.Model.Entities.Transacciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entities.Handlers
{
    /// <summary>
    /// Handler DTO de entidad <see cref="Cuenta"/>
    /// </summary>
    public class CuentaHandler
    {

        public string Id { get; set; }
        public string IdCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public TipoCuenta tipoCuenta { get; set;}
        public EstadoCuenta EstadoCuenta { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoDisponible { get; set; }
        public bool Exenta { get; set; }
        public List<TransacciónHandler> Transacciones { get; set; }
        
        
    }

}
