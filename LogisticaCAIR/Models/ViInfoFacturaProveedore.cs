using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class ViInfoFacturaProveedore
    {
        public int NumDoc { get; set; }
        public string Proveedor { get; set; }
        public string TipoMoneda { get; set; }
        public string SubTotal { get; set; }
        public string Iva { get; set; }
        public string Retencion { get; set; }
        public string MontoTotal { get; set; }
        public string FormaPago { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string FechaPago { get; set; }
        public string Estatus { get; set; }
    }
}
