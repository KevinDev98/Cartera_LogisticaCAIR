using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class FacturasProveedore
    {
        public string FpIdRegistro { get; set; }
        public DateTime? FpFechaCreacion { get; set; }
        public DateTime FpFechaInicio { get; set; }
        public DateTime FpFechaVencimiento { get; set; }
        public int FpNumeroCaja { get; set; }
        public int FpPedimento { get; set; }
        public int? FpNumFactProv { get; set; }
        public int FpNumFactCair { get; set; }
        public string FpIdprov { get; set; }
        public string FpTipoMoneda { get; set; }
        public double? FpSubtotal { get; set; }
        public double? FpIva { get; set; }
        public double? FpCargoExtra { get; set; }
        public double? FpRetencion { get; set; }
        public double FpCostoTotal { get; set; }
        public string FpReferencia { get; set; }
        public string FpFormaPago { get; set; }
        public string FpEstatus { get; set; }
        public DateTime? FpFechaPago { get; set; }

        public virtual CatProveedore FpIdprovNavigation { get; set; }
    }
}
