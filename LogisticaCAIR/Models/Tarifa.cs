using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class Tarifa
    {
        public Tarifa()
        {
            PagoProveedores = new HashSet<PagoProveedore>();
        }

        public int IdTarifa { get; set; }
        public string TipoTarifa { get; set; }
        public string Cliente { get; set; }
        public double? TarifExportacion { get; set; }
        public double? TarifImportacion { get; set; }

        public virtual ICollection<PagoProveedore> PagoProveedores { get; set; }
    }
}
