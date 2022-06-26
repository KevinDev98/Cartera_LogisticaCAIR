using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class ViInfoCobroCliente
    {
        public int NumDoc { get; set; }
        public string Cliente { get; set; }
        public string TipoMoneda { get; set; }
        public string SubTotal { get; set; }
        public string Iva { get; set; }
        public string Retencion { get; set; }
        public string MontoTotal { get; set; }
        public string Banco { get; set; }
        public string NumeroCta { get; set; }
        public string FechaPago { get; set; }
        public string Verificador { get; set; }
        public string FechaRegistro { get; set; }
        public string ConceptoApagar { get; set; }
    }
}
