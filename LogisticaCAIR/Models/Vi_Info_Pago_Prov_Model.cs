using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LogisticaCAIR.Models
{
    public class Vi_Info_Pago_Prov_Model
    {
        public int NumDoc { get; set; }
        public string Cliente { get; set; }
        public string SubTotal { get; set; }
        public string Iva { get; set; }
        public string Retencion { get; set; }
        public string MontoTotal { get; set; }
        public string Banco { get; set; }
        public string NumeroCta { get; set; }
        public string FechaPago { get; set; }
        public string Verificador { get; set; }
        public string FechaRegistro { get; set; }
        public string TipoMoneda { get; set; }
        public string ConceptoAPagar { get; set; }
    }
}
