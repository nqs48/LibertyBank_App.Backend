using Domain.Model.Entities.Cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entities.Commands
{
    /// <summary>
    /// Comando para crear una entidad de tipo <see cref="Cuenta"/>
    /// </summary>
    public class EstadosCuenta
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

    }
}
