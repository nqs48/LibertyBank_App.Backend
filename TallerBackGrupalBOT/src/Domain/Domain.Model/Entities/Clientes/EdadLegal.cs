using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Clientes
{
    /// <summary>
    /// grupo de edades legales en diferentes países
    /// </summary>
    public enum EdadLegal
    {
        /// <summary>
        /// Edad legal en Colombia
        /// </summary>
        col = 18,

        /// <summary>
        /// Edad legal en Estados Unidos
        /// </summary>
        usa = 21
    }
}