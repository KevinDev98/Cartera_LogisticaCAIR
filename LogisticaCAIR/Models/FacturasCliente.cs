using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class FacturasCliente
    {
        public string FcIdRegistro { get; set; }
        public DateTime? FcFechaCreacion { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FcFechaInicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FcFechaVencimiento { get; set; }
        public int FcNumFactCair { get; set; }
        public string FcIdclie { get; set; }
        public string FcTipoMoneda { get; set; }
        public double? FcSubtotal { get; set; }
        public double? FcIva { get; set; }
        public double? FcCargoExtra { get; set; }
        public double? FcRetencion { get; set; }
        public double FcCostoTotal { get; set; }
        public string FcNumeroComplemento { get; set; }
        public string FcFormaPago { get; set; }
        public string FcEstatus { get; set; }
        public DateTime? FcFechaPago { get; set; }

        public virtual CatCliente FcIdclieNavigation { get; set; }
    }
}
